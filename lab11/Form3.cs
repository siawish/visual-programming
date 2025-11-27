using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_11
{
    public partial class Form3 : Form
    {
        private bool ShouldPaint = false;

        public Form3()
        {
            InitializeComponent();
            this.DoubleBuffered = true;  // prevents flicker
        }

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            ShouldPaint = true;
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (ShouldPaint)     // check if mouse button is being pressed
            {
                // draw a circle where the mouse pointer is present
                using (Graphics graphics = CreateGraphics())
                {
                    graphics.FillEllipse(new SolidBrush(Color.BlueViolet),
                                         e.X, e.Y, 4, 4);
                }   // end using (graphics.Dispose() is called automatically)
            }
        }

        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            ShouldPaint = false;
        }
    }
}
