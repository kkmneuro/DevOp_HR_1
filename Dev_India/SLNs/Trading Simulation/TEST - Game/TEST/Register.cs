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

using System.ComponentModel.DataAnnotations;

using TradingAccount;

namespace TEST
{
    public partial class Register : Form
    {


        public Entities Dt { get; set; }

        public bool UserCreated { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        /* 

        cmd2 = new OleDbCommand();
        cmd2.CommandType = CommandType.Text;
        cmd2.Connection = myconn;

         */



        public Register()
        {

            TEST.Helpers.Configuration.LoadConf();
            InitializeComponent();


            /*
            cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = myconn;

            cmd.CommandText = "???"

            cmd.ExecuteNonQuery(); // ??
            */

            // lets init initial price

        }


        private void textBox4Name_Enter(object sender, EventArgs e)
        {
            if (textBox4Name.Text == "Name") textBox4Name.Text = "";
        }

        private void textBox4Name_Leave(object sender, EventArgs e)
        {
            if (textBox4Name.Text == "") textBox4Name.Text = "Name";
        }

        private void textBox1Email_Enter(object sender, EventArgs e)
        {
            if (textBox1Email.Text == "Email") textBox1Email.Text = "";
        }

        private void textBox1Email_Leave(object sender, EventArgs e)
        {
            if (textBox1Email.Text == "") textBox1Email.Text = "Email";
        }

        private void textBox2Pass_Leave(object sender, EventArgs e)
        {
            if (textBox2Pass.Text == "")
            {
                textBox2Pass.Text = "Password";
                textBox2Pass.PasswordChar = (char)0;
            }
        }

        private void textBox2Pass_Enter(object sender, EventArgs e)
        {
            if (textBox2Pass.Text == "Password")
            {
                textBox2Pass.Text = "";
                textBox2Pass.PasswordChar = '*';
            }

        }

        private void textBox3PassConf_Enter(object sender, EventArgs e)
        {
            if (textBox3PassConf.Text == "Confirm Password")
            {
                textBox3PassConf.Text = "";
                textBox3PassConf.PasswordChar = '*';
            }
        }

        private void textBox3PassConf_Leave(object sender, EventArgs e)
        {
            if (textBox3PassConf.Text == "")
            {
                textBox3PassConf.Text = "Confirm Password";
                textBox3PassConf.PasswordChar = (char)0;
            }
        }

        private void textBox5Company_Enter(object sender, EventArgs e)
        {
            if (textBox5Company.Text == "Company") textBox5Company.Text = "";
        }

        private void textBox5Company_Leave(object sender, EventArgs e)
        {
            if (textBox5Company.Text == "") textBox5Company.Text = "Company";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                if (registesNewUser())
                {
                    UserCreated = true;
                    Email = textBox1Email.Text;
                    Password = textBox2Pass.Text;
                    this.Hide();
                }
                else
                {
                    UserCreated = false;
                    Email = "";
                    Password = "";
                }
            }
        }

        private bool registesNewUser()
        {
            int a = 0; 
            Users u = new Users();
            u.Name = textBox4Name.Text;
            u.Email = textBox1Email.Text.ToLower();
            u.Password = textBox2Pass.Text;
            u.Company = textBox5Company.Text;
            u.UserID = System.Guid.NewGuid();

            try
            {
               var eu = Dt.Users.First<Users>(x => x.Email == u.Email);
                if (eu != null) MessageBox.Show("User with e-mail - " + u.Email + " already exists.", "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                try
                {
                    Dt.Users.Add(u);
                    a = Dt.SaveChanges();
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Error While Registering a New User.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (a == 1) return true; // one record updated

            return false;
        }

        private bool validateForm()
        {
            if (textBox4Name.Text == "Name")
            {
                MessageBox.Show("Please Enter Your Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1Email.Text == "Email")
            {
                MessageBox.Show("Please Enter Your Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1Email.Text != "Email")
            {
                var a = new EmailAddressAttribute();
                bool isValid = a.IsValid(textBox1Email.Text);
                if (!isValid)
                {
                    MessageBox.Show("Email Adress Seems To Be Not Valid, Please Enter a Valid One.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            


            if (textBox2Pass.Text == "Password")
            {
                MessageBox.Show("Please Enter Password For Your Account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2Pass.Text == "Confirm Password")
            {
                MessageBox.Show("Please Corfim Your Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2Pass.Text != textBox3PassConf.Text)
            {
                MessageBox.Show("Passwords Do Not Match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // textBox2Pass.Text = "Password";
                // textBox2Pass.Text = "Confirm Password";
                return false;
            }

            if (textBox5Company.Text == "Company")
            {
                MessageBox.Show("Enter Your Company Name or 'none' If Not From Company. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
