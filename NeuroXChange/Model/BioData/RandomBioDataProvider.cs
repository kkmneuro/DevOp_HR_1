using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public class RandomBioDataProvider : AbstractBioDataProvider
    {
        private volatile bool NeedStop = false;
        private Thread thread;
        private Random random;

        public RandomBioDataProvider()
        {
            thread = new Thread(new ThreadStart(GenerateNewData));
            random = new Random();
        }

        private void GenerateNewData()
        {
            while (!NeedStop)
            {
                Thread.Sleep(250);
                var data = new BioData();
                data.psychophysiological_Session_Data_ID = random.Next();
                data.time = DateTime.Now;
                data.temperature = 20 + random.NextDouble() * 20;
                data.hartRate = 40 + random.NextDouble() * 30;
                data.skinConductance = 1 + random.NextDouble();
                data.accX = -20 - random.NextDouble() * 40;
                data.accY = -20 - random.NextDouble() * 40;
                data.accZ = -20 - random.NextDouble() * 40;
                data.session_Component_ID = 2;
                data.sub_Component_ID = 4;
                data.sub_Component_Protocol_ID = random.Next(70,80);
                data.sub_Protocol_ID = random.Next(10, 400);
                data.participant_ID = 1;
                NotifyObservers(data);
            }
        }

        public override void StartProcessing()
        {
            thread.Start();
        }

        public override void StopProcessing()
        {
            NeedStop = true;
        }
    }
}
