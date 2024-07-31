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
    public partial class Form13 : Form
    {
        private Form7 form7;
        public Form13(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
        }
        public double CaloriesBurned { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text;

            // Get the user's target number of calories to burn from the database
            string query = "SELECT targetCalories FROM Profile WHERE name = @name";
            double targetCalories = 0;
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
                targetCalories = Convert.ToDouble(reader["targetCalories"]);
            }
            double distance = 0.0;
            double speed = 0.0;
            double weight = 0.0;

            // Validating the values
            try
            {
                distance = double.Parse(textBox2.Text);
                if (distance <= 0)
                {
                    MessageBox.Show("Distance must be greater than 0.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for distance.");
                return;
            }

            try
            {
                speed = double.Parse(textBox3.Text);
                if (speed <= 0)
                {
                    MessageBox.Show("Speed must be greater than 0.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for speed.");
                return;
            }

            try
            {
                weight = double.Parse(textBox4.Text);
                if (weight <= 0)
                {
                    MessageBox.Show("Weight must be greater than 0.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for weight.");
                return;
            }

            // Calculate the calories burned 
            double caloriesBurned = (0.0175 * weight * (distance / 1000) * (speed / 1.609)) / 60;

            // Insert calculated calories into the database
            query = "INSERT INTO Cycling (Name, Distance, Speed, Weight, CaloriesBurned) VALUES (@name, @distance, @speed, @weight, @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@distance", distance);
                command.Parameters.AddWithValue("@speed", speed);
                command.Parameters.AddWithValue("@weight", weight);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }
            string insertQuery = "INSERT INTO UserActivities (Username, Activity, CaloriesBurned) VALUES (@username, 'Cycling', @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@username", Form4.CurrentUsername);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }
            // Display the result
            CaloriesBurned = caloriesBurned;

            if (caloriesBurned >= targetCalories)
            {
                MessageBox.Show("Congratulations! You have burned " + caloriesBurned.ToString("N2") + " calories, which is more than your target of " + targetCalories + " calories.");
            }
            else
            {
                MessageBox.Show("You have burned " + caloriesBurned.ToString("N2") + " calories, which is less than your target of " + targetCalories + " calories. You need to burn " + (targetCalories - caloriesBurned) + " more calories to reach your target.");
            }
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
