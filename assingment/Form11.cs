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

namespace assingment
{
    public partial class Form11 : Form
    {
        private Form7 form7;
        public Form11(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public double CaloriesBurned { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
           
            string name = textBox5.Text;

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/Asus/Desktop/register.mdb;";

            double weight;

            int minutes;

            string intensity;

            double caloriesBurned;

            string query;

            // Retrieve the weight from the profile database
            query = "SELECT weight FROM Profile WHERE name = @name";

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

                if (!double.TryParse(reader["weight"].ToString(), out weight) || weight <= 0)
                {
                    MessageBox.Show("Invalid weight value in the profile database.");
                    return;
                }
            }

            // Get the duration of the yoga session and the intensity level from the user
            if (!int.TryParse(textBox3.Text, out minutes) || minutes <= 0)
            {
                MessageBox.Show("Invalid duration. Please enter a positive integer.");
                return;
            }

            intensity = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(intensity))
            {
                MessageBox.Show("Please select an intensity level.");
                return;
            }

            // Calculate the calories burned
            switch (intensity.ToLower())
            {
                case "low":
                    caloriesBurned = (weight * 0.00035) * minutes;
                    break;
                case "medium":
                    caloriesBurned = (weight * 0.00046) * minutes;
                    break;
                case "high":
                    caloriesBurned = (weight * 0.00056) * minutes;
                    break;
                default:
                    MessageBox.Show("Invalid intensity level.");
                    return;
            }

            CaloriesBurned = caloriesBurned;

            // Insert the yoga metrics and calculated calories into the database
            query = "INSERT INTO Yoga (name, minutes, intensity, caloriesBurned) VALUES (@name, @minutes, @intensity, @caloriesBurned)";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@minutes", minutes);
                command.Parameters.AddWithValue("@intensity", intensity);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // Retrieve the target calories from the profile database
            query = "SELECT targetCalories FROM Profile WHERE name = @name";
            double targetCalories;

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

                if (!double.TryParse(reader["targetCalories"].ToString(), out targetCalories) || targetCalories <= 0)
                {
                    MessageBox.Show("Invalid target calories value in the profile database.");
                    return;
                }
            }
            string insertQuery = "INSERT INTO UserActivities (Username, Activity, CaloriesBurned) VALUES (@username, 'Yoga', @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@username", Form4.CurrentUsername);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }
            // Check if the user has reached their target calories
            if (CaloriesBurned >= targetCalories)
            {
                MessageBox.Show("Congratulations! You have reached your target calories burned for today.");
                this.Close();
            }
            else
            {
                MessageBox.Show("You have burned " + caloriesBurned + " calories during your yoga session. You need to burn " + (targetCalories - CaloriesBurned) + " more calories to reach your target.");
            }

            this.Close();
            form7.UpdateCalories();
            textBox3.Text = "";
            comboBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
    
}
