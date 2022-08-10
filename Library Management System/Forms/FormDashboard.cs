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
using System.IO;

namespace Library_Management_System.Forms
{

    public partial class FormDashboard : Form
    {
       
        
        public FormDashboard()
        {
            InitializeComponent();
        }        
        private void FormDashboard_Load(object sender, EventArgs e)
        {           
            LoadTheme();
            StudentCount();
            BooksCount();
            issueCount();
            returnCount();
            BindData();
            Displaybook();
           

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
            //BookDataGridView.DataSource = dt;                   
        }                    
        private void StudentCount()
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
            conn.Open();
            string TotalStudent = "SELECT * FROM add_student";
            MySqlCommand cmd = new MySqlCommand(TotalStudent, conn);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            conn.Close();

            lblStudent.Text = ds.Tables[0].Rows.Count.ToString();
        }
        private void BooksCount()
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
            conn.Open();
            string TotalStudent = "SELECT * FROM add_Book";
            MySqlCommand cmd = new MySqlCommand(TotalStudent, conn);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            conn.Close();

            lblBooks.Text = ds.Tables[0].Rows.Count.ToString();
        }
        private void issueCount()
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
            conn.Open();
            string TotalStudent = "SELECT * FROM add_issue";
            MySqlCommand cmd = new MySqlCommand(TotalStudent, conn);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            conn.Close();

            lblIssue.Text = ds.Tables[0].Rows.Count.ToString();
        }
        private void returnCount()
        {
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
            conn.Open();
            string TotalStudent = "SELECT * FROM add_return";
            MySqlCommand cmd = new MySqlCommand(TotalStudent, conn);
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            conn.Close();

            lblReturn.Text = ds.Tables[0].Rows.Count.ToString();
        } 
        private void Displaybook()
        {      
            MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; pwd = 1994; database = dbbook");
               
            conn.Open();         
            MySqlCommand cmd = new MySqlCommand("SELECT image FROM add_book order by accession_no desc limit 6", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                MemoryStream ms1 = new MemoryStream((byte[])ds.Tables[0].Rows[0]["image"]);              
                pbBook1.Image = new Bitmap(ms1);
                MemoryStream ms2 = new MemoryStream((byte[])ds.Tables[0].Rows[1]["image"]);
                pbBook2.Image = new Bitmap(ms2);
                MemoryStream ms3 = new MemoryStream((byte[])ds.Tables[0].Rows[2]["image"]);
                pbBook3.Image = new Bitmap(ms3);
                MemoryStream ms4 = new MemoryStream((byte[])ds.Tables[0].Rows[3]["image"]);
                pbBook4.Image = new Bitmap(ms4);
                MemoryStream ms5 = new MemoryStream((byte[])ds.Tables[0].Rows[4]["image"]);
                pbBook5.Image = new Bitmap(ms5);
                MemoryStream ms6 = new MemoryStream((byte[])ds.Tables[0].Rows[5]["image"]);
                pbBook6.Image = new Bitmap(ms6);
            }                      
        }        
    }
}
