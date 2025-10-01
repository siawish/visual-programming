using System;
using System.Windows.Forms;

namespace LabExercise5
{
    public partial class FormLoadClose : Form
    {
        private Label titleLabel;
        private Label descriptionLabel;
        private Label instructionLabel;
        private Button closeButton;

        public FormLoadClose()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.instructionLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.titleLabel.Location = new System.Drawing.Point(80, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(240, 26);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Form Events";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.descriptionLabel.Location = new System.Drawing.Point(50, 80);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(300, 17);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "This form demonstrates Form Load and Close events.";
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.instructionLabel.Location = new System.Drawing.Point(30, 120);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(340, 60);
            this.instructionLabel.TabIndex = 2;
            this.instructionLabel.Text = "• Form_Load event shows a message when form loads\r\n• FormClosing event asks for confirmation before closing\r\n• Try closing the form to see the confirmation dialog\r\n• Click 'No' to cancel closing, 'Yes' to close";
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.closeButton.Location = new System.Drawing.Point(150, 220);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 35);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close Form";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // FormLoadClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.instructionLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "FormLoadClose";
            this.Text = "Form Load and Close";
            this.Load += new System.EventHandler(this.FormLoadClose_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLoadClose_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void FormLoadClose_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Form has been loaded successfully!", "Form Load Event", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormLoadClose_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the form?", 
                                                "Confirm Close", MessageBoxButtons.YesNo, 
                                                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}