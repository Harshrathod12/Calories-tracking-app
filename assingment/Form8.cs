using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace assingment
{
    public partial class Form8 : Form
    {
        private Form7 form7;
        public Form8(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
        }
        public string name { get; set; }
        public double CaloriesBurned { get; set; }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        internal void button1_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text;

            // Get the user's weight and target number of calories to burn from the database
            string query = "SELECT weight, targetCalories FROM Profile WHERE name = @name";
            double weight;
            int targetCalories = 0;     // ACE.OLEDB.12.0   Jet.OLEDB.4.0
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

            // Get the number of laps, minutes, and heart rate from the user
            int laps = 0;
            int minutes = 0;
            int heartRate = 0;

            // Validate the input values
            try
            {
                laps = int.Parse(textBox2.Text);
                if (laps < 0)
                {
                    MessageBox.Show("Number of laps cannot be negative.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for laps.");
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

            try
            {
                heartRate = int.Parse(textBox4.Text);
                if (heartRate < 0)
                {
                    MessageBox.Show("Heart rate cannot be negative.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for heart rate.");
                return;
            }

            // Calculate the calories burned
            double caloriesBurned = laps * (0.02 * (10 * weight / minutes) + 0.05 * heartRate);
            CaloriesBurned = caloriesBurned;

            // Insert the swimming metrics and calculated calories into the database
            query = "INSERT INTO Swimming (name, laps, minutes, heartRate, caloriesBurned) VALUES (@name, @laps, @minutes, @heartRate, @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@laps", laps);
                command.Parameters.AddWithValue("@minutes", minutes);
                command.Parameters.AddWithValue("@heartRate", heartRate);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }
            string insertQuery = "INSERT INTO UserActivities (Username, Activity, CaloriesBurned) VALUES (@username, 'Swimming', @caloriesBurned)";
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
                DialogResult result = MessageBox.Show("Well done! You have burned " + caloriesBurned + " calories, which is more than your target of " + targetCalories + " calories.", "Success", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                   
                    this.Close();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("You have burned " + caloriesBurned + " calories, You need to burn " + (targetCalories - CaloriesBurned) + " more calories to reach your target.", "Not Enough Calories", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    
                    this.Close();
                }
            }
           // form7.totalCaloriesBurned += caloriesBurned;
            form7.UpdateCalories();

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        internal void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
