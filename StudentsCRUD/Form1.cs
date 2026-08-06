using StudentsCRUD.DataBaseConnection;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace StudentsCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();

        }
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            LoadStudents();
           
        }
        private void LoadStudents()
        {
            using (OleDbConnection conn = DbConnection.GetDbConnection())
            {
                conn.Open();
                string sql = "select * from [students]";

                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                studentGripView.DataSource = dt;
            }
        }
       
    }
}
