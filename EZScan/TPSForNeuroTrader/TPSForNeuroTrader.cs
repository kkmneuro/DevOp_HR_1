using AxTTLLiveCtrlLib;
using CTapp_def_ns;
using System;

namespace TPSForNeuroTrader
{
	public class TPSForNeuroTrader
	{
		private TPSReader tpsr1;

		private int timeFrame;

		private string connectionString;

		public TPSForNeuroTrader(int timef, AxTTLLive TTLLive, string conenctionStr)
		{
			this.timeFrame = timef;
			this.tpsr1 = new TPSReader(TTLLive);
			this.connectionString = conenctionStr;
		}

		public bool conectToDevice()
		{
			try
			{
				this.tpsr1.tryConnection(this.connectionString);
			}
			catch (Exception eee)
			{
				throw new Exception("Connection to device was not succsessful. Check device and try again\r\n" + eee.Message);
			}
			bool flag = this.tpsr1.cxnState == TPSReader.e_cs.DISCONNECTED;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.tpsr1.cxnState == TPSReader.e_cs.CONNECTED;
				result = flag2;
			}
			return result;
		}

		public bool disconectFromDevice()
		{
			this.tpsr1.Disconnect();
			return this.tpsr1.cxnState == TPSReader.e_cs.DISCONNECTED;
		}

		public bool startDevice()
		{
			bool flag = this.tpsr1.cxnState == TPSReader.e_cs.CONNECTED;
			bool result;
			if (flag)
			{
				this.tpsr1.Start();
				bool flag2 = this.tpsr1.cxnState == TPSReader.e_cs.STARTED;
				result = flag2;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool stopDevice()
		{
			bool flag = this.tpsr1.cxnState == TPSReader.e_cs.STARTED;
			if (flag)
			{
				this.tpsr1.Stop();
				return this.tpsr1.cxnState == TPSReader.e_cs.CONNECTED;
			}
			throw new Exception("Trying to stop device which is not started or connected.");
		}

		public e_cs getState()
		{
			return (e_cs)this.tpsr1.cxnState;
		}

		public TPSData GetTPSData()
		{
			TPSData data = new TPSData();
			bool flag = this.tpsr1.cxnState == TPSReader.e_cs.STARTED;
			if (flag)
			{
				string eerr = "";
				try
				{
					this.tpsr1.process();
				}
				catch (Exception exept)
				{
					eerr = exept.Message;
				}
				bool flag2 = eerr == "";
				if (flag2)
				{
					data.dt = DateTime.Now;
					data.SC = this.tpsr1.SC;
					data.HR = this.tpsr1.HR;
					data.Temp = this.tpsr1.Temp * 100.0;
					data.Gesture = this.tpsr1.gestureAngle;
					data.AccX = this.tpsr1.AccX;
					data.AccY = this.tpsr1.AccY;
					data.AccZ = this.tpsr1.AccZ;
				}
			}
			else
			{
				bool flag3 = this.tpsr1.cxnState == TPSReader.e_cs.CONNECTED;
				if (flag3)
				{
					throw new Exception("Device is connected, but not started.");
				}
			}
			return data;
		}

		~TPSForNeuroTrader()
		{
		}
	}
}
