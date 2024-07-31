using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace assingment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        internal void button1_Click(object sender, EventArgs e)
        {
            string uname = textBox1.Text;
            string pass = textBox2.Text;
            string que = textBox3.Text;
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(que))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }
            if (Regex.IsMatch(uname, "^[a-zA-Z0-9]+$") && uname.Length <= 50 && pass.Length == 12)
            {
                if (Regex.IsMatch(pass, "^(?=.*[a-z])(?=.*[A-Z]).+$"))
                {
                    string st = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Asus/Desktop/register.mdb;";
                    OleDbConnection cn = new OleDbConnection(st);
                    cn.Open();
                    string qry = "insert into registration values('" + uname + "','" + pass + "','" + que + "')";
                    OleDbCommand command = new OleDbCommand(qry, cn);
                    command.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("User registered! Proceed to Login");
                    Form4 form4 = new Form4();
                    form4.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Password must contain at least one lowercase and one uppercase letter.");
                }
            }
            else
            {
                MessageBox.Show("Username can only contain letters and numbers. Password must be 12 characters.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}
