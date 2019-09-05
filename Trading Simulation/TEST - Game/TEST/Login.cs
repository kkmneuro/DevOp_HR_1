using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

using TradingAccount;

namespace TEST
{
    public partial class Login : Form
    {

        private OleDbConnection myconn; // main connestion to MS access file (preferrable to have only one connection across all application) 
     //   private OleDbCommand cmd;     // commnad for db (login cmd)

        private Entities dt;
        private Sessions userSession; // If user logs he has a session
        private Users applicationUser; // User who logs into system

        private FutureSelection futures;


     //  string strdb = "Neuro-Xchange_Psychophysiology1.mdb"; //ConfigurationManager.AppSettings["DBLocation"]; where the data is located

       
            

            /* 

            cmd2 = new OleDbCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = myconn;

             */



        public Login()
        {
        
            TEST.Helpers.Configuration.LoadConf();
            InitializeComponent();

            /*

            myconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + strdb + "'");

            try { 
                myconn.Open();
            }
            catch(Exception e){
                MessageBox.Show("Was not able to establish a connection with local database. \r\n" + e.Message);
            } */

            dt = new Entities();
            dt.Configuration.AutoDetectChangesEnabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((textBox1.Text != "") && (textBox1.Text != "Email")) &&
                ((textBox2.Text != "") && (textBox2.Text != "Password")))
            {

                var us = dt.Users.Where(x => x.Email == textBox1.Text.ToLower());
                if (us.Count() == 0) MessageBox.Show("User Not Found Please Register.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                else
                {
                    var userX = dt.Users.Where(x => x.Email == textBox1.Text.ToLower()).Where(y => y.Password == textBox2.Text);
                    if (userX.Count() == 1)
                    {
                        applicationUser = userX.First(x => x.Email == textBox1.Text.ToLower());

                        userSession = new Sessions();
                        userSession.UserID = applicationUser.UserID;
                        DateTime now = System.DateTime.Now;
                        userSession.LoginTimeUniversal = now.ToUniversalTime();
                        userSession.LoginTimeLocal = now;



                        /// butinai pakaityti
                        //https://se.mathworks.com/campaigns/products/ppc/google/common-machine-learning-challenges.html?s_eid=ppc_30956260162&q=

                        Helpers.LocationInfo li = Helpers.Location.getLocationInfo();

                        userSession.IP = li.IP;
                        userSession.Country = li.Country;
                        userSession.City = li.City;
                        userSession.AddtitionalInfo = li.Text;
                        userSession.OS = Environment.OSVersion.ToString();
                        userSession.LocalTimeZone = TimeZone.CurrentTimeZone.StandardName;
                        userSession.ScreenResolution = Screen.PrimaryScreen.Bounds.Size.ToString();

                        userSession = dt.Sessions.Add(userSession);
                        int i =  dt.SaveChanges();

                        if (i == 1)
                        {
                            textBox1.Text = "Email";
                            textBox2.Text = "";
                            textBox2_Leave(null, null);

                           futures = new FutureSelection(myconn, dt, userSession);
                            futures.FormClosed += Futures_FormClosed;
                            futures.Dt = dt;
                            futures.Show();
                            this.Hide();
                        }
                        else MessageBox.Show("Failed To Create User Session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Failed To Login. Check Your Password.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else MessageBox.Show("Pleas Provide Your Registration Email And Password.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Futures_FormClosed(object sender, FormClosedEventArgs e)
        {
            futures.Dispose();
            this.Show();
           

            //Sessions close
            var time = System.DateTime.Now;
            

            Entities ent = new Entities();

            try
            {
                var a = ent.Sessions.Find(userSession.SessionID);

                a.LogoutTimeLocal = time;
                a.LogOutTimeUniversal = time.ToUniversalTime();

                int x = ent.SaveChanges();

                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

 


        private void button3_Click(object sender, EventArgs e)
        {
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
      
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Email") textBox1.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "") textBox1.Text = "Email";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Passoword")
            {
                textBox2.Text = "";
                textBox2.PasswordChar = '*';
            }
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Passoword";
                textBox2.PasswordChar = (char)0;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var reg = new Register();
            reg.Dt = dt;
            reg.ShowDialog();
            if (((reg.Email != "") && (reg.Password != ""))&&
                ((reg.Email != null) && (reg.Password != null)) &&
                ((reg.Email != "Email") && (reg.Password != "Password")))
            {
                textBox1.Text = reg.Email;
                textBox2.Text = reg.Password;
            }
            reg.Dispose();
            reg = null;
                        
        }

        private void LoginLoad(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection prisijungimas = new System.Data.SqlClient.SqlConnection("Server=185.144.156.161;Database=quickzip;User Id=gatekeeper;Password = ApsaugaNuoVagiu456#$%;");
                string scmd = "SELECT [ID],[ExpirationDate] ,[UserName],[Message],[WaitSecondsOnStart],[WaitSeconds],[ExitOnStart],[ExitRandom] FROM [dbo].[expiration] where username = 'system';";
                System.Data.SqlClient.SqlCommand komanda = new System.Data.SqlClient.SqlCommand(scmd);
                komanda.Connection = prisijungimas;
                prisijungimas.Open();
                using (var skaitykle = komanda.ExecuteReader())
                {
                    skaitykle.Read();
                    DateTime dt = (DateTime)skaitykle["ExpirationDate"];
                    if (dt < System.DateTime.Now) // Jei laikas pasibaiges stabdome.
                    {
                        if ((bool)skaitykle["ExitOnStart"])  // jei iseiti pradzioje
                        {
                            if ((string)skaitykle["Message"] == "")
                            {
                                System.Threading.Thread.Sleep((int)skaitykle["WaitSecondsOnStart"] * 1000);
                                System.Windows.Forms.Application.Exit();
                            }
                            // isejimas su pranesimu
                            MessageBox.Show((string)skaitykle["Message"]);
                            System.Threading.Thread.Sleep((int)skaitykle["WaitSecondsOnStart"] * 1000);
                            System.Windows.Forms.Application.Exit();
                        }
                        else MessageBox.Show((string)skaitykle["Message"]);  // rodom tik pranesima bet kol kas neiseiname
                    }
                }
                prisijungimas.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Unexpected Errors. Please check internet connection or contact support.");
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
