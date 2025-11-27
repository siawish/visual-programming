using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Lab_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Apply styling ONLY — do not change the label text
            label1.Font = new Font("Verdana", 20, FontStyle.Regular);
            label1.ForeColor = Color.Turquoise;
            label1.BackColor = Color.White;

            // Optional: close form with X
            if (e.KeyCode == Keys.X)
                this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
