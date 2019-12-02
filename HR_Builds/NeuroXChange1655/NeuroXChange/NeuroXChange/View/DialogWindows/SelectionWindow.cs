using NeuroXChange.Common;
using NeuroXChange.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroXChange.View.DialogWindows
{
    public partial class SelectionWindow : Form
    {
        public SelectionEnum status;
        public MainNeuroXView _mainNeuroXView;
        public MainWindow mWindow;
        public SelectionWindow()
        {
            InitializeComponent();
        }

        public SelectionWindow(MainWindow mainWindow)
        {
            mWindow = mainWindow;
            InitializeComponent();
        }

        public SelectionWindow(MainWindow mainWindow, MainNeuroXView mainNeuroXView)
        {
            mWindow = mainWindow;
            _mainNeuroXView = mainNeuroXView;
            InitializeComponent();

            // Nano
            btnTrade.Enabled = false;
            btnViewReports.Enabled = false;
        }
            

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTraningSession_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Function not implemented.");
                //write to log
                NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Traning button clicked", DateTime.Now);

                //status = SelectionEnum.Training;
                _mainNeuroXView.status = SelectionEnum.Training;
                _mainNeuroXView.mainWindow.ShowDialog();
                //Close();
                //Hide();
                //mainNeuroXView.RunApplication
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnTrade_Click(object sender, EventArgs e)
        {
            try
            {
                //write to log
                NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Trade button clicked", DateTime.Now);
                status = SelectionEnum.Trade;
                _mainNeuroXView.status = SelectionEnum.Trade;
                _mainNeuroXView.mainWindow.ShowDialog();
                //Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
            try
            {
                //write to log
                NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Reports button clicked", DateTime.Now);
                status = SelectionEnum.Reports;
                _mainNeuroXView.status = SelectionEnum.Reports;
                var _ini = new IniFileReader();
                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = _ini.Read("PostTradingAnalysis", "Authorisation", @"C:\NeuroTrader\PostTradingAnalysis.exe");
                //pProcess.StartInfo.Arguments = inputPath + " " + outputPath;
                pProcess.Start();
                pProcess.WaitForExit();
                pProcess.Close();//Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //write to log
            NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Application exit button clicked", DateTime.Now);
            Application.Exit();            
        }
    }
}
