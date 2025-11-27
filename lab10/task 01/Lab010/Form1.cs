using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab010
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder cb;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Create connection using YOUR path
            con = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                  AttachDbFilename=C:\Users\Student\source\repos\Lab010\StudentDB.mdf;
                  Integrated Security=True");

            // 2. Create data adapter for Student table
            da = new SqlDataAdapter("SELECT * FROM Student", con);

            // 3. Auto-generate Insert, Update, Delete commands
            cb = new SqlCommandBuilder(da);

            // 4. Load DataSet
            ds = new DataSet();
            da.Fill(ds, "Student");

            // 5. Display table in DataGridView
            dataGridView1.DataSource = ds.Tables["Student"];
        }

        // ---------------- INSERT -------------------
        private void btnInsert_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["Student"].NewRow();

            row["Name"] = txtName.Text;
            row["Age"] = int.Parse(txtAge.Text);

            ds.Tables["Student"].Rows.Add(row);

            da.Update(ds, "Student"); // Save to DB
            MessageBox.Show("Record Added!");
        }

        // ---------------- UPDATE -------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;

                ds.Tables["Student"].Rows[index]["Name"] = txtName.Text;
                ds.Tables["Student"].Rows[index]["Age"] = int.Parse(txtAge.Text);

                da.Update(ds, "Student");
                MessageBox.Show("Record Updated!");
            }
        }

        // ---------------- DELETE -------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;

                ds.Tables["Student"].Rows[index].Delete();

                da.Update(ds, "Student");
                MessageBox.Show("Record Deleted!");
            }
        }
    }
}
