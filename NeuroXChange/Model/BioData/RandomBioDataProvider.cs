using NeuroXChange.Common;
using NeuroXChange.Model.Database;
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
        private int bioDataTickInterval;

        public RandomBioDataProvider(MainNeuroXModel model,
            LocalDatabaseConnector localDatabaseConnector,
            IniFileReader iniFileReader)
            :base(model, localDatabaseConnector)
        {
            bioDataTickInterval = Int32.Parse(iniFileReader.Read("BioDataTickInterval", "BioData", "500"));

            thread = new Thread(new ThreadStart(GenerateNewData));
            random = new Random();
        }

        private void GenerateNewData()
        {
            while (!NeedStop)
            {
                var data = new BioData();

                data.time = DateTime.Now;
                data.temperature = 20 + random.NextDouble() * 20;
                data.heartRate = 40 + random.NextDouble() * 30;
                data.skinConductance = 1 + random.NextDouble();
                data.accX = -20 - random.NextDouble() * 40;
                data.accY = -20 - random.NextDouble() * 40;
                data.accZ = -20 - random.NextDouble() * 40;
                FillApplicaitonStates(data);

                data.id = localDatabaseConnector.WriteBioData(data);

                NotifyObservers(BioDataEvent.NewBioDataTick, data);

                Thread.Sleep(bioDataTickInterval);
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
