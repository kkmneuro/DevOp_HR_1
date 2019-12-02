using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using CommonLibrary.Cls;
//using Logging;
using System.Reflection;
using System.Linq;
using System.Security.Principal;

namespace PALSA
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            RegisterCOMDependencies();
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new FrmMain());
            //SingleInstance.SingleApplication.Run(new FrmMain());
        }


      

        private static void RegisterCOMDependencies()
        {                //".\\Res\\StockTicker.ocx",//".\\Res\\MFENT.dll",//".\\Res\\tabctl32.ocx"
            var files = new string[] {Application.StartupPath+"\\StockChartX.ocx", Application.StartupPath+"\\TradeScript.dll"};

            for (int index = 0; index < files.Length; index++)
            {
                string file = files[index];
                // unregister first to ensure latest library
                UnregisterCOM(file);
                RegisterCOM(file);
            }
        }

        private static void RegisterCOM(string file)
        {
            //'/s' : indicates regsvr32.exe to run silently.
            var fileinfo = String.Format("/s \"{0}\"", file);

            var reg = new Process
            {
                StartInfo =
                {
                    FileName = "regsvr32.exe",
                    Arguments = fileinfo,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };
            reg.Start();
            reg.WaitForExit();
            reg.Close();
        }

        private static void UnregisterCOM(string file)
        {
            //'/u' : indicates regsvr32.exe to uninstall.
            var fileinfo = String.Format("/s /u \"{0}\"", file);

            var reg = new Process
            {
                StartInfo =
                {
                    FileName = "regsvr32.exe",
                    Arguments = fileinfo,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };
            reg.Start();
            reg.WaitForExit();
            reg.Close();
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs ex)
        {
            string exceptionObject=string.Empty;
            if(ex.ExceptionObject!=null)
                exceptionObject=ex.ExceptionObject.ToString();
            //FileHandling.WriteAllLog("Critical Error : " + sender.GetType().GetProperty("Name") +" > "+ sender.GetType().GetProperty("Parent") +" > " +exceptionObject);

            ClsCommonMethods.ShowErrorBox("Critical Error : " + sender.GetType().GetProperty("Name") + sender.GetType().GetProperty("Parent") + ex.ExceptionObject);

            //Environment.Exit(0);
            //Application.Exit();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs ex)
        {
            //FileHandling.WriteAllLog("Critical Error : " + ex.Exception.Message + " Exception Generated from " + ex.Exception.Source
            //                         + " File in " + ex.Exception.TargetSite + " Method At " +
            //                         ex.Exception.StackTrace);

            ClsCommonMethods.ShowErrorBox("Critical Error : " + ex.Exception.Message + " Exception Generated from" + ex.Exception.Source + " File in" + ex.Exception.TargetSite + " Method At" + ex.Exception.StackTrace);

            //Environment.Exit(0);
            //Application.Exit();
        }
    }

}