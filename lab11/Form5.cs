using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_11
{
    public partial class Form5 : Form
    {
        bool ShouldPaint = false;

        // Default sizes & colors
        int thick1 = 4;
        int thick2 = 4;
        Color front = Color.Aquamarine;

        public Form5()
        {
            InitializeComponent();
            this.DoubleBuffered = true;     // smoother drawing
        }

        private void Form5_MouseDown(object sender, MouseEventArgs e)
        {
            ShouldPaint = true;
        }

        private void Form5_MouseMove(object sender, MouseEventArgs e)
        {
            if (ShouldPaint)
            {
                using (Graphics graphics = CreateGraphics())
                {
                    graphics.FillEllipse(new SolidBrush(front),
                                         e.X, e.Y,
                                         thick1, thick2);
                }
            }
        }

        private void Form5_MouseUp(object sender, MouseEventArgs e)
        {
            ShouldPaint = false;
        }

        // ============================
        // MENU: COLOR -> CUSTOM COLOR
        // ============================
        private void customColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                front = colorDialog1.Color;   // Brush color
            }
        }

        // =====================================
        // MENU: COLOR -> CUSTOM BACKGROUND COLOR
        // =====================================
        private void customBackColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
            }
        }

        // ============================
        // MENU: BRUSH THICKNESS
        // ============================
        private void t6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thick1 = 6; thick2 = 6;
        }

        private void t10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thick1 = 10; thick2 = 10;
        }

        private void t20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thick1 = 20; thick2 = 20;
        }

        private void t40ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thick1 = 40; thick2 = 40;
        }

        // ============================
        // MENU: RESET PARAMETERS
        // ============================
        private void resetParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thick1 = 4;
            thick2 = 4;
            front = Color.Aquamarine;
            this.BackColor = Color.White;
        }
    }
}
