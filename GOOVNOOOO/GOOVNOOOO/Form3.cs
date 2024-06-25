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
using static GOOVNOOOO.Form1;

namespace GOOVNOOOO
{
    public partial class Form3 : Form
    {
        private string userLogin;
        private string Role;
        public Form3(string login, string role)
        {
            InitializeComponent();
            userLogin = login;
            Role = role;
            ConfigureRoleSpecificFeatures();
            label1.Text = "Здравствуйте, " + userLogin;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.moduleTableAdapter1.Fill(this.database1DataSet1.Module);
            this.moduleTableAdapter.Fill(this.database1DataSet.Module);
            SqlConnection conn = new SqlConnection(ClassConnect.CN);
            conn.Open();
            string query = "SELECT * FROM Problems";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
                    
                
            
        }

        private void ConfigureRoleSpecificFeatures()
        {
            if (Role == "Агент")
            {
                comboBox2.Visible = true;
                button2.Visible = true;
            }

            else
            {
                comboBox2.Visible = false;
                comboBox4.Visible = false;
                button2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedUser = comboBox4.Text;
            string selectedRole = comboBox2.SelectedItem.ToString();
            SqlConnection conn = new SqlConnection(ClassConnect.CN);
            conn.Open();
            string query = "UPDATE Module SET Role = @Role WHERE Login = @Login";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@Role", SqlDbType.NVarChar).Value = selectedRole;
            cmd.Parameters.Add("@Login", SqlDbType.NVarChar).Value = selectedUser;
            cmd.ExecuteNonQuery();
            Form3_Load(sender, e);
            MessageBox.Show("Роль пользователя успешно обновлена");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string problemDescription = textBox1.Text;
            string problemType = comboBox1.Text;

            if (string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Описание проблемы не может быть пустым.");
                return;
            }

            if (string.IsNullOrEmpty(problemType))
            {
                MessageBox.Show("Пожалуйста, выберите тип проблемы.");
                return;
            }

            SqlConnection conn = new SqlConnection(ClassConnect.CN);
            conn.Open();
            string query = "INSERT INTO Problems (UserLogin, Description, ProblemType, Status, UserRole) VALUES (@UserLogin, @Description, @ProblemType, @Status, @UserRole)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@UserLogin", SqlDbType.NVarChar).Value = userLogin;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = problemDescription;
            cmd.Parameters.Add("@ProblemType", SqlDbType.NVarChar).Value = problemType;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "В ожидании";
            cmd.Parameters.Add("@UserRole", SqlDbType.NVarChar).Value = Role;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Проблема успешно отправлена");
            Form3_Load(sender, e);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите заявку для обновления статуса.");
                return;
            }

            string selectedProblemId = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();
            string selectedStatus = comboBox3.Text;

            SqlConnection conn = new SqlConnection(ClassConnect.CN);
            conn.Open();
            string query = "UPDATE Problems SET Status = @Status WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, conn);          
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = selectedStatus;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(selectedProblemId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Статус заявки успешно обновлен");
            Form3_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 userForm = new Form1();
            userForm.Show();
            this.Close();
        }
    }
    
   
}
