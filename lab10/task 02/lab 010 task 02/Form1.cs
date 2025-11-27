using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab_010_task_02
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Database connection (modify path if needed)
            con = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Student\source\repos\lab 010 task 02\School.mdf"";Integrated Security=True");

            LoadAllData();
        }

        // ---------------- DISPLAY ALL JOINED DATA ----------------------
        private void LoadAllData()
        {
            string query = @"
                SELECT 
                    S.StudentID,
                    S.FirstName,
                    S.LastName,
                    S.Major,
                    D.DeptName AS Department,
                    D.DeptChair AS [Dept. Chair]
                FROM Student S
                INNER JOIN Department D
                ON S.DeptID = D.DeptID";

            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        // ---------------- RETRIEVE BY LAST NAME (PARAMETERIZED) ----------------------
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            string filterQuery = @"
                SELECT 
                    S.StudentID,
                    S.FirstName,
                    S.LastName,
                    S.Major,
                    D.DeptName AS Department,
                    D.DeptChair AS [Dept. Chair]
                FROM Student S
                INNER JOIN Department D
                ON S.DeptID = D.DeptID
                WHERE S.LastName = @LastName";

            SqlCommand cmd = new SqlCommand(filterQuery, con);
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);

            SqlDataAdapter filterAdapter = new SqlDataAdapter(cmd);
            DataTable filterTable = new DataTable();
            filterAdapter.Fill(filterTable);

            dataGridView1.DataSource = filterTable;
        }
    }
}
