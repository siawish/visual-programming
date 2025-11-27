using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Lab_11
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            label1.Text = "Mouse Clicked at " + x + " , " + y;
        }
    }
}
