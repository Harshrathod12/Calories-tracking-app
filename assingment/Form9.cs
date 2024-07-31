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
    public partial class Form9 : Form
    {
        private Form7 form7;
        public Form9(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
        }
        public double CaloriesBurned { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
           
            string name = textBox5.Text;

            // Get the user's target number of calories to burn and weight from the database
            string query = "SELECT weight, targetCalories FROM Profile WHERE name = @name";
            double weight;
            int targetCalories = 0;
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Asus/Desktop/register.mdb;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                connection.Open();
                var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("No profile found for user " + name);
                    return;
                }
                weight = Convert.ToDouble(reader["weight"]);
                targetCalories = Convert.ToInt32(reader["targetCalories"]);
            }

            // Get the number of steps taken, distance walked, and time taken from the user
            int steps;
            double distance;
            int minutes;

            try
            {
                steps = int.Parse(textBox4.Text);
                if (steps < 0)
                {
                    MessageBox.Show("Steps cannot be negative.");
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid steps input.");
                return;
            }

            try
            {
                distance = double.Parse(textBox2.Text);
                if (distance < 0)
                {
                    MessageBox.Show("Distance cannot be negative.");
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid distance input.");
                return;
            }

            try
            {
                minutes = int.Parse(textBox3.Text);
                if (minutes < 0)
                {
                    MessageBox.Show("Minutes cannot be negative.");
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid minutes input.");
                return;
            }


            // Calculate the calories burned
            double caloriesBurned = (steps * 0.04) + (distance * 0.75) + (minutes * 0.01);
           

            // Insert the walking metrics and calculated calories into the database
            query = "INSERT INTO Walking (name, steps, distance, minutes, caloriesBurned) VALUES (@name, @steps, @distance, @minutes, @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@steps", steps);
                command.Parameters.AddWithValue("@distance", distance);
                command.Parameters.AddWithValue("@minutes", minutes);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }


            // Insert the walking metrics and calculated calories into the database
            query = "INSERT INTO Walking (name, steps, distance, minutes, caloriesBurned) VALUES (@name, @steps, @distance, @minutes, @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@steps", steps);
                command.Parameters.AddWithValue("@distance", distance);
                command.Parameters.AddWithValue("@minutes", minutes);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }
            CaloriesBurned = caloriesBurned;
            string insertQuery = "INSERT INTO UserActivities (Username, Activity, CaloriesBurned) VALUES (@username, 'Walking', @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@username", Form4.CurrentUsername);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // Display the result
            if (caloriesBurned >= targetCalories)
            {
                MessageBox.Show("Congratulations! You have burned " + caloriesBurned + " calories, which is more than your target of " + targetCalories + " calories.");
            }
            else
            {
                MessageBox.Show("You have burned " + caloriesBurned + " calories, You need to burn " + (targetCalories - CaloriesBurned) + " more calories to reach your target.");
            } this.Close();
           // form7.totalCaloriesBurned += CaloriesBurned;
            form7.UpdateCalories();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
