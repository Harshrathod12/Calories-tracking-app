using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace assingment
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private int loginAttempts = 0;
        public static string CurrentUsername { get; private set; }
        internal void button1_Click(object sender, EventArgs e)
        {

            
            string uname = textBox1.Text;
            string pass = textBox2.Text;

            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            string st = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Asus/Desktop/register.mdb;";
            OleDbConnection cn = new OleDbConnection(st);
            cn.Open();

            string qry = "SELECT * FROM registration WHERE username = @username";
            OleDbCommand command = new OleDbCommand(qry, cn);
            command.Parameters.AddWithValue("@username", uname);

            OleDbDataReader reader = command.ExecuteReader();

            if (loginAttempts >= 3)
            {
                MessageBox.Show("Login form is locked due to 3 failed attempt, answer the security question to unlock.");
                Form3 securityQuestionForm = new Form3();
                securityQuestionForm.ShowDialog();

                if (reader.Read() && securityQuestionForm.SecurityQuestion == reader["securityQuestion"].ToString())
                {
                    // Security question answered correctly, reset login attempts
                    loginAttempts = 0;
                    MessageBox.Show("Security question answered correctly. You can try logging in again.");
                }
                else
                {
                    MessageBox.Show("Incorrect security question. You have exceeded the maximum login attempts.");
                }
            }
            else
            {
                if (reader.Read() && pass == reader["password"].ToString())
                {
                    // Login successful
                    MessageBox.Show("Login successful! Welcome " + uname);
                    CurrentUsername = uname;

                    // Check if the user's login record exists in the Login table
                    string checkQuery = "SELECT COUNT(*) FROM Login WHERE username = @username";
                    OleDbCommand checkCommand = new OleDbCommand(checkQuery, cn);
                    checkCommand.Parameters.AddWithValue("@username", uname);
                    int loginCount = (int)checkCommand.ExecuteScalar();

                    if (loginCount > 0)
                    {
                        // User has logged in before, redirect to Form7
                        Form7 form7 = new Form7();
                        form7.Show();
                        this.Hide();
                    }
                    else
                    {
                        // User is logging in for the first time, redirect to Form6
                        Form6 form6 = new Form6();
                        form6.Show();
                        this.Hide();
                    }

                    // Check if the user's login record exists in the Login table
                    string count = "SELECT COUNT(*) FROM Login WHERE username = @username AND [password] = @password";
                    OleDbCommand checkCommand2 = new OleDbCommand(count, cn);
                    checkCommand2.Parameters.AddWithValue("@username", uname);
                    checkCommand2.Parameters.AddWithValue("@password", pass);
                    int loginCount2 = (int)checkCommand2.ExecuteScalar();

                    if (loginCount2 > 0)
                    {
                        // User's login record exists, update the existing record
                        string update = "UPDATE Login SET [password] = @password WHERE username = @username";
                        OleDbCommand updateCommand = new OleDbCommand(update, cn);
                        updateCommand.Parameters.AddWithValue("@username", uname);
                        updateCommand.Parameters.AddWithValue("@password", pass);
                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // User's login record does not exist, insert a new record
                        string insert = "INSERT INTO Login (username, [password]) VALUES (@username, @password)";
                        OleDbCommand insertCommand = new OleDbCommand(insert, cn);
                        insertCommand.Parameters.AddWithValue("@username", uname);
                        insertCommand.Parameters.AddWithValue("@password", pass);
                        insertCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Login failed, increment login attempts
                    loginAttempts++;
                    MessageBox.Show("Invalid username or password. Attempts remaining: " + (3 - loginAttempts));
                }
            }

            reader.Close();
            cn.Close();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
