using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace PALSA
{
    public partial class SplashScreen : Form
    {
        public bool Finished;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;
            var version = GetApplicationVersionNumber();
            lblVersion.Text = string.Format("version {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        private Version GetApplicationVersionNumber()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            return assembly.GetName().Version;
        }

        private void tmrUnload_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
            tmrUnload.Enabled = false;
            Finished = true;
        }
    }
}
