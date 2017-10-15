using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public class UdpBioDataProvider : AbstractBioDataProvider
    {
        private volatile bool NeedStop = false;
        private int port;
        private Socket winSocket;
        private Thread thread;

        public UdpBioDataProvider(int port)
        {
            this.port = port;
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, port);
            winSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            winSocket.Bind(serverEndPoint);

            thread = new Thread(new ThreadStart(GenerateNewData));
        }

        private void GenerateNewData()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            byte[] data = new byte[4096];
            winSocket.ReceiveTimeout = 1000;
            char[] sepChar = {'|'};

            while (!NeedStop)
            {
                int recv = 0;
                try
                {
                    recv = winSocket.ReceiveFrom(data, data.Length, SocketFlags.None, ref Remote);
                    string message = Encoding.ASCII.GetString(data, 0, recv);
                    var data_tps = new BioData();
                    var args = message.Split(sepChar);
                    if (args.Length > 0)
                        data_tps.psychophysiological_Session_Data_ID = Int32.Parse(args[0]);
                    if (args.Length > 1)
                        data_tps.time = DateTime.Parse(args[1]);
                    if (args.Length > 2)
                        data_tps.temperature = Double.Parse(args[2]);
                    if (args.Length > 3)
                        data_tps.hartRate = Double.Parse(args[3]);
                    if (args.Length > 4)
                        data_tps.skinConductance = Double.Parse(args[4]);
                    if (args.Length > 5)
                        data_tps.accX = Double.Parse(args[5]);
                    if (args.Length > 6)
                        data_tps.accY = Double.Parse(args[6]);
                    if (args.Length > 7)
                        data_tps.accZ = Double.Parse(args[7]);
                    if (args.Length > 8)
                        data_tps.session_Component_ID = Int32.Parse(args[8]);
                    if (args.Length > 9)
                        data_tps.sub_Component_ID = Int32.Parse(args[9]);
                    if (args.Length > 10)
                        data_tps.sub_Component_Protocol_ID = Int32.Parse(args[10]);
                    if (args.Length > 11)
                        data_tps.sub_Protocol_ID = Int32.Parse(args[11]);
                    if (args.Length > 12)
                        data_tps.participant_ID = Int32.Parse(args[12]);
                    if (args.Length > 13)
                        data_tps.data = args[13];
                    NotifyObservers(data_tps);
                } catch
                {
                }
            }
        }

        public override void StartProcessing()
        {
            thread.Start();
        }

        public override void StopProcessing()
        {
            winSocket.Shutdown(SocketShutdown.Both);
            NeedStop = true;
        }
    }
}
