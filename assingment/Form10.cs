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
using System.Xml.Linq;

namespace assingment
{
    public partial class Form10 : Form
    {
        private Form7 form7;
        public Form10(Form7 form7)
        {
            InitializeComponent();
            this.form7 = form7;
        }
        public double CaloriesBurned { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text;
            // Get the user's weight and target number of calories to burn from the database
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
            int minutes = 0;
            double speed = 0;
            double distance = 0;
            try
            {
                distance = double.Parse(textBox2.Text);
                if (distance < 0)
                {
                    MessageBox.Show("Distance cannot be negative.");
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
                speed = double.Parse(textBox4.Text);
                if (speed < 0)
                {
                    MessageBox.Show("speed cannot be negative.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input for speed.");
                return;
            }

            // Calculate the calories burned
            double caloriesBurned = (weight * 0.00075) * (minutes * speed);
            CaloriesBurned = caloriesBurned;

            // Insert the running metrics and calculated calories into the database
            query = "INSERT INTO Running (name, minutes, speed, distance, caloriesBurned) VALUES (@name, @minutes, @speed, @distance, @caloriesBurned)";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@minutes", minutes);
                command.Parameters.AddWithValue("@speed", speed);
                command.Parameters.AddWithValue("@distance", distance);
                command.Parameters.AddWithValue("@caloriesBurned", caloriesBurned);
                connection.Open();
                command.ExecuteNonQuery();
            }
            string insertQuery = "INSERT INTO UserActivities (Username, Activity, CaloriesBurned) VALUES (@username, 'Running', @caloriesBurned)";
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
                MessageBox.Show("Well done! You have burned " + caloriesBurned + " calories, which is more than your target of " + targetCalories + " calories.");
            }
            else
            {
                MessageBox.Show("You have burned " + caloriesBurned + " calories, You need to burn " + (targetCalories - CaloriesBurned) + " more calories to reach your target.");
            } this.Close();
          //  form7.totalCaloriesBurned += CaloriesBurned;
            form7.UpdateCalories();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}
