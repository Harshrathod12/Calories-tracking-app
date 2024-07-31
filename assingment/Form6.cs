using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assingment
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            string name = textBox1.Text;
            string age = textBox2.Text;
            string height = textBox3.Text;
            string weight = textBox4.Text;
            bool gender = checkBox1.Checked || checkBox2.Checked;
            string cal = textBox5.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(age) || string.IsNullOrEmpty(height) || string.IsNullOrEmpty(weight) || !gender)
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }
          

            string st = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Asus/Desktop/register.mdb;";
            OleDbConnection cn = new OleDbConnection(st);
            cn.Open();
            string qry = "insert into Profile values('" + name + "','" + age + "','" + height + "','" + weight + "','" + gender + "','" + cal + "')";
            OleDbCommand command = new OleDbCommand(qry, cn);
            command.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Hello " + name + " Ready to burn some calories?");
            Form7 form7 = new Form7();
            form7.Show();
            this.Hide();

          

        }


        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
