using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace TEST.Helpers
{
    class RegisterDLL
    {
        public static void Registar_Dlls(string filePath)
        {
            try
            {

                string arg_fileinfo = "" + " " + "\"" + filePath + "\"";
                Process reg = new Process();
                reg.StartInfo.FileName = "regsvr32.exe";
                reg.StartInfo.Arguments = arg_fileinfo;
                reg.StartInfo.Verb = "runas";
                reg.Start();
                reg.WaitForExit();
                reg.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

    }
}
