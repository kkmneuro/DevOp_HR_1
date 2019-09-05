using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NeuroGrantAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Attempt to access file.
            try
            {
                //FileStream stream = null;
                string fileName = Path.Combine(System.Environment.GetFolderPath(
        System.Environment.SpecialFolder.ProgramFiles),"NeuroTrader\\Neuro", "DockPanel.config");

                // Deny 'Everyone' access to the file
                FileSecurity fSecurity = File.GetAccessControl(fileName);
                fSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl(fileName, fSecurity);

                string fileDocPanel = Path.Combine(System.Environment.GetFolderPath(
        System.Environment.SpecialFolder.ProgramFiles), "NeuroTrader\\Neuro", "PTADockPanel.config");

                // Deny 'Everyone' access to the file
                FileSecurity fSecuritypanel = File.GetAccessControl(fileDocPanel);
                fSecuritypanel.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl(fileDocPanel, fSecuritypanel);

                string filesetting = Path.Combine(System.Environment.GetFolderPath(
      System.Environment.SpecialFolder.ProgramFiles), "NeuroTrader\\Neuro", "NeuroXChangeSettings.ini");

                // Deny 'Everyone' access to the file
                FileSecurity fexchangesetting = File.GetAccessControl(filesetting);
                fexchangesetting.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                File.SetAccessControl(filesetting, fexchangesetting);


                

                //stream = new FileStream(fileName, FileMode.Create);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown: " + ex.Message);
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //stream.Close();
                //stream.Dispose();
            }

            //Console.WriteLine("Press any key to exist.");
            //Console.ReadKey();

        }
    }
}