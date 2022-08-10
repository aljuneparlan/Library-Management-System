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
    public partial class FormReturn : Form
    {
        
        public FormReturn()
        {
            InitializeComponent();
            this.txtSearchReturn.Leave += new System.EventHandler(this.txtSearchReturn_Leave);
            this.txtSearchReturn.Enter += new System.EventHandler(this.txtSearchReturn_Enter);
        }
        private void FormReturn_Load_1(object sender, EventArgs e)
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
            MySqlCommand cmd = new MySqlCommand("Select * From add_return", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            returnIssueDataGridView.DataSource = dt;
        }      
        private void ClearContent()
        {
            txtIssueNo.Text = null;
            txtTittle.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            dtpDateIssue.Text = null;
            dtpDueDate.Text = null;

        }
        private void btnRetured_Click(object sender, EventArgs e)
        {
            if (txtIssueNo.Text == "")
            {
                MessageBox.Show("To return enter the issued number!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string query = "DELETE FROM add_issue WHERE issue_no='" + this.txtIssueNo.Text + "'; INSERT INTO add_return(issue_no,tittle,first_name,last_name,date_issued,due_date) VALUES('" + this.txtIssueNo.Text + "','" + this.txtTittle.Text + "','" + this.txtFirstName.Text + "','" + this.txtLastName.Text + "','" + this.dtpDateIssue.Value.ToString("yyyy-MM-dd") + "','" + this.dtpDueDate.Value.ToString("yyyy-MM-dd") + "')";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    MessageBox.Show("Book has been succesfully returned", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);               
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
        private void txtIssueNo_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
            conn.Open();

            if (txtIssueNo.Text != "")
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT tittle, first_name, last_name, date_issued, due_date FROM dbbook.add_issue WHERE issue_no=@issue_no", conn);
                    cmd.Parameters.AddWithValue("@issue_no", int.Parse(txtIssueNo.Text));
                    MySqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        txtTittle.Text = da.GetValue(0).ToString();
                        txtFirstName.Text = da.GetValue(1).ToString();
                        txtLastName.Text = da.GetValue(2).ToString();
                        dtpDateIssue.Text = da.GetValue(3).ToString();
                        dtpDueDate.Text = da.GetValue(4).ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();
                BindData();

            }
        }
        private void btnClearData_Click(object sender, EventArgs e)
        {
            if (txtDeleteData.Text == "")
            {
                MessageBox.Show("To remove enter the Issued number!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string query = "DELETE FROM add_return WHERE issue_no='" + this.txtDeleteData.Text + "'";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    MessageBox.Show("Data Has Been Successfully Deleted", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    BindData();
                    ClearContent();
                    txtDeleteData.Text = null;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void txtSearchReturn_TextChanged(object sender, EventArgs e)
        {
            string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from dbbook.add_return WHERE issue_no LIKE '" + txtSearchReturn.Text + "%'", conn);
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                returnIssueDataGridView.DataSource = dbdataset;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtSearchReturn_Enter(object sender, EventArgs e)
        {
            if (txtSearchReturn.Text == "Search by: Issue no")
            {
                txtSearchReturn.Text = "";
                txtSearchReturn.ForeColor = Color.Black;
            }
        }

        private void txtSearchReturn_Leave(object sender, EventArgs e)
        {
            if (txtSearchReturn.Text.Length == 0)
            {
                txtSearchReturn.Text = "Search by: Issue no";
                txtSearchReturn.ForeColor = Color.Silver;
                BindData();
            }
        }         
    }
}
