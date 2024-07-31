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
    public partial class Form12 : Form
    {
        private Form7 form7;
        public Form12(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
        }
        public double CaloriesBurned { get; set; }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string name = textBox5.Text;

            // Get the user's target number of calories to burn from the database
            string query = "SELECT targetCalories FROM Profile WHERE name = @name";
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
                targetCalories = Convert.ToInt32(reader["targetCalories"]);
            }

            // Get the user's rope skipping metrics from the text boxes
            int jumpCount = 0;
            double jumpHeight = 0.0;
            double jumpWeight = 0.0;
            int minutes = 0;

            // Validate the input values
            try
            {
                jumpCount = int.Parse(textBox2.Text);
                if (jumpCount < 0)
                {
                    MessageBox.Show("Number of jumps cannot be negative.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for jumps per minute.");
                return;
            }

            try
            {
                jumpHeight = double.Parse(textBox3.Text);
                if (jumpHeight <= 0)
                {
                    MessageBox.Show("Jump height must be greater than 0.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for jump height.");
                return;
            }

            try
            {
                jumpWeight = double.Parse(textBox4.Text);
                if (jumpWeight <= 0)
                {
                    MessageBox.Show("Jump weight must be greater than 0.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for jump weight.");
                return;
            }

            try
            {
                minutes = int.Parse(textBox3.Text);
                if (minutes <= 0)
                {
                    MessageBox.Show("Number of minutes must be greater than 0.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for minutes.");
                return;
            }

            // Calculate the calories burned based on the user's metrics
            jumpCount = jumpCount * minutes;
            double caloriesBurned = jumpCount * (0.0021 * jumpWeight + 0.0078 * jumpHeight);

            // Insert the rope skipping metrics and calculated calories into the database
            query = "INSERT INTO RopeSkipping (Name, Minutes, Jumps, CaloriesBurned) VALUES (@name, @minutes, @jumps, @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@minutes", minutes);
                command.Parameters.AddWithValue("@jumps", jumpCount);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }
            string insertQuery = "INSERT INTO UserActivities (Username, Activity, CaloriesBurned) VALUES (@username, 'Rope Skipping', @caloriesBurned)";
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
           // form7.UpdateTotalCaloriesBurned(caloriesBurned);

            if (caloriesBurned >= targetCalories)
            {
                MessageBox.Show("Congratulations! You have burned " + caloriesBurned.ToString("N2") + " calories, which is more than your target of " + targetCalories + " calories.");
            }
            else
            {
                MessageBox.Show("You have burned " + caloriesBurned.ToString("N2") + " calories, which is less than your target of " + targetCalories + " calories. You need to burn " + (targetCalories - (int)caloriesBurned) + " more calories to reach your target.");
            }this.Close();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
