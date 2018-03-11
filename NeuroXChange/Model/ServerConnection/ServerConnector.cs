using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NeuroXChange.Model.ServerConnection
{
    public class ServerConnector
    {
        private MainNeuroXModel model;

        public bool SaveCredentials { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string Server { get; private set; }
        public ushort Port { get; private set; }

        public ServerConnector(MainNeuroXModel model)
        {
            this.model = model;

            UserLogin = string.Empty;
            UserPassword = string.Empty;

            SaveCredentials = bool.Parse(model.iniFileReader.Read("SaveCredentials", "Authorisation", "false"));
            if (SaveCredentials)
            {
                UserLogin = model.iniFileReader.Read("Login", "Authorisation", string.Empty);
                UserPassword = model.iniFileReader.Read("Password", "Authorisation", string.Empty);
            }

            Server = model.iniFileReader.Read("Server", "Authorisation", "127.0.0.1");
            Port = ushort.Parse(model.iniFileReader.Read("Port", "Authorisation", "7657"));
        }

        public void UpdateINICredentials()
        {
            model.iniFileReader.Write("SaveCredentials", SaveCredentials.ToString(), "Authorisation");
            model.iniFileReader.Write("Login", SaveCredentials ? UserLogin : string.Empty, "Authorisation");
            model.iniFileReader.Write("Password", SaveCredentials ? UserPassword : string.Empty, "Authorisation");
        }

        public bool ConnectToServer(out string errorMessage)
        {
            errorMessage = null;

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Server);
                IPAddress ipAddress = ipHostInfo.AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(remoteEP);

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes($"{UserLogin}\t{UserPassword}\v");

                    // Send the data through the socket.  
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    byte[] bytes = new byte[16];
                    int bytesRec = sender.Receive(bytes);

                    // Release the socket.  
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    // 200 - is Ok
                    switch(bytes[0])
                    {
                        case 201:
                            errorMessage = "Error in the protocol!";
                            return false;
                        case 202:
                            errorMessage = "Wrong credentials!\nCan't find such user";
                            return false;
                        case 203:
                            errorMessage = "Wrong credentials!\nWrong password";
                            return false;
                    }
                }
                catch (Exception e)
                {
                    errorMessage = e.ToString();
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorMessage = e.ToString();
                return false;
            }

            return true;
        }
    }
}
