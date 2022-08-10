using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Library_Management_System
{
    public partial class FormLogin : Form
    {
        MySqlConnection Lgn = new MySqlConnection("server=localhost;;database=dblogin;uid=root;password=1994");
        
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Fill up the missing fields", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtUsername.Text == "admin" && txtPassword.Text == "admin")
            {
                this.Hide();
                FormDashBoard dashboard = new FormDashBoard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password!","Login Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void chkShowpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowpassword.Checked ? '\0' : '•';
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUsername.Text = "";
            chkShowpassword.Checked = false;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
