using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroXChange.View
{
    public partial class BMColorCodedWithPriceWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public BMColorCodedWithPriceWindow()
        {
            InitializeComponent();
        }

        private void BMColorCodedWithPriceWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void ChartUpdate(object sender, EventArgs e)
        {
            ControlPaintUtils.Update(chart);
        }

        private void BMColorCodedWithPriceWindow_Load(object sender, EventArgs e)
        {
            //Utils.Suspend(chart);
        }
    }
}
