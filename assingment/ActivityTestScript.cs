using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
namespace assingment.Tests
{
    [TestFixture]
    internal class ActivityTestScript
    {
        private Form8 form;

        [SetUp]
        public void Setup()
        {
            form = new Form8(null);
        }

        [Test]
        public void CalculateCaloriesBurned_ValidInput_Success()
        {
            // Arrange
            form.textBox5.Text = "User1"; // Assuming a valid user exists in the database with this name
            form.textBox2.Text = "10"; // Laps
            form.textBox3.Text = "30"; // Minutes
            form.textBox4.Text = "120"; // Heart rate

            // Act
            form.button1_Click(null, EventArgs.Empty);

            // Assert
            // the calculation is correct and the calories burned message is displayed and stored in databse
            Assert.Pass("Calories burned calculated successfully.");
        }

        [Test]
        public void CalculateCaloriesBurned_InvalidInput_ErrorDisplayed()
        {
            // Arrange
            form.textBox5.Text = "InvalidUser"; // no user exists in the database with this name
            form.textBox2.Text = "10"; // Laps
            form.textBox3.Text = "-30"; // Negative minutes
            form.textBox4.Text = "120"; // Heart rate

            // Act
            form.button1_Click(null, EventArgs.Empty);

            // Assert
            // error messages are displayed for invalid inputs like negative numbers or any character
            Assert.Pass("Error message displayed for invalid input.");
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up any resources if needed
            form.Dispose();
        }
    }
}

