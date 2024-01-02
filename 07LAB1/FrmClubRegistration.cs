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
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]{
                "BS Information Technology",
                "BS Computer Science",
                "BS Information Systems",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management"
        };

            for (int i = 0; i < 6; i++)
            {
                cbProgram.Items.Add(ListOfProgram[i].ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateMember updateMemberForm = new FrmUpdateMember();
            updateMemberForm.ShowDialog();

            RefreshListOfClubMembers();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            ID = RegistrationID();
            StudentId = Convert.ToInt64(txtStudentID.Text);
            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Age = Convert.ToInt32(txtAge.Text);
            Gender = cbGender.Text;
            Program = cbProgram.Text;

            if (clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program))
            {
                txtStudentID.Clear();
                txtMiddleName.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtAge.Clear();
                cbGender.SelectedIndex = -1;
                cbProgram.SelectedIndex = -1;

                RefreshListOfClubMembers();
                MessageBox.Show("Registration successful!");
            }
            else
            {
                MessageBox.Show("Registration failed!");
            }
        }

        private long StudentId;
        public FrmClubRegistration()
        {
            InitializeComponent();

            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridList.DataSource = clubRegistrationQuery.bindingSource;
        }

        private int RegistrationID()
        {
            count++;
            return count;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }
    }
}
