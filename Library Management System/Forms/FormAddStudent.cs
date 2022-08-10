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
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace Library_Management_System.Forms
{    
    public partial class FormAddStudent : Form
    {       
        public FormAddStudent()
        {
            InitializeComponent();
            this.txtIdNumber.KeyPress += new KeyPressEventHandler(this.txtIdNumber_KeyPress);
            this.txtContactNumber.KeyPress += new KeyPressEventHandler(this.txtContactNumber_KeyPress);
            this.txtSearchStudent.Leave += new System.EventHandler(this.txtSearchStudent_Leave);
            this.txtSearchStudent.Enter += new System.EventHandler(this.txtSearchStudent_Enter);
        }              
        private void FormAddStudent_Load_1(object sender, EventArgs e)
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
            MySqlCommand cmd = new MySqlCommand("Select * From add_student", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            studentDataGridView.DataSource = dt;
        }
        private void ClearContent()
        {
            txtIdNumber.Text = null ;
            txtFirstName.Text = null ;
            txtLastName.Text = null;
            cbxCourse.Text = null ;
            cbxYearSection.Text = null ;
            txtContactNumber.Text = null;
            txtEmailAddress.Text = null ;
        }      
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (txtIdNumber.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || cbxCourse.Text == "" || cbxYearSection.Text == "" || txtContactNumber.Text == "" || txtEmailAddress.Text == "")
            {
                MessageBox.Show("Please enter required fields!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                MySqlConnection conn = new MySqlConnection(connString);

                int id = Convert.ToInt32(txtIdNumber.Text);
                string fname = txtFirstName.Text;
                string lname = txtLastName.Text;
                string course = cbxCourse.Text;
                string Section = cbxYearSection.Text;
                string contact = txtContactNumber.Text;
                string email = txtEmailAddress.Text;

                try
                {
                    string insertCommand = "INSERT INTO add_student(id, first_name, last_name, course, section, contact , email) VALUES(@id, @fname, @lname, @course, @section, @contact , @email)";
                    MySqlCommand cmd = new MySqlCommand(insertCommand, conn);

                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    cmd.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
                    cmd.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
                    cmd.Parameters.Add("@course", MySqlDbType.VarChar).Value = course;
                    cmd.Parameters.Add("@section", MySqlDbType.VarChar).Value = Section;
                    cmd.Parameters.Add("@contact", MySqlDbType.VarChar).Value = contact;
                    cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                    //Database info display in datagrindview
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM dbbook.add_student", "server=localhost;uid=root;pwd=1994;database=dbbook");
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "dbbook.add_student");
                    studentDataGridView.DataSource = ds.Tables["dbbook.add_student"].DefaultView;
                    conn.Open();

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        conn.Close();
                        MessageBox.Show("Student has been succesfully added", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                        ClearContent();
                    }
                    else
                    {
                        conn.Close();
                        MessageBox.Show("error");
                        BindData();
                        ClearContent();
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
       
           
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(txtIdNumber.Text == "" )
            {
                MessageBox.Show("To remove enter the ID number!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                                  
            else
            {
                try
                {
                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string Query = "delete from dbbook.add_student where id='" + this.txtIdNumber.Text + "';";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, conn);
                    MySqlDataReader MyReader2;
                    conn.Open();
                    MyReader2 = MyCommand2.ExecuteReader();
                    while (MyReader2.Read())
                    {
                    }
                    conn.Close();
                    BindData();
                    ClearContent();
                    MessageBox.Show("Student has been successfully deleted","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        private void studentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.studentDataGridView.Rows[e.RowIndex];

                txtIdNumber.Text = row.Cells["id"].Value.ToString();
                txtFirstName.Text = row.Cells["first_name"].Value.ToString();
                txtLastName.Text = row.Cells["last_name"].Value.ToString();
                txtContactNumber.Text = row.Cells["contact"].Value.ToString();
                txtEmailAddress.Text = row.Cells["email"].Value.ToString();
                cbxCourse.Text = row.Cells["course"].Value.ToString();
                cbxYearSection.Text = row.Cells["section"].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearContent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtIdNumber.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || cbxCourse.Text == "" || cbxYearSection.Text == "" || txtContactNumber.Text == "" || txtEmailAddress.Text == "")
            {
                MessageBox.Show("Please enter required fields!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string query = "UPDATE add_student Set id='" + this.txtIdNumber.Text + "',first_name='" + this.txtFirstName.Text + "',last_name='" + this.txtLastName.Text + "',course='" + this.cbxCourse.Text + "',section='" + this.cbxYearSection.Text + "',contact='" + this.txtContactNumber.Text + "',email='" + this.txtEmailAddress.Text + "' WHERE id='" + this.txtIdNumber.Text + "' ";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    MessageBox.Show("Record Has Been Updated Successfully", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtIdNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void txtContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void txtSearchStudent_TextChanged(object sender, EventArgs e)
        {
            string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from dbbook.add_student WHERE first_name LIKE '" + txtSearchStudent.Text + "%'", conn);
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                studentDataGridView.DataSource = dbdataset;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtSearchStudent_Enter(object sender, EventArgs e)
        {
            if (txtSearchStudent.Text == "Search by: First name")
            {
                txtSearchStudent.Text = "";
                txtSearchStudent.ForeColor = Color.Black;
            }
        }

        private void txtSearchStudent_Leave(object sender, EventArgs e)
        {
            if (txtSearchStudent.Text.Length == 0)
            {
                txtSearchStudent.Text = "Search by: First name";
                txtSearchStudent.ForeColor = Color.Silver;
                BindData();
            }
        }
    }     
}        
    

