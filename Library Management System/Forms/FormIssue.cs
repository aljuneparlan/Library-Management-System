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

namespace Library_Management_System.Forms
{
    public partial class FormIssue : Form
    {
        public FormIssue()
        {
            InitializeComponent();
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
        }
        private void FormIssue_Load(object sender, EventArgs e)
        {
            LoadTheme();
            BindData();
        }
        private void LoadTheme()

        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimarColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }
        private void BindData()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;uid=root;pwd=1994;database=dbbook");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From add_issue", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            issueDataGridView.DataSource = dt;
        }
        private void ClearContent()
        {
            txtCategory.Text = null;
            txtSection.Text = null;
            txtCourse.Text = null;
            txtContact.Text = null;
            txtEmail.Text = null;
            txtissueNo.Text = null;
            txtAccessionNo.Text = null;
            txtIdNumber.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;                     
            txtAuthor.Text = null;
            txtTittle.Text = null;
            txtYearPublished.Text = null;
           
        }

        private void txtIdNumber_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
            conn.Open();
            
            if (txtIdNumber.Text != "")
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT first_name, last_name, course, section, contact , email  FROM dbbook.add_student WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", int.Parse(txtIdNumber.Text));
                    MySqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        txtFirstName.Text = da.GetValue(0).ToString();
                        txtLastName.Text = da.GetValue(1).ToString();
                        txtCourse.Text = da.GetValue(2).ToString();
                        txtSection.Text = da.GetValue(3).ToString();
                        txtContact.Text = da.GetValue(4).ToString();
                        txtEmail.Text = da.GetValue(5).ToString();
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();

            }
        }
       
        private void txtAccessionNo_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
            conn.Open();

            if (txtAccessionNo.Text != "")
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT tittle, author, year_published, category  FROM dbbook.add_book WHERE accession_no=@accessionno", conn);
                    cmd.Parameters.AddWithValue("@accessionno", int.Parse(txtAccessionNo.Text));
                    MySqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        txtTittle.Text = da.GetValue(0).ToString();
                        txtAuthor.Text = da.GetValue(1).ToString();
                        txtYearPublished.Text = da.GetValue(2).ToString();
                        txtCategory.Text = da.GetValue(3).ToString();                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();

            }
        }                        
        private void btnIssued_Click(object sender, EventArgs e)
        {  
            if(txtissueNo.Text == "" || txtIdNumber.Text == "" || txtAccessionNo.Text == "")
            {
                MessageBox.Show("Please enter required fields!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string query = "INSERT INTO add_issue(issue_no,tittle,first_name,last_name,date_issued,due_date) VALUES('" + this.txtissueNo.Text + "','" + this.txtTittle.Text + "','" + this.txtFirstName.Text + "','" + this.txtLastName.Text + "','" + this.dtpDateIssue.Value.ToString("yyyy-MM-dd") + "','" + this.dtpDueDate.Value.ToString("yyyy-MM-dd") + "')";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    MessageBox.Show("Book has been succesfully issued", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    BindData();
                    ClearContent();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }  
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from dbbook.add_issue WHERE first_name LIKE '" + txtSearch.Text + "%'", conn);
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                issueDataGridView.DataSource = dbdataset;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search by: First name")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                txtSearch.Text = "Search by: First name";
                txtSearch.ForeColor = Color.Silver;
                BindData();
            }
        }
    }
}
