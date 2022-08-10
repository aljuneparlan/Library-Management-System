using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Library_Management_System.Forms
{
    
    public partial class FormAddBook : Form
    {        
        public FormAddBook()
        {
            InitializeComponent();
            this.txtSearchBook.Leave += new System.EventHandler(this.txtSearchBook_Leave);
            this.txtSearchBook.Enter += new System.EventHandler(this.txtSearchBook_Enter);          
        } 
        private void FormAddBook_Load(object sender, EventArgs e)
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
            MySqlCommand cmd = new MySqlCommand("Select * From add_book", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bookDataGridView.DataSource = dt;
        }
        private void ClearContent()
        {

            txtAccensionNo.Text = "";
            txtAuthor.Text = "";
            txtTittle.Text = "";
            txtYearPublished.Text = "";
            cbxCategory.SelectedItem = null;  
            pictureImage.Image = null;
        }
  
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (txtAccensionNo.Text == "" || txtAuthor.Text == "" || txtTittle.Text == "" || txtYearPublished.Text == "" || cbxCategory.Text == "" || pictureImage.ImageLocation == "")
            {
                MessageBox.Show("Please enter required fields!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    byte[] imageBt = null;
                    FileStream fs = new FileStream(this.txtImagePath.Text, FileMode.Open,FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageBt = br.ReadBytes((int)fs.Length);

                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string query = "INSERT INTO add_book(accession_no,tittle,author,year_published,category,image) VALUES('" + this.txtAccensionNo.Text + "','" + this.txtTittle.Text + "','" + this.txtAuthor.Text + "','" + this.txtYearPublished.Text + "','" + this.cbxCategory.Text + "',@IMG);";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr;
                    conn.Open();
                    cmd.Parameters.Add(new MySqlParameter("@IMG", imageBt));
                    dr = cmd.ExecuteReader();
                    MessageBox.Show("Book has been succesfully added", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtAccensionNo.Text == "" || txtAuthor.Text == "" || txtTittle.Text == "" || txtYearPublished.Text == "" || cbxCategory.Text == "")
            {
                MessageBox.Show("Please enter required fields!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
            else
            {
                try
                {
                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string query = "UPDATE add_book Set tittle='" + this.txtTittle.Text + "',author='" + this.txtAuthor.Text + "',year_published='" + this.txtYearPublished.Text + "',category='" + this.cbxCategory.Text + "' WHERE accession_no='" + this.txtAccensionNo.Text + "' ";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr;
                    conn.Open();                   
                    dr = cmd.ExecuteReader();
                    MessageBox.Show("Record Has Been Updated Successfully","Information!",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (txtAccensionNo.Text == "" )
            {
                MessageBox.Show("To remove enter the Accession number!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
                    string query = "DELETE FROM add_book WHERE accession_no='" + this.txtAccensionNo.Text + "'";
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr;
                    conn.Open();
                    dr = cmd.ExecuteReader();
                    MessageBox.Show("Book Has Been Successfully Deleted", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void txtSearchBook_TextChanged(object sender, EventArgs e)
        {
            string connString = "server=localhost;uid=root;pwd=1994;database=dbbook";
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from dbbook.add_book WHERE tittle LIKE '" + txtSearchBook.Text + "%'", conn);
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                bookDataGridView.DataSource = dbdataset;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtSearchBook_Enter(object sender, EventArgs e)
        {
            if (txtSearchBook.Text == "Search by: Tittle")
            {
                txtSearchBook.Text = "";
                txtSearchBook.ForeColor = Color.Black;
            }
        }

        private void txtSearchBook_Leave(object sender, EventArgs e)
        {
            if (txtSearchBook.Text.Length == 0)
            {
                txtSearchBook.Text = "Search by: Tittle";
                txtSearchBook.ForeColor = Color.Silver;
                BindData();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearContent();
        }

        private void bookDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.bookDataGridView.Rows[e.RowIndex];

                txtAccensionNo.Text = row.Cells["accession_no"].Value.ToString();
                txtAuthor.Text = row.Cells["tittle"].Value.ToString();
                txtTittle.Text = row.Cells["author"].Value.ToString();
                txtYearPublished.Text = row.Cells["year_published"].Value.ToString();
                cbxCategory.Text = row.Cells["category"].Value.ToString(); 
                
            }
        } 
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG Files (*.jpg)|*.jpg| PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string imageLocation = ofd.FileName.ToString();
                txtImagePath.Text = imageLocation;
                pictureImage.ImageLocation = imageLocation;
            }
        }
    }
}
