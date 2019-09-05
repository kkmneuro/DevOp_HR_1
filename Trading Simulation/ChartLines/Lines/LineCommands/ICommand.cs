using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartLines.Lines.LineCommands
{
    interface ICommand
    {
        /// <summary>
        /// Bouding action to double click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Cc_MouseDoubleClick(object sender, MouseEventArgs e);
    }
}
