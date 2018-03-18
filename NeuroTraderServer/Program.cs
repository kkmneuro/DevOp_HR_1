using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;
using global::NeuroTraderServer.Properties;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using NeuroTraderProtocols;

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

                        ProcessClient(sslStream);

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

        public static void ProcessClient(SslStream sslStream)
        {
            // Protocol description: General (1 byte), Authorisation block, [Other blocks]
            // Block description: block header (1 byte), block data

            BinaryReader reader = new BinaryReader(sslStream);

            var generalHeader = (NTProtocolHeader)reader.ReadByte();
            if (generalHeader != NTProtocolHeader.General)
            {
                return;
            }

            long userId = -1;
            BinaryFormatter formatter = new BinaryFormatter();
            int blocksReaded = 0;
            while (true)
            {
                NTProtocolHeader blockHeader;

                try
                {
                    blockHeader = (NTProtocolHeader)reader.ReadByte();
                }
                // No more blocks
                catch(EndOfStreamException)
                {
                    Console.WriteLine($"\tBlock reading finished succesfully, readed {blocksReaded} blocks");
                    break;
                }
                catch(Exception e)
                {
                    Console.WriteLine("\tConnection forcibly closed by the client! Potential error");
                    break;
                }

                if (blockHeader != NTProtocolHeader.Authorisation && userId == -1)
                {
                    throw new Exception("User was not authorised!");
                }

                switch (blockHeader)
                {
                    case NTProtocolHeader.Authorisation:
                        
                        userId = ProcessAuthorisation(formatter, sslStream);
                        break;
                    case NTProtocolHeader.RequestStatistics:
                        ProcessRequestStatistics(formatter, sslStream, userId);
                        break;
                    case NTProtocolHeader.NewData:
                        ProcessNewDataRequest(formatter, sslStream, userId);
                        break;
                }
                blocksReaded++;
            }
        }

        public static long ProcessAuthorisation(BinaryFormatter formatter, SslStream stream)
        {
            var data = (AuthorisationPacket)formatter.Deserialize(stream);

            long result = -1;
            AuthorisationResult authorisationResult = AuthorisationResult.UnknownError;

            Console.WriteLine($"\tReceived credentials:\t'{data.Login}'\t'{data.Password}'");

            // trick to skip further code, by calling break in this loop
            do
            {
                using (var command = new SqlCommand($"SELECT [Id], [Login], [Password] FROM dbo.Users WHERE [Login] = @login", connection))
                {
                    command.Parameters.AddWithValue("@login", data.Login);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            authorisationResult = AuthorisationResult.NoSuchUser;
                            break;
                        }

                        var correctPassword = reader["Password"].ToString();
                        if (data.Password != correctPassword)
                        {
                            authorisationResult = AuthorisationResult.WrongPassword;
                            break;
                        }

                        authorisationResult = AuthorisationResult.Ok;
                        result = long.Parse(reader["Id"].ToString());
                    }
                }
            } while (false);

            switch (authorisationResult)
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

            var resultData = new byte[1];
            resultData[0] = (byte)authorisationResult;
            stream.Write(resultData, 0, 1);
            stream.Flush();

            return result;
        }

        public static void ProcessRequestStatistics(BinaryFormatter formatter, SslStream stream, long userId)
        {
            var stats = new StatisticsPacket();
            stats.LastBioDataTime = 0;
            stats.LastOrderTime = 0;
            stats.LastUserActionTime = 0;

            using (var command = new SqlCommand(@"SELECT TOP 1 [Time] As TopTime FROM dbo.UserActions WHERE UserId = @UserId ORDER BY [TopTIme] DESC", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                var sqlResult = command.ExecuteScalar();
                if (sqlResult != null)
                {
                    stats.LastUserActionTime = DateTime.Parse(sqlResult.ToString()).ToOADate();
                }
            }

            formatter.Serialize(stream, stats);
            stream.Flush();
        }

        public static void ProcessNewDataRequest(BinaryFormatter formatter, SslStream stream, long userId)
        {
            var data = formatter.Deserialize(stream);

            try
            {
                if (data is UserActionsDataPacket)
                {
                    var packet = (UserActionsDataPacket)data;
                    using (var command = new SqlCommand(
                        @"INSERT INTO [dbo].[UserActions]
                           ([UserID]
                           ,[ActionID]
                           ,[Time]
                           ,[Data])
                     VALUES
                           (@UserID
                           ,@ActionID
                           ,@Time
                           ,@Data)", connection))
                    {
                        foreach (var action in packet.Actions)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@UserID", userId);
                            command.Parameters.AddWithValue("@ActionID", (int)action.ActionID);
                            command.Parameters.AddWithValue("@Time", DateTime.FromOADate(action.Time));
                            command.Parameters.AddWithValue("@Data", action.Data);
                            command.ExecuteNonQuery();
                        }
                    }
                    Console.WriteLine($"\tAdd UserActionsData with {packet.Actions.Length} items");
                }
            }catch(Exception e)
            {
                Console.WriteLine("Error in processing DataRequest: " + e.Message);
            }
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
