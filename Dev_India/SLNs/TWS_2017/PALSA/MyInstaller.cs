using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;


namespace PALSA
{
    [RunInstaller(true)]
    public partial class MyInstaller : System.Configuration.Install.Installer
    {
        public MyInstaller()
        {
            InitializeComponent();
        }
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            //FileInfo finf = new FileInfo(Application.StartupPath + "\\StockChartX.ocx");
            //if (finf.Exists)
            //{
                //ProcessStartInfo p1 = new ProcessStartInfo("cmd", "/c " + @"regsvr32.exe StockChartX.ocx");
                //p1.UseShellExecute = true;
                //p1.Verb = "runas";
                //p1.WorkingDirectory = Application.StartupPath;
                //p1.CreateNoWindow = true;
                //Process.Start(p1);
            //}
            //else
            //{
            //    MessageBox.Show("File not found.");
            //}
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

    }
}
