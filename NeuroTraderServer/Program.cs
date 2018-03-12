using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Data.SqlClient;
using global::NeuroTraderServer.Properties;

public static class AsynchronousSocketListener
{
    public static char[] credentialsSeparators = {'\t', '\v'};

    // Thread signal.  
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    // Azure database connection
    public static SqlConnection connection;

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
        // Establish the local endpoint for the socket.  
        // The DNS name of the computer  
 
        var dnsGetHostName = Dns.GetHostName();
        //var dnsGetHostName = "localhost";

        Console.WriteLine($"Dns.GetHostName: '{dnsGetHostName}'");
        IPHostEntry ipHostInfo = Dns.GetHostEntry(dnsGetHostName);
        IPAddress ipAddress = ipHostInfo.AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork);
        Console.WriteLine($"ipAddress: '{ipAddress}'");
        ushort port = Settings.Default.Port;
        Console.WriteLine($"Port: '{port}'");
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.  
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.  
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Wait until a connection is made before continuing.  
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.  
        allDone.Set();

        // Get the socket that handles the client request.  
        Socket listener = (Socket)ar.AsyncState;
        using (Socket handler = listener.EndAccept(ar))
        {
            string content = string.Empty;
            while (content.IndexOf("\v") < 0)
            {
                var bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                content += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            }

            var credentials = content.Split(credentialsSeparators);
            Console.WriteLine($"\tReceived credentials: {string.Join(" : ", credentials)}");
            var resultMsg = new byte[1];
            resultMsg[0] = 200;
            if (credentials.Count() < 2)
            {
                resultMsg[0] = 201;
            }
            else
            {
                using (var command = new SqlCommand($"SELECT * FROM dbo.Users WHERE [Login] = '{credentials[0]}'", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            resultMsg[0] = 202;
                        }
                        else
                        {
                            var correctPassword = reader["Password"].ToString();
                            if (credentials[1] != correctPassword)
                            {
                                resultMsg[0] = 203;
                            }
                        }
                    }
                }
            }

            switch (resultMsg[0])
            {
                case 200:
                    Console.WriteLine("\tAuthorised");
                    break;
                case 201:
                    Console.WriteLine("\tError in the protocol");
                    break;
                case 202:
                    Console.WriteLine("\tNo such user");
                    break;
                case 203:
                    Console.WriteLine("\tWrong password");
                    break;
            }

            handler.Send(resultMsg);

            handler.Close();
        }
    }

    public static int Main(String[] args)
    {
        try
        {
            ConnectToDatabase();
            StartListening();
        }
        catch(Exception e)
        {
            Console.WriteLine("Error occured: " + e.Message);
        }
        return 0;
    }
}
