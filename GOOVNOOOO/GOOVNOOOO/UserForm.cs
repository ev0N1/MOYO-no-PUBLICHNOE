using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GOOVNOOOO.Form1;

namespace GOOVNOOOO
{
    public partial class UserForm : Form
    {
        private string User;
        private string UserRole;
        public UserForm(string login, string role)
        {
            InitializeComponent();
            User = login;
            label1.Text = "Здравствуйте, " + User;
            UserRole = role;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string problemType = comboBox1.SelectedItem.ToString();
            string Problems = textBox1.Text;
            SqlConnection conn = new SqlConnection(ClassConnect.CN);
            conn.Open();
            string query = "INSERT INTO [Problems](UserLogin, Description, Status, ProblemType, UserRole) VALUES (@UserLogin, @Description, @Status, @ProblemType, @UserRole)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserLogin", User);
            cmd.Parameters.AddWithValue("@Description", Problems);
            cmd.Parameters.AddWithValue("@Status", "В ожидании");
            cmd.Parameters.AddWithValue("@ProblemType", problemType);
            cmd.Parameters.AddWithValue("@UserRole", UserRole);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Заявка создана");
            dataGridView1.Refresh();
            UserForm_Load(sender, e);

        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ClassConnect.CN);
            conn.Open();
            string query = "SELECT * FROM Problems WHERE UserLogin = @Login AND UserRole = @UserRole";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Login", User);
            cmd.Parameters.AddWithValue("@UserRole", UserRole);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 userForm = new Form1();
            userForm.Show();
            this.Close();
        }
    }
}
