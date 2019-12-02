using AxTTLLiveCtrlLib;
using System;
using System.Linq;
using System.Windows.Media.Media3D;
using TPSbvp_cl;

namespace CTapp_def_ns
{
	internal class TPSReader
	{
		public enum e_cs
		{
			DISCONNECTED,
			CONNECTED,
			STARTED
		}

		private AxTTLLive TTLLive;

		public int m_hEnc;

		private int m_hChSC;

		private int m_hChBVP;

		private int m_hChTemp;

		private int m_hChAccX;

		private int m_hChAccY;

		private int m_hChAccZ;

		private TPSbvp m_hralg;

		private justbpf m_bpf1;

		private justbpf m_bpf2;

		public const int rate = 300;

		private Vector3D gestureOrientation;

		public TPSReader.e_cs cxnState
		{
			get;
			private set;
		}

		public double SC
		{
			get;
			private set;
		}

		public double Temp
		{
			get;
			private set;
		}

		public double HR
		{
			get;
			private set;
		}

		public double AccX
		{
			get;
			private set;
		}

		public double AccY
		{
			get;
			private set;
		}

		public double AccZ
		{
			get;
			private set;
		}

		public Vector3D acc
		{
			get;
			private set;
		}

		public double gestureAngle
		{
			get;
			private set;
		}

		public TPSReader(AxTTLLive TTLLive)
		{
			this.TTLLive = TTLLive;
			this.m_hralg = new TPSbvp();
			this.m_hralg.Setup(300f);
			this.m_bpf1 = new justbpf();
			this.m_bpf1.Setup(10f, 0.01f, 10f);
			this.m_bpf2 = new justbpf();
			this.m_bpf2.Setup(10f, 0.01f, 2f);
			this.m_hEnc = (this.m_hChSC = (this.m_hChBVP = (this.m_hChTemp = -1)));
			this.m_hChAccX = (this.m_hChAccY = (this.m_hChAccZ = -1));
		}

		public void tryConnection(string cxn)
		{
			this.m_hEnc = this.TTLLive.OpenConnection(cxn, 1000);
			bool flag = this.m_hEnc >= 0;
			if (flag)
			{
				this.TTLLive.AddChannel(this.m_hEnc, 1, ref this.m_hChSC);
				this.TTLLive.set_SensorType(this.m_hChSC, this.TTLLive.get_SensorID(this.m_hChSC));
				this.TTLLive.set_UnitType(this.m_hChSC, 16);
				this.TTLLive.AddChannel(this.m_hEnc, 0, ref this.m_hChBVP);
				this.TTLLive.set_SensorType(this.m_hChBVP, this.TTLLive.get_SensorID(this.m_hChBVP));
				this.TTLLive.AddChannel(this.m_hEnc, 2, ref this.m_hChTemp);
				this.TTLLive.set_SensorType(this.m_hChTemp, this.TTLLive.get_SensorID(this.m_hChTemp));
				this.TTLLive.AddChannel(this.m_hEnc, 3, ref this.m_hChAccX);
				this.TTLLive.set_SensorType(this.m_hChTemp, this.TTLLive.get_SensorID(this.m_hChAccX));
				this.TTLLive.set_UnitType(this.m_hChAccX, 9);
				this.TTLLive.AddChannel(this.m_hEnc, 4, ref this.m_hChAccY);
				this.TTLLive.set_SensorType(this.m_hChTemp, this.TTLLive.get_SensorID(this.m_hChAccX));
				this.TTLLive.set_UnitType(this.m_hChAccY, 9);
				this.TTLLive.AddChannel(this.m_hEnc, 5, ref this.m_hChAccZ);
				this.TTLLive.set_SensorType(this.m_hChTemp, this.TTLLive.get_SensorID(this.m_hChAccZ));
				this.TTLLive.set_UnitType(this.m_hChAccZ, 9);
				this.cxnState = TPSReader.e_cs.CONNECTED;
			}
		}

		public void Start()
		{
			bool flag = this.cxnState == TPSReader.e_cs.CONNECTED;
			if (flag)
			{
				this.TTLLive.StartChannels();
				this.cxnState = TPSReader.e_cs.STARTED;
			}
		}

		public void Stop()
		{
			bool flag = this.cxnState == TPSReader.e_cs.STARTED;
			if (flag)
			{
				this.TTLLive.StopChannels();
				this.cxnState = TPSReader.e_cs.CONNECTED;
			}
		}

		public void Disconnect()
		{
			bool flag = this.cxnState == TPSReader.e_cs.STARTED;
			if (flag)
			{
				this.Stop();
			}
			bool flag2 = this.cxnState == TPSReader.e_cs.CONNECTED;
			if (flag2)
			{
				this.TTLLive.CloseConnections();
				this.cxnState = TPSReader.e_cs.DISCONNECTED;
			}
		}

		public void readValues(out double sc, out double temp, out double hr)
		{
			this.process();
			sc = this.SC;
			temp = this.Temp;
			hr = this.HR;
		}

		public void process()
		{
			bool flag = this.cxnState == TPSReader.e_cs.STARTED;
			if (flag)
			{
				int sA = this.TTLLive.get_SamplesAvailable(this.m_hChSC);
				bool flag2 = sA > 0;
				if (flag2)
				{
					Array buf = (Array)this.TTLLive.ReadChannelDataVT(this.m_hChSC, 1000);
					bool flag3 = buf.GetLength(0) > 0;
					if (flag3)
					{
						this.SC = (double)((float[])buf).Average();
					}
					buf = (Array)this.TTLLive.ReadChannelDataVT(this.m_hChTemp, 1000);
					bool flag4 = buf.GetLength(0) > 0;
					if (flag4)
					{
						this.Temp = (double)((float[])buf).Average();
					}
					buf = (Array)this.TTLLive.ReadChannelDataVT(this.m_hChBVP, 1000);
					int ulim = buf.GetUpperBound(0);
					Console.WriteLine(ulim - 1);
					for (int i = 0; i <= ulim; i++)
					{
						this.m_hralg.Process((float)buf.GetValue(i));
					}
					this.HR = (double)this.m_hralg.HR();
					buf = (Array)this.TTLLive.ReadChannelDataVT(this.m_hChAccX, 1000);
					bool flag5 = buf.GetLength(0) > 0;
					if (flag5)
					{
						this.AccX = (double)((float[])buf).Average();
					}
					buf = (Array)this.TTLLive.ReadChannelDataVT(this.m_hChAccY, 1000);
					bool flag6 = buf.GetLength(0) > 0;
					if (flag6)
					{
						this.AccY = (double)((float[])buf).Average();
					}
					buf = (Array)this.TTLLive.ReadChannelDataVT(this.m_hChAccZ, 1000);
					bool flag7 = buf.GetLength(0) > 0;
					if (flag7)
					{
						this.AccZ = (double)((float[])buf).Average();
					}
					this.acc = new Vector3D(this.AccX, this.AccY, this.AccZ);
					this.acc.Normalize();
					this.gestureAngle = Vector3D.AngleBetween(this.acc, this.gestureOrientation);
				}
			}
		}

		internal void CaptureGestureOrientation()
		{
			this.gestureOrientation = this.acc;
		}
	}
}
