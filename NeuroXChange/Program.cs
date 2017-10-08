﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroXChange
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                new NeuroXApplication();
            }catch (Exception e)
            {
                MessageBox.Show(e.Message, "NeuroXChange", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
