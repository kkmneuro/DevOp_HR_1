using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Data.SqlClient;
using global::NeuroTraderServer.Properties;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;

namespace NeuroTraderServer
{
    public static class AsynchronousSocketListener
    {
        public static char[] credentialsSeparators = { '\t', '\v' };

        static X509Certificate serverCertificate = null;

        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        // Azure database connection
        public static SqlConnection connection;

        public static void InitializeSSLCertificate()
        {
            Console.WriteLine($"Certificate initialization...");
            X509Certificate2Collection collection = new X509Certificate2Collection();
            collection.Import(Settings.Default.SslCertificatePath, Settings.Default.SslCertificatePassword, X509KeyStorageFlags.PersistKeySet);
            serverCertificate = collection[0];
            Console.WriteLine("Initialized!\n");
        }

        public static void ConnectToDatabase()
        {
            var cb = new SqlConnectionStringBuilder();
            cb.DataSource = Settings.Default.DataSource;
            cb.UserID = Settings.Default.UserID;
            cb.Password = Settings.Default.Password;
            cb.InitialCatalog = Settings.Default.InitialCatalog;

            Console.WriteLine($"Connecting to database {cb.InitialCatalog} on {cb.DataSource} ...");
            connection = new SqlConnection(cb.ConnectionString);
            connection.Open();
            Console.WriteLine("Connected!\n");
        }

        public static void StartListening()
        {
            Console.WriteLine($"Started listening on port {Settings.Default.Port}");

            try
            {
                // Create a TCP/IP (IPv4) socket and listen for incoming connections.
                TcpListener listener = new TcpListener(IPAddress.Any, Settings.Default.Port);
                listener.Start();
                while (true)
                {
                    allDone.Reset();
                    listener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), listener);

                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            var listener = (TcpListener)ar.AsyncState;

            using (TcpClient client = listener.EndAcceptTcpClient(ar))
            {
                try
                {
                    Console.WriteLine($"{DateTime.Now}\tAttempt to connect with {client.Client.RemoteEndPoint}");

                    using (SslStream sslStream = new SslStream(client.GetStream(), false))
                    {
                        sslStream.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, true);

                        Console.WriteLine($"\t{DateTime.Now}\tConnected!");
                        Console.WriteLine("\tIs Encrypted: {0}", sslStream.IsEncrypted);

                        string content = string.Empty;
                        while (content.IndexOf("\v") < 0)
                        {
                            var bytes = new byte[1024];
                            int bytesRec = sslStream.Read(bytes, 0, 1024);
                            content += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        }

                        Authorisation(content, sslStream);

                        sslStream.Close();
                    }
                }
                catch (AuthenticationException e)
                {
                    Console.WriteLine("Exception: {0}", e.Message);
                    if (e.InnerException != null)
                    {
                        Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                    }
                    Console.WriteLine("Authentication failed - closing the connection.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("General Exception: {0}", e.Message);
                }

                client.Close();
            }
        }

        static void Authorisation(string input, SslStream stream)
        {
            AuthorisationResult resultMsg;
            var userId = GetUserIdByCredentials(input, out resultMsg);

            switch (resultMsg)
            {
                case AuthorisationResult.UnknownError:
                    Console.WriteLine("\tUnknown error");
                    break;
                case AuthorisationResult.Ok:
                    Console.WriteLine("\tAuthorised");
                    break;
                case AuthorisationResult.ProtocolError:
                    Console.WriteLine("\tError in the protocol");
                    break;
                case AuthorisationResult.NoSuchUser:
                    Console.WriteLine("\tNo such user");
                    break;
                case AuthorisationResult.WrongPassword:
                    Console.WriteLine("\tWrong password");
                    break;
            }

            var data = new byte[1];
            data[0] = (byte)resultMsg;
            stream.Write(data, 0, 1);
            stream.Flush();
        }

        // return user id in the database if credentials are correct
        public static long GetUserIdByCredentials(string credentialString, out AuthorisationResult authorisationResult)
        {
            long result = -1;
            authorisationResult = AuthorisationResult.UnknownError;

            var credentials = credentialString.Split(credentialsSeparators);
            Console.WriteLine($"\tReceived credentials: {string.Join(" : ", credentials)}");

            if (credentials.Count() != 3)
            {
                authorisationResult = AuthorisationResult.ProtocolError;
                return result;
            }

            string login = credentials[0];
            string password = credentials[1];

            using (var command = new SqlCommand($"SELECT [Id], [Login], [Password] FROM dbo.Users WHERE [Login] = @login", connection))
            {
                command.Parameters.AddWithValue("@login", login);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        authorisationResult = AuthorisationResult.NoSuchUser;
                        return result;
                    }

                    var correctPassword = reader["Password"].ToString();
                    if (password != correctPassword)
                    {
                        authorisationResult = AuthorisationResult.WrongPassword;
                        return result;
                    }

                    authorisationResult = AuthorisationResult.Ok;
                    result = long.Parse(reader["Id"].ToString());
                }
            }

            return result;
        }

        public static int Main(String[] args)
        {
            try
            {
                InitializeSSLCertificate();
                ConnectToDatabase();
                StartListening();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured: " + e.Message);
            }
            return 0;
        }
    }
}
