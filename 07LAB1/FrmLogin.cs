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
    public partial class FormLogin : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        public FormLogin()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (clubRegistrationQuery.ValidateLogin(username, password))
            {
                FrmClubRegistration frmClubRegistration = new FrmClubRegistration();
                MessageBox.Show("Login successful!");

                frmClubRegistration.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            FrmSignUp frmSignUp = new FrmSignUp();
            frmSignUp.Show();
        }
    }
}
