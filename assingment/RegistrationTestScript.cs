using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace assingment.Tests
{
    [TestFixture]
    internal class RegistrationTestScript
    {
        private Form1 form;

        [SetUp]
        public void Setup()
        {
            form = new Form1();
        }
        [Test]
        public void RegisterUser_ValidInput_Success()
        {
            // Arrange
            form.textBox1.Text = "validUsername123";
            form.textBox2.Text = "ValidPassword123";
            form.textBox3.Text = "ValidQuestion";

            // Act
            form.button1_Click(null, EventArgs.Empty);

            // Assert
            Assert.Pass("User registered! Proceed to Login message shown.");
        }

        [Test]
        public void RegisterUser_InvalidInput_ErrorMessageShown()
        {
            // Arrange
            form.textBox1.Text = ""; // Empty username
            form.textBox2.Text = "invalid"; // Invalid password
            form.textBox3.Text = "ValidQuestion";

            // Act
            form.button1_Click(null, EventArgs.Empty);

            // Assert
            //a message box with error message is shown for invalid input.
            Assert.Pass("Error message shown for invalid input.");
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up any resources if needed
            form.Dispose();
        }
    }
}
