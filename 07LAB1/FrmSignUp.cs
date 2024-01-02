using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public partial class FrmSignUp : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        public FrmSignUp()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtCreateUsername.Text;
            string password = txtCreatePassword.Text;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (clubRegistrationQuery.RegisterAccount(username, password))
                {
                    MessageBox.Show("Account created successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error", "Failed to create account. Please try a different username.");
                }
            }
            else
            {
                MessageBox.Show("Please enter both username and password.");
            }
        }
    }
}
