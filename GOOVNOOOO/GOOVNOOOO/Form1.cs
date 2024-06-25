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

namespace GOOVNOOOO
{
    public partial class Form1 : Form
    {

        public class ClassConnect
        {
            public const string CN = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\iluxa\\source\\repos\\GOOVNOOOO\\GOOVNOOOO\\Database1.mdf;Integrated Security=True";
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string role = who(login, password);
            if (role == "Агент")
            {
                MessageBox.Show("Вы агент");

            }
            else if (role == "Пользователь")
            {
                UserForm userForm = new UserForm();
                userForm.Show();
                this.Hide();
            }
            else if (role == null)
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите данные");
                }
                else
                {
                    MessageBox.Show("Пользователь не найден");
                }
            }
        }

        private string who(string login, string password)
        {
            SqlConnection conn = new SqlConnection(ClassConnect.CN);
            conn.Open();
            string query = "SELECT Role FROM Module WHERE Login = @Login AND Password = @Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                return reader["Role"].ToString();
            else return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 addUserForm = new Form2();
            addUserForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
            
        
    




