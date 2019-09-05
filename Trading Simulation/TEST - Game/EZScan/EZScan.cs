using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TestStack.White;

namespace EZScan
{
    /// <summary>
    /// 
    /// </summary>
    public enum TimeFrame 
    {
       FiftyMiliSeonds,// =0.05,
       TenthOfSecond,// =0.1,
       ForthOfSecond,// =0.25,
       HalfSecond,// =0.5,
       ThreeForthOfSecond,// =0.75,
       OneSecond,// =1,
       TwoSeconds // =2,
    }

    /// <summary>
    /// 
    /// </summary>
    public struct EZData
    {
        public DateTime dt;
        public string a;
        public string b;
        public string c;
        public string d;
        public string e;
        public string f;
        public string g;
        public string h;
        public string i;
        public string j;
    }


    /// <summary>
    /// 
    /// </summary>
    public class EZScan
    {

/*
        private TestStack.White.Application app;

        private TestStack.White.UIItems.ListViewCell lvc1; // cells with data
        private TestStack.White.UIItems.ListViewCell lvc2;
        private TestStack.White.UIItems.ListViewCell lvc3;
        private TestStack.White.UIItems.ListViewCell lvc4;
        private TestStack.White.UIItems.ListViewCell lvc5;
        private TestStack.White.UIItems.ListViewCell lvc6;
        private TestStack.White.UIItems.ListViewCell lvc7;
        private TestStack.White.UIItems.ListViewCell lvc8;
        private TestStack.White.UIItems.ListViewCell lvc9;
        private TestStack.White.UIItems.ListViewCell lvc10; */
        //TestStack.White.UIItems.ListBoxItems.ComboBox cmbb; // refresh rate controlas

      //  private TestStack.White.UIItems.ListBoxItems.Win32ComboBox cmbb; // refresh rate controlas

        private int rfr; // refresh rate integerris
        private string refeshRate; // refresh rate stringas
    /*    private TestStack.White.UIItems.ListView lv;      // List vies with channel data
        private TestStack.White.UIItems.Button btC; //Connect button
        private TestStack.White.UIItems.Button btD; //Close button

        */



        private string file;
        private TimeFrame tf;

        /// <summary>
        /// Before data reading inicialication is needed.
        /// </summary>
        /// <param name="fileLocationofEZ">Location of file EZScan.exe including file name</param>
        /// <param name="timef">One of values from EZScan.TimeFrame enum.</param>
        public EZScan(string fileLocationofEZ, TimeFrame timef)
        {
       /*     file = fileLocationofEZ;
            tf = timef;

            app = TestStack.White.Application.Launch(file);
            TestStack.White.UIItems.WindowItems.Window wd = app.GetWindow("E-Z Scan");
            wd.WaitWhileBusy();
            cmbb = wd.Get<TestStack.White.UIItems.ListBoxItems.Win32ComboBox>(TestStack.White.UIItems.Finders.SearchCriteria.ByAutomationId("1126"));
            cmbb.Select((int)tf);
            wd.WaitWhileBusy();

            */
         
         //ToDo
         //   btC = wd.Get<TestStack.White.UIItems.Button>(
           //     TestStack.White.UIItems.Finders.SearchCriteria.ByText("Connect"));
        //ToDo
        //    btD = wd.Get<TestStack.White.UIItems.Button>(
           //     TestStack.White.UIItems.Finders.SearchCriteria.ByText("Close"));

      //      lv = wd.Get<TestStack.White.UIItems.ListView>(
        //                TestStack.White.UIItems.Finders.SearchCriteria.ByText("Channel Properties"));

            //A=A, D=F, F=D,,G=E, H=G

       /*a*/   //  lvc1 = lv.Rows[3].Cells["CH #A"];
       /*b*/   //  lvc2 = lv.Rows[3].Cells["CH #B"];
       /*c*/   //  lvc3 = lv.Rows[3].Cells["CH #C"];
       /*d*/   //  lvc4 = lv.Rows[3].Cells["CH #D"];  // F                                                    
       /*e*/   //  lvc5 = lv.Rows[3].Cells["CH #E"];  // E 
       /*f*/   //  lvc6 = lv.Rows[3].Cells["CH #F"];   // D
       /*g*/   //  lvc7 = lv.Rows[3].Cells["CH #G"];  // G ???         
       /*h*/   //  lvc8 = lv.Rows[3].Cells["CH #H"];  // H
       /*i*/   //  lvc9 = lv.Rows[3].Cells["CH #I"];
       /*j*/   //  lvc10= lv.Rows[3].Cells["CH #J"];
            
        }

        /// <summary>
        /// This one is not implemented yet it supposes to connect to device automatically (Simulation of button "Connect" click). 
        /// </summary>
        public void conectToDevice()
        {
           // btC.Click();
        }

        /// <summary>
        /// Returns status of device 
        /// </summary>
        /// <returns>Returns data type EZScan.EZData (raw data from chanels A-J)</returns>
        public EZData GetEZData()
        {
            EZData ezstatus = new EZData();
      /*      ezstatus.dt = System.DateTime.Now;
            ezstatus.a = lvc1.Text;
            ezstatus.b = lvc2.Text;
            ezstatus.c = lvc3.Text;
            ezstatus.d = lvc4.Text;
            ezstatus.e = lvc5.Text;
            ezstatus.f = lvc6.Text;
            ezstatus.g = lvc7.Text;
            ezstatus.h = lvc8.Text;
            ezstatus.i = lvc9.Text;
            ezstatus.j = lvc10.Text;*/
           
            return ezstatus;
        }

        /// <summary>
        /// Closes E-Z Scanner application 
        /// </summary>
        public void CloseEZ()
        {
            // btC.Click();  // ??
       //     app.Close();
        }

        
        /// <summary>
        /// Destructor 
        /// </summary>
        ~EZScan() {
            //app.Close(); 
        }


    }
}
