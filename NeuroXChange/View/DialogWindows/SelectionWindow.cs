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
using System.Diagnostics;

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
        }
            

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTraningSession_Click(object sender, EventArgs e)
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

        private void btnTrade_Click(object sender, EventArgs e)
        {
            //write to log
            NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Trade button clicked", DateTime.Now);
            status = SelectionEnum.Trade;
            //Close();

            //ggo dodo za test
           //_mainNeuroXView.status = SelectionEnum.Trade;
           //_mainNeuroXView.mainWindow.ShowDialog();
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
            //write to log
            NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Reports button clicked", DateTime.Now);
            status = SelectionEnum.Reports;
            //Close();
            _mainNeuroXView.status = SelectionEnum.Reports;
            Process.Start("PostTradingAnalysis");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //write to log
            NeuroXChange.Model.Globals.LoggerClient.WriteLog(Globals.AccountId.ToString(), "Application exit button clicked", DateTime.Now);
            Application.Exit();            
        }
    }
}
