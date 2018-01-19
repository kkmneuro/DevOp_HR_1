﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroXChange.Model.BioData
{
    public class BioData
    {
        public long id;
        public DateTime time;
        public double temperature;
        public double heartRate;
        public double skinConductance;
        public double accX;
        public double accY;
        public double accZ;
        public int trainingType;
        public int trainingStep;
        public int applicationStates;

        public double? sellPrice;
        public double? buyPrice;

        // implementation dependent payload
        public object payload;

        public static BioData FromOleDbDataReader(OleDbDataReader reader)
        {
            var data = new BioData();
            data.id = Int64.Parse(reader["ID"].ToString());
            data.time = DateTime.Parse(reader["Time"].ToString());
            data.temperature = Double.Parse(reader["Temperature"].ToString());
            data.heartRate = Double.Parse(reader["HeartRate"].ToString());
            data.skinConductance = Double.Parse(reader["SkinConductance"].ToString());
            data.accX = Double.Parse(reader["AccX"].ToString());
            data.accY = Double.Parse(reader["AccY"].ToString());
            data.accZ = Double.Parse(reader["AccZ"].ToString());
            data.trainingType = Int32.Parse(reader["TrainingType"].ToString());
            data.trainingStep = Int32.Parse(reader["TrainingStep"].ToString());
            data.applicationStates = Int32.Parse(reader["ApplicationStates"].ToString());

            var sellPrice = reader["SellPrice"].ToString();
            var buyPrice = reader["BuyPrice"].ToString();
            if (sellPrice != "")
            {
                data.sellPrice = Double.Parse(sellPrice);
            }
            if (buyPrice != "")
            {
                data.buyPrice = Double.Parse(buyPrice);
            }

            return data;
        }
    }
}
