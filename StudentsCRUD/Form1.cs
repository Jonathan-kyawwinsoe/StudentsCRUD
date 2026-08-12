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
            txtId.ReadOnly = true;
            LoadStudents();
        }
       
        private void btnCreate_Click(object sender, EventArgs e)
        {
            txtName.Focus();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            EditStudent();
        }
        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            NewCreate();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // data table ကို grip ထဲ ပြခြင်း
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
        
        // ီData create 
        private void NewCreate()
        {
            using (OleDbConnection conn = DbConnection.GetDbConnection())
            {
                conn.Open();
                string sql = "insert into students " +
                    "([StudentName],[FatherName],[DOB],[Class],[RollNumber],[Address])" +
                    "values(?,?,?,?,?,?) ";

                OleDbCommand cmd = new OleDbCommand(sql, conn);

                cmd.Parameters.AddWithValue("@StudentName", txtName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@Class", txtClass.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("ှSystem ထဲ သို့ အောင်မြင်စွာ ထည့်သ္ငင်းပြီးပါပြီး");

                    LoadStudents();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Create Fail");
                }

                
            }
        }
        
        // data clear 
        private void ClearForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtFatherName.Clear();
            txtDOB.Clear();
            txtClass.Clear();
            txtRollNumber.Clear();
            txtAddress.Clear();
            txtFatherName.Clear();

            txtName.Focus();

        }
        
        // Data grip view မှ data ကို collect လုပ်ခြင်း
        private void studentGripView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            LoadStudentToText(e.RowIndex);

        }
        
        private void LoadStudentToText(int rowIndex)
        {
            DataGridViewRow row = studentGripView.Rows[rowIndex];

            txtId.Text = row.Cells["StudentId"].Value?.ToString();
            txtName.Text = row.Cells["StudentName"].Value?.ToString();
            txtFatherName.Text = row.Cells["FatherName"].Value?.ToString();
            txtDOB.Text = row.Cells["DOB"].Value?.ToString();
            txtRollNumber.Text = row.Cells["RollNumber"].Value.ToString();
            txtClass.Text = row.Cells["Class"].Value?.ToString();
            txtAddress.Text = row.Cells["Address"].Value?.ToString();
        }
        
        // data edit လုပ်ခြင်း
        private void EditStudent()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Data ရွေးချယ်ရန်လိုအပ်ပါသည်");
                return;
            }

            using (OleDbConnection edit = DbConnection.GetDbConnection())
            {
                edit.Open();

                string sql = @"update students
                                set [StudentName] = ?,
                                    [FatherName] = ?,
                                    [DOB] = ?,
                                    [RollNumber] = ?,
                                    [Class] = ?,
                                    [Address] = ? where [StudentId] = ?";
                using (OleDbCommand command = new OleDbCommand(sql, edit))
                {
                    command.Parameters.AddWithValue("@StudentName", txtName.Text);
                    command.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                    command.Parameters.AddWithValue("@DOB", txtDOB.Text);
                    command.Parameters.AddWithValue("@RollNumber", txtRollNumber.Text);
                    command.Parameters.AddWithValue("@Class", txtClass.Text);
                    command.Parameters.AddWithValue("@Address", txtAddress.Text);
                    command.Parameters.AddWithValue("@StudentId", txtId.Text);


                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Student Update Data");
                        LoadStudents();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Update Fail");
                    }
                }
            }

        }

        // delete Student 
        private void DeleteStudent()
        {
            if(string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("You need to delete select Data");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure to delete this student", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
            if(result != DialogResult.Yes)
            {
                return;
            }

            using (OleDbConnection delete = DbConnection.GetDbConnection())
            {
                delete.Open();

                string sql = @"delete from students where [studentId] = ?";

                using (OleDbCommand del = new OleDbCommand(sql, delete))
                {
                    del.Parameters.AddWithValue(@"studentId",Convert.ToInt32(txtId.Text));

                    int deleterow = del.ExecuteNonQuery();

                    if(deleterow > 0)
                    {
                        MessageBox.Show("Delete is successfully");
                        LoadStudents(); 
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Delete fail");
                    }
                }
            }

        }

    }
}
