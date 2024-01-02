using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClubRegistration
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        public DataTable dataTable;
        public BindingSource bindingSource;

        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\emjix\\OneDrive\\Documents\\ClubDB.mdf;Integrated Security=True;Connect Timeout=30";
        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        int _Age;

        public ClubRegistrationQuery()
        {
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            try
            {
                string viewClubMembers = "SELECT StudentId, FirstName, LastName, Age, Gender, Program FROM [dbo].[Table]";

                sqlAdapter = new SqlDataAdapter(viewClubMembers, sqlConnect);
                dataTable.Clear();
                sqlAdapter.Fill(dataTable);
                bindingSource.DataSource = dataTable;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: ", ex.Message);
                return false;
            }
        }
        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(connectionString))
                {
                    sqlConnect.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO [Table] VALUES(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect))
                    {
                        sqlCommand.Parameters.AddWithValue("@ID", ID);
                        sqlCommand.Parameters.AddWithValue("@StudentID", StudentID);
                        sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
                        sqlCommand.Parameters.AddWithValue("@MiddleName", MiddleName);
                        sqlCommand.Parameters.AddWithValue("@LastName", LastName);
                        sqlCommand.Parameters.AddWithValue("@Age", Age);
                        sqlCommand.Parameters.AddWithValue("@Gender", Gender);
                        sqlCommand.Parameters.AddWithValue("@Program", Program);

                        sqlCommand.ExecuteNonQuery();
                    } 
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
        public bool RegisterAccount(string username, string password)
        {
            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(connectionString))
                {
                    sqlConnect.Open();

                    string checkExistingUserQuery = "SELECT COUNT(*) FROM AccountTable WHERE Username = @Username";
                    using (SqlCommand checkUserCommand = new SqlCommand(checkExistingUserQuery, sqlConnect))
                    {
                        checkUserCommand.Parameters.AddWithValue("@Username", username);
                        int existingUserCount = (int)checkUserCommand.ExecuteScalar();

                        if (existingUserCount > 0)
                        {

                            MessageBox.Show("Username already exists. Please choose a different username.");
                            return false;
                        }
                    }

                    string registerQuery = "INSERT INTO AccountTable (Username, Password) VALUES (@Username, @Password)";
                    using (SqlCommand sqlCommand = new SqlCommand(registerQuery, sqlConnect))
                    {
                        sqlCommand.Parameters.AddWithValue("@Username", username);
                        sqlCommand.Parameters.AddWithValue("@Password", password);

                        sqlCommand.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(connectionString))
                {
                    sqlConnect.Open();

                    string query = "SELECT COUNT(*) FROM AccountTable WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnect))
                    {
                        sqlCommand.Parameters.AddWithValue("@Username", username);
                        sqlCommand.Parameters.AddWithValue("@Password", password);

                        int count = (int)sqlCommand.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        public bool UpdateStudentData(long studentId, string firstName, string middleName, string lastName, int age, string gender, string program)
        {
            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(connectionString))
                {
                    sqlConnect.Open();

                    string query = "UPDATE [Table] SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId = @StudentId";

                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnect))
                    {
                        sqlCommand.Parameters.AddWithValue("@StudentId", studentId);
                        sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                        sqlCommand.Parameters.AddWithValue("@MiddleName", middleName);
                        sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                        sqlCommand.Parameters.AddWithValue("@Age", age);
                        sqlCommand.Parameters.AddWithValue("@Gender", gender);
                        sqlCommand.Parameters.AddWithValue("@Program", program);

                        sqlCommand.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
        public DataRow GetStudentData(long studentId)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectionString))
            {
                sqlConnect.Open();

                string query = "SELECT * FROM [Table] WHERE StudentId = @StudentId";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnect))
                {
                    sqlCommand.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dataTable = new DataTable();
                        sqlAdapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            return dataTable.Rows[0];
                        }
                    }
                }
            }

            return null;
        }
        public DataTable GetStudentIds()
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectionString))
            {
                sqlConnect.Open();

                string query = "SELECT StudentId FROM [Table]";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnect))
                {
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dataTable = new DataTable();
                        sqlAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

    }
}
