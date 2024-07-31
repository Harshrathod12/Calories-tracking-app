using System;
using System.Collections;
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
    public partial class Form7 : Form
    {
        private User user;

        private Form8 form8;
        private Form9 form9;
        private Form10 form10;
        private Form11 form11;
        private Form12 form12;
        private Form13 form13;
        public double totalCaloriesBurned = 0;
        public Form7 ()
        {
            InitializeComponent();  
            form8 = new Form8(this);
            form9 = new Form9(this);
            form10 = new Form10(this);
            form11 = new Form11(this);
            form12 = new Form12(this);
            form13 = new Form13(this);
            label6.Text = Form4.CurrentUsername;
        }
       

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form8.ShowDialog();
            totalCaloriesBurned += form8.CaloriesBurned;
            UpdateCalories();
        }

        private void solidGauge1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
             form9.ShowDialog();
             totalCaloriesBurned += form9.CaloriesBurned;
             UpdateCalories();
        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("You have been logged out.");
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             form10.ShowDialog();
             totalCaloriesBurned += form10.CaloriesBurned;
             UpdateCalories();
        }
        // this was orignal.
       public void UpdateCalories()
        {
             label4.Text = $"Total Calories Burned: {totalCaloriesBurned.ToString("N2")}";
        }
      

        private void button4_Click(object sender, EventArgs e)
        {
            form11.ShowDialog();
            totalCaloriesBurned += form11.CaloriesBurned;
            UpdateCalories();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
             string st = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Asus/Desktop/register.mdb;";
              OleDbConnection cn = new OleDbConnection(st);
              cn.Open();

              string qry = "SELECT caloriesBurned FROM Swimming WHERE username = @username";
              OleDbCommand command = new OleDbCommand(qry, cn);
              command.Parameters.AddWithValue("@username", form8.Name); // Replace with the current user's username

              double caloriesBurned = 0;
              using (OleDbDataReader reader = command.ExecuteReader())
              {
                  if (reader.Read())
                  {
                      caloriesBurned = Convert.ToDouble(reader["caloriesBurned"]);
                  }
              }

              MessageBox.Show($"Total Calories Burned today for {Form4.CurrentUsername} is {totalCaloriesBurned.ToString("N2")}");

              cn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form12.ShowDialog();
            totalCaloriesBurned += form12.CaloriesBurned;
            UpdateCalories();
        }

        private void solidGauge1_ChildChanged_1(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void solidGauge1_ChildChanged_2(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            form13.ShowDialog();
            totalCaloriesBurned += form13.CaloriesBurned;
            UpdateCalories();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Loadusersactivities();
        }
        private void Loadusersactivities()
        {
            string st = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Asus/Desktop/register.mdb;";
            using (OleDbConnection cn = new OleDbConnection(st))
            {
                cn.Open();

                string qry = "SELECT Activity, CaloriesBurned FROM UserActivities WHERE Username = @username";
                OleDbCommand command = new OleDbCommand(qry, cn);
                command.Parameters.AddWithValue("@username", Form4.CurrentUsername);

                StringBuilder sb = new StringBuilder();
                double totalCaloriesBurned = 0;

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string activity = reader.GetString(0);
                        double caloriesBurned = 0;
                        if (!double.TryParse(reader["CaloriesBurned"].ToString(), out caloriesBurned))
                        {
                            MessageBox.Show($"Invalid calories burned value for activity: {activity}");
                            continue;
                        }
                        sb.AppendLine($"{activity}: {caloriesBurned.ToString("N2")} calories");
                        totalCaloriesBurned += caloriesBurned;
                    }
                }

                MessageBox.Show($"Activities you've performed:\n{sb.ToString()}\nTotal Calories Burned: {totalCaloriesBurned.ToString("N2")}");
               //label7.Text = $"Activities:\n{sb.ToString()}\nTotal Calories Burned: {totalCaloriesBurned.ToString("N2")}";
            }
        }

    }
}
