using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColeProject3
{
    public partial class Form1 : Form
    {
        RadioButton[] buttonarr = new RadioButton[6];
        public Form1()
        {
            InitializeComponent();
            buttonarr[0] = radioButton1;
            buttonarr[1] = radioButton2;
            buttonarr[2] = radioButton3;
            buttonarr[3] = radioButton4;
            buttonarr[4] = radioButton5;
            buttonarr[5] = radioButton6;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void emailTexBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidEmailAddress(emailTextBox.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                emailTextBox.Select(0, emailTextBox.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(emailTextBox, errorMsg);
            }
        }

        private void emailTextBox_Validated(object sender, System.EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorProvider1.SetError(emailTextBox, "");
        }
        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            if (emailAddress.Contains("'"))
            {
                errorMessage = "email address is required";
                return false;
            }
            // Confirm that the email address string is not empty.
            if (emailAddress.Length == 0)
            {
                errorMessage = "email address is required.";
                return false;
            }

            // Confirm that there is an "@" and a "." in the email address, and in the correct order.
            if (emailAddress.IndexOf("@") > 0)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "email address must be valid email address format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }

        private void zipTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidZip(zipTextBox.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                zipTextBox.Select(0, zipTextBox.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(zipTextBox, errorMsg);
            }
        }

        private void zipTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(zipTextBox, "");
        }

        private bool ValidZip(string zip, out string errorMessage)
        {
            if (zip.Length == 0)
            {
                errorMessage = "zip code is required.";
                return false;
            }
            if (zip.Length != 5 || !isDigitsOnly(zip))
            {
                errorMessage = "Please enter a 5-digit zip code";
                return false;
            }
            else if (zip.Contains("'"))
            {
                errorMessage = "Please enter a 5-digit zip code";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }

        }

        private void stateComboBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!validState(stateComboBox.Text, out errorMessage))
            {
                e.Cancel = true;
                stateComboBox.Select(0, stateComboBox.Text.Length);
                errorProvider1.SetError(stateComboBox, errorMessage);
            }
        }

        private void stateComboBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(stateComboBox, "");
        }

        private bool validState(string state, out string errorMessage)
        {
            if (state.Length == 0)
            {
                errorMessage = "State is required";
                return false;
            }
            if (state.Length != 2)
            {
                errorMessage = "Please enter a 2-letter State abbreviation";
                return false;
            }
            else if (state.Contains("'"))
            {
                errorMessage = "Please enter a 2-letter State abbreviation";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void phoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!validPhoneNumber(phoneTextBox.Text, out errorMessage))
            {
                e.Cancel = true;
                phoneTextBox.Select(0, phoneTextBox.Text.Length);
                errorProvider1.SetError(phoneTextBox, errorMessage);

            }
        }

        private void phoneTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(phoneTextBox, "");
        }

        private bool validPhoneNumber(string phoneNumber, out string errorMessage)
        {
            if (phoneNumber.Length == 0)
            {
                errorMessage = "Please enter a phone number";
                return false;
            }

            if (phoneNumber.Length != 10 || !isDigitsOnly(phoneNumber))
            {
                errorMessage = "Please enter a 10-digit phone number with no parenthesis or dashes";
                return false;
            }
            else if (phoneNumber.Contains("'"))
            {
                errorMessage = "Please enter a 10-digit phone number with no parenthesis or dashes";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }
        private bool isDigitsOnly(String phone)
        {
            foreach (char c in phone)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (firstNameTextBox.Text.Length > 0
                && lastNameTextBox.Text.Length > 0 && phoneTextBox.Text.Length > 0 && genderComboBox.Text.Length > 0
                && addressTextBox.Text.Length > 0 && emailTextBox.Text.Length > 0 && stateComboBox.Text.Length > 0 && stateComboBox.Text.Length > 0 && cityTextBox.Text.Length > 0
                && userTextBox.Text.Length > 0 && zipTextBox.Text.Length > 0 && passwordTextBox.Text.Length > 0)
            {
                bool anyChecked = false;
                foreach (RadioButton r in buttonarr)
                {
                    if (r.Checked)
                    {
                        anyChecked = true;
                        if (CreateAccount(r))
                        {
                            var frm = new SuccessForm(firstNameTextBox.Text, lastNameTextBox.Text, userTextBox.Text, emailTextBox.Text);
                            frm.Location = this.Location;
                            frm.StartPosition = FormStartPosition.Manual;
                            frm.FormClosing += delegate { this.Show(); };
                            frm.Show();
                            this.Hide();
                        }

                    }
                }
                if(anyChecked == false)
                {
                    MessageBox.Show("Please fill in how you heard of us!");
                }
            }
            else
            {
                MessageBox.Show("Please do not leave any field blank!");
            }


        }

        private bool CreateAccount(RadioButton selectedButton)
        {
            try

            {

                String str = "ENTER CONNECTION HERE";

                String query = $"INSERT INTO[Users] VALUES('{userTextBox.Text}', '{passwordTextBox.Text}', '{firstNameTextBox.Text}', '{lastNameTextBox.Text}', '{phoneTextBox.Text}', '{genderComboBox.Text}', '{addressTextBox.Text}', '{emailTextBox.Text}', '{stateComboBox.Text}', '{cityTextBox.Text}', '{phoneTextBox.Text}', '{selectedButton.Text}')";

                SqlConnection con = new SqlConnection(str);

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

                return true;

            }

            catch (Exception e)

            {

                return false;



            }
        }

        private void userTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!ValidUser(userTextBox.Text, out errorMessage))
            {
                e.Cancel = true;
                phoneTextBox.Select(0, userTextBox.Text.Length);
                errorProvider1.SetError(userTextBox, errorMessage);
            }
        }

        private void userTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(userTextBox, "");
        }

        private bool ValidUser(string userName, out string errorMessage)
        {
            if (userName.Length > 20 || userName.Length < 6)
            {
                errorMessage = "Please enter a username(6-20 character length)";
                return false;
            }
            else if (userName.Contains("'"))
            {
                errorMessage = "Please enter a username(6-20 character length)";
                return false;
            }
            errorMessage = "";
            return true;
        }
        private void passTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!ValidPassword(passwordTextBox.Text, out errorMessage))
            {
                e.Cancel = true;
                phoneTextBox.Select(0, passwordTextBox.Text.Length);
                errorProvider1.SetError(passwordTextBox, errorMessage);
            }
        }

        private void passTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(passwordTextBox, "");
        }

        private bool ValidPassword(string password, out string errorMessage)
        {
            if (password.Length > 20 || password.Length < 6)
            {
                errorMessage = "Please enter a password(6-20 character length)";
                return false;
            }
            else if (password.Contains("'"))
            {
                errorMessage = "Please enter a password(6-20 character length)";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private void firstNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!ValidName(firstNameTextBox.Text, out errorMessage))
            {
                e.Cancel = true;
                phoneTextBox.Select(0, firstNameTextBox.Text.Length);
                errorProvider1.SetError(firstNameTextBox, errorMessage);
            }
        }

        private void firstNameTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(firstNameTextBox, "");
        }
        private void lastNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!ValidName(lastNameTextBox.Text, out errorMessage))
            {
                e.Cancel = true;
                phoneTextBox.Select(0, lastNameTextBox.Text.Length);
                errorProvider1.SetError(lastNameTextBox, errorMessage);
            }
        }

        private void lastNameTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(lastNameTextBox, "");
        }

        private void addressTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!ValidLocation(addressTextBox.Text, out errorMessage))
            {
                e.Cancel = true;
                phoneTextBox.Select(0, addressTextBox.Text.Length);
                errorProvider1.SetError(addressTextBox, errorMessage);
            }
        }

        private void addressTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(addressTextBox, "");
        }

        private void cityTextBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMessage;
            if (!ValidLocation(cityTextBox.Text, out errorMessage))
            {
                e.Cancel = true;
                phoneTextBox.Select(0, cityTextBox.Text.Length);
                errorProvider1.SetError(cityTextBox, errorMessage);
            }
        }

        private void cityTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(cityTextBox, "");
        }

        private bool ValidName(string name, out string errorMessage)
        {
            if (name.Contains("'"))
            {
                errorMessage = "Enter a valid name";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        private bool ValidLocation(string location, out string errorMessage)
        {
            if (location.Contains("'"))
            {
                errorMessage = "Enter a valid location";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }
    }
}


