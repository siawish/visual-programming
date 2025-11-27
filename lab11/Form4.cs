using System;
using System.Windows.Forms;

namespace Lab_11
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.KeyPreview = true;   // allows form to receive key events even if controls exist
        }

        private void KeyDemo_KeyPress(object sender, KeyPressEventArgs e)
        {
            charLabel.Text = "Key pressed: " + e.KeyChar;
        }

        private void KeyDemo_KeyDown(object sender, KeyEventArgs e)
        {
            keyInfoLabel.Text =
                "Alt: " + (e.Alt ? "Yes" : "No") + "\n" +
                "Shift: " + (e.Shift ? "Yes" : "No") + "\n" +
                "Ctrl: " + (e.Control ? "Yes" : "No") + "\n" +
                "KeyCode: " + e.KeyCode + "\n" +
                "KeyData: " + e.KeyData + "\n" +
                "KeyValue: " + e.KeyValue;
        }

        private void KeyDemo_KeyUp(object sender, KeyEventArgs e)
        {
            charLabel.Text = "";
            keyInfoLabel.Text = "";
        }
    }
}
