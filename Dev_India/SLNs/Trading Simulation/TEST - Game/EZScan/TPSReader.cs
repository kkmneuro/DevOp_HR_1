using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;  // just for Vector3D (at this point)


using System.Windows.Forms;

using AxTTLLiveCtrlLib;
using TPSbvp_cl;

/* To-do:
 *      Figure out process and read values
 *      Deal with situation where process is not called evenly, including when it's called with no values to process
 *      This could mean an IIR on SC and Temp? Yes, if we value a bit of de-noising... otherwise in that case we just take 
 *      the last value. Worst case would be a single value.
 */

namespace CTapp_def_ns // _def_ns = default namespace
{
    enum e_pci
    {
        TPS_PHYSCHAN_BVP, TPS_PHYSCHAN_SC, TPS_PHYSCHAN_TEMP,
        TPS_PHYSCHAN_ACCX, TPS_PHYSCHAN_ACCY, TPS_PHYSCHAN_ACCZ
    };

    class TPSReader
    {
        public enum e_cs { DISCONNECTED, CONNECTED, STARTED };
        public e_cs cxnState { get; private set; }

        // TTLLive plus handles
        AxTTLLiveCtrlLib.AxTTLLive TTLLive;
        public int m_hEnc;
        int m_hChSC;
        int m_hChBVP;
        int m_hChTemp;
        int m_hChAccX;
        int m_hChAccY;
        int m_hChAccZ;

        // heart rate algorithm
        TPSbvp m_hralg;

        justbpf m_bpf1, m_bpf2;

        //public int rate {get; private set;}
        public const int rate = 300;

        Vector3D gestureOrientation;

        public TPSReader(AxTTLLiveCtrlLib.AxTTLLive TTLLive)
        {
            this.TTLLive = TTLLive;
            m_hralg = new TPSbvp();
            m_hralg.Setup(rate);

            m_bpf1 = new justbpf();
            m_bpf1.Setup(10.0f, 0.01f, 10.0f);

            m_bpf2 = new justbpf();
            m_bpf2.Setup(10.0f, 0.01f, 2.0f);

            m_hEnc = m_hChSC = m_hChBVP = m_hChTemp = -1;
            m_hChAccX = m_hChAccY = m_hChAccZ = -1;
        }

        public double SC { get; private set; }
        public double Temp { get; private set; }
        public double HR { get; private set; }
        public double AccX { get; private set; }
        public double AccY { get; private set; }
        public double AccZ { get; private set; }
        public Vector3D acc { get; private set; }

        public double gestureAngle { get; private set; }

        public void tryConnection(string cxn)
        {
            m_hEnc = TTLLive.OpenConnection(cxn, 1000);
            if (m_hEnc >= 0)
            {
                TTLLive.AddChannel(m_hEnc, (int)e_pci.TPS_PHYSCHAN_SC, ref m_hChSC);
                TTLLive.set_SensorType(m_hChSC, TTLLive.get_SensorID(m_hChSC));
                TTLLive.set_UnitType(m_hChSC, 16);  // TTLAPI_UT_USIEMENS

                TTLLive.AddChannel(m_hEnc, (int)e_pci.TPS_PHYSCHAN_BVP, ref m_hChBVP);
                TTLLive.set_SensorType(m_hChBVP, TTLLive.get_SensorID(m_hChBVP));

                TTLLive.AddChannel(m_hEnc, (int)e_pci.TPS_PHYSCHAN_TEMP, ref m_hChTemp);
                TTLLive.set_SensorType(m_hChTemp, TTLLive.get_SensorID(m_hChTemp));

                TTLLive.AddChannel(m_hEnc, (int)e_pci.TPS_PHYSCHAN_ACCX, ref m_hChAccX);
                TTLLive.set_SensorType(m_hChTemp, TTLLive.get_SensorID(m_hChAccX));
                TTLLive.set_UnitType(m_hChAccX, 9);  // TTLAPI_UT_COUNT

                TTLLive.AddChannel(m_hEnc, (int)e_pci.TPS_PHYSCHAN_ACCY, ref m_hChAccY);
                TTLLive.set_SensorType(m_hChTemp, TTLLive.get_SensorID(m_hChAccX));
                TTLLive.set_UnitType(m_hChAccY, 9);  // TTLAPI_UT_COUNT

                TTLLive.AddChannel(m_hEnc, (int)e_pci.TPS_PHYSCHAN_ACCZ, ref m_hChAccZ);
                TTLLive.set_SensorType(m_hChTemp, TTLLive.get_SensorID(m_hChAccZ));
                TTLLive.set_UnitType(m_hChAccZ, 9);  // TTLAPI_UT_COUNT

                cxnState = e_cs.CONNECTED;
            }
        }

        public void Start()
        {
            if (cxnState == e_cs.CONNECTED)
            {
                TTLLive.StartChannels();
                cxnState = e_cs.STARTED;
            }
        }

        public void Stop()
        {
            if (cxnState == e_cs.STARTED)
            {
                TTLLive.StopChannels();
                cxnState = e_cs.CONNECTED;
            }
        }

        public void Disconnect()
        {
            if (cxnState == e_cs.STARTED)
            {
                Stop();
            }

            if (cxnState == e_cs.CONNECTED)
            {
                TTLLive.CloseConnections();
                cxnState = e_cs.DISCONNECTED;
            }
        }

        public void readValues(out double sc, out double temp, out double hr)
        {
            process();

            sc = SC;
            temp = Temp;
            hr = HR;
        }


        // Read available samples and perform necessary processing
        public void process()
        {
            Array buf;
            if (cxnState == e_cs.STARTED)
            {
                int sA = TTLLive.get_SamplesAvailable(m_hChSC);
                if (sA > 0)
                {
                    int ulim;

                    // process the SC data
                    buf = (Array)TTLLive.ReadChannelDataVT(m_hChSC, 1000);
                    if (buf.GetLength(0) > 0) SC = ((float[])buf).Average();

                    // process the Temp data
                    buf = (Array)TTLLive.ReadChannelDataVT(m_hChTemp, 1000);
                    if (buf.GetLength(0) > 0) Temp = ((float[])buf).Average();

                    // process the BVP data
                    buf = (Array)TTLLive.ReadChannelDataVT(m_hChBVP, 1000);
                    ulim = buf.GetUpperBound(0); Console.WriteLine(ulim - 1);
                    for (int i = 0; i <= ulim; i++)
                    {
                        m_hralg.Process((float)buf.GetValue(i));
                    }

                    HR = m_hralg.HR();

                    // process the AccX data
                    buf = (Array)TTLLive.ReadChannelDataVT(m_hChAccX, 1000);
                    if (buf.GetLength(0) > 0) AccX = ((float[])buf).Average();

                    // process the AccY data
                    buf = (Array)TTLLive.ReadChannelDataVT(m_hChAccY, 1000);
                    if (buf.GetLength(0) > 0) AccY = ((float[])buf).Average();

                    // process the AccZ data
                    buf = (Array)TTLLive.ReadChannelDataVT(m_hChAccZ, 1000);
                    if (buf.GetLength(0) > 0) AccZ = ((float[])buf).Average();

                    acc = new Vector3D(AccX, AccY, AccZ);
                    acc.Normalize();
                    gestureAngle = (double)Vector3D.AngleBetween(acc, gestureOrientation);
                }
            }
        }

        internal void CaptureGestureOrientation()
        {
            gestureOrientation = acc;
        }
    }
}
