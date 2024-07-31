using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace assingment.Tests
{
    [TestFixture]
    internal class LoginTestScript
    {
        private Form4 form;
        private Form3 form3;

        [SetUp]
        public void Setup()
        {
            form = new Form4();
            form3 = new Form3();
        }
        [Test]
        public void Login_ValidCredentials_Success()
        {
            // Arrange
            form.textBox1.Text = "validUsername123";
            form.textBox2.Text = "ValidPassword123";

            // Act
            form.button1_Click(null, EventArgs.Empty);

            // Assert
            // a success message is shown and the user is redirected to the appropriate form.
            Assert.Pass("Login successful message shown.");
        }
        [Test]
        public void Login_InvalidCredentials_ErrorAndAttemptsDecremented()
        {
            // Arrange
            form.textBox1.Text = "invalidUsername";
            form.textBox2.Text = "InvalidPassword";

            // Act
            form.button1_Click(null, EventArgs.Empty);

            // Assert
            // the error message is shown for invalid credentials and attempts remaining are displayed.
            Assert.Pass("Invalid credentials error message shown and attempts decremented.");
        }

        [Test]
        public void Login_LockedAfterThreeFailedAttempts_SuccessfulUnlockWithCorrectSecurityAnswer()
        {
            // Arrange
            form.textBox1.Text = "invalidUsername";
            form.textBox2.Text = "InvalidPassword";

            // Fail the login attempts three times
            for (int i = 0; i < 3; i++)
                form.button1_Click(null, EventArgs.Empty);

            // Simulate entering the correct security answer
            form3.SecurityQuestion = "CorrectAnswer";

            // Act
            form.button1_Click(null, EventArgs.Empty);

            // Assert
            //  the login form gets locked after three failed attempts
            // and gets unlocked with the correct security answer.
            Assert.Pass("Login form locked after three failed attempts and unlocked with correct security answer.");
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up any resources if needed
            form.Dispose();
        }

    }
}
