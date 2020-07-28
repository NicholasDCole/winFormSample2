using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColeProject3
{
    public partial class SuccessForm : Form
    {
        public SuccessForm()
        {
            InitializeComponent();
        }

        public SuccessForm(string firstName, string lastName, string username, string email)
        {
            InitializeComponent();
            WebMail mail = new WebMail();
            mail.ToAddress = email;
            mail.Subject = "Account Created";
            mail.MessageBody = "Account Created";
            mail.CCAddress = "";
            mail.BccAddress = "";
            mail.FromAddress = "nc217856@reddies.hsu.edu";
            mail.Send();
            nameLabel.Text = firstName + " " + lastName;
            userLabel.Text = username;
            messageLabel.Text = $"Your account has been succesfully created and an email has been sent to {email}";
        }
    }
}
