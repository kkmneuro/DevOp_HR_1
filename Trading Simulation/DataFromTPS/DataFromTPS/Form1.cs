using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using EZScan;

namespace DataFromTPS
{
    public partial class Form1 : Form
    {
        TPSScan tps = new TPSScan();

        int lineN = 0;
    

        public Form1()
        {
            InitializeComponent();
            tps.exefile = @"C:\Neuro-Trader\TPS\DCU INTERNAL\TtlLau_Internal.exe";
            tps.datafileDirectory = @"C:\Neuro-Trader\TPS\Export\";
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tPSExecutableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1EXE.ShowDialog();
        }

        private void exportFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1DATA.ShowDialog();
        }

        private void openFileDialog1EXE_FileOk(object sender, CancelEventArgs e)
        {
            tps.exefile = openFileDialog1EXE.FileName;
        }

        private void openFileDialog1DATA_FileOk(object sender, CancelEventArgs e)
        {
            tps.datafileDirectory = openFileDialog1DATA.FileName.Replace(openFileDialog1DATA.SafeFileName,"") ;
        }

        bool prepared = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!prepared)
            {
                tps.prepare();
                prepared = true;
            }

                timer1.Start();
            tps.start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            // reading file

            lineN++;

            label1.Text = "" + lineN;
            EZScan.DataObject dt = tps.getLatestData();

            // ,BVP,HR,Skin conductance, Temperature, AccX, AccY, AccZ
            string x = "BVP=" +dt.BVP + ", HR=" + dt.HR + ", SC=" + dt.SkinConductance + ", T=" + dt.Temperature + ", AccX=" + dt.AccX + ", AccY=" + dt.AccY + ", AccZ=" + dt.AccZ;

            textBox1.AppendText(x + "\r\n");

            //   }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tps.stop();
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";

            tps.filename = "";

            tps.stop();
            timer1.Stop();

            tps.prepare();
            timer1.Start();
            tps.start();
        }
    }
}
