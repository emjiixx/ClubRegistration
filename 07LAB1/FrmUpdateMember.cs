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
    public partial class FrmUpdateMember : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private long selectedStudentId;
        public FrmUpdateMember()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            LoadStudentIDs();

            if (cbStudentID.Items.Count > 0)
            {
                cbStudentID.SelectedIndex = 0;
                selectedStudentId = Convert.ToInt64(cbStudentID.SelectedItem);
                LoadStudentData(selectedStudentId);
            }
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
                cbUpdateProgram.Items.Add(ListOfProgram[i].ToString());
            }
            cbStudentID.SelectedIndexChanged += cbStudentID_SelectedIndexChanged;
        }
        private void LoadStudentIDs()
        {
            DataTable studentIdDataTable = clubRegistrationQuery.GetStudentIds();
            foreach (DataRow row in studentIdDataTable.Rows)
            {
                cbStudentID.Items.Add(row["StudentId"].ToString());
            }
        }

        private void LoadStudentData(long studentId)
        {
            DataRow studentData = clubRegistrationQuery.GetStudentData(studentId);

            if (studentData != null)
            {
                txtUpdateFirstName.Text = studentData["FirstName"].ToString();
                txtUpdateMiddleName.Text = studentData["MiddleName"].ToString();
                txtUpdateLastName.Text = studentData["LastName"].ToString();
                txtUpdateAge.Text = studentData["Age"].ToString();
                cbUpdateGender.Text = studentData["Gender"].ToString();
                cbUpdateProgram.Text = studentData["Program"].ToString();
            }
        }

        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStudentID.SelectedIndex >= 0)
            {
                selectedStudentId = Convert.ToInt64(cbStudentID.SelectedItem);
                LoadStudentData(selectedStudentId);
            }
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            UpdateStudentData(selectedStudentId);
        }

        private void UpdateStudentData(long studentId)
        {
            string firstName = txtUpdateFirstName.Text;
            string middleName = txtUpdateMiddleName.Text;
            string lastName = txtUpdateLastName.Text;
            int age = Convert.ToInt32(txtUpdateAge.Text);
            string gender = cbUpdateGender.Text;
            string program = cbUpdateProgram.Text;

            if (clubRegistrationQuery.UpdateStudentData(studentId, firstName, middleName, lastName, age, gender, program))
            {
                MessageBox.Show("Update successful!");

                cbStudentID.SelectedIndex = -1;
                txtUpdateFirstName.Clear();
                txtUpdateMiddleName.Clear();
                txtUpdateLastName.Clear();
                txtUpdateAge.Clear();
                cbUpdateGender.SelectedIndex = -1;
                cbUpdateProgram.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Update failed!");
            }
        }
    }
}
