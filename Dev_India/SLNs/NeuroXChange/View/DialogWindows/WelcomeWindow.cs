using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuroXChange.Model;
using NeuroXChange.Model.ServerConnection;

namespace NeuroXChange.View.DialogWindows
{
   

    public partial class WelcomeWindow : Form
    {
        private MainNeuroXModel _model;
       
public WelcomeWindow(MainNeuroXModel model)
        {
            _model = model;
            InitializeComponent();           
        }


   

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void WelcomeWindow_Load(object sender, EventArgs e)
        {
            timer.Start();
            timerblur.Start();
            this.Opacity = 0;

            lblHeader.Text = "Welcome " + Globals.CurrentUserId;


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Hide();
        }

        private void timerblur_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.1;
        }

        private void WelcomeWindow_Closing(object sender, FormClosingEventArgs e)
        {
            _model.ReInitalize();
            Globals.BuildTableSchema();
        }
    }
}
