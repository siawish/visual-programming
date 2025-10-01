using System;
using System.Windows.Forms;

namespace LabExercise5
{
    public partial class HideForm : Form
    {
        private Button hideButton;
        private Button showButton;
        private Label titleLabel;
        private Label descriptionLabel;
        private Label instructionLabel;
        private Label hideInfoLabel;

        public HideForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.hideButton = new System.Windows.Forms.Button();
            this.showButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.instructionLabel = new System.Windows.Forms.Label();
            this.hideInfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.titleLabel.Location = new System.Drawing.Point(80, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(240, 26);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Hide Function";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.descriptionLabel.Location = new System.Drawing.Point(30, 60);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(340, 17);
            this.descriptionLabel.TabIndex = 1;
            this.descriptionLabel.Text = "This form demonstrates using Hide() function to hide the form.";
            // 
            // hideInfoLabel
            // 
            this.hideInfoLabel.AutoSize = true;
            this.hideInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.hideInfoLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.hideInfoLabel.Location = new System.Drawing.Point(30, 90);
            this.hideInfoLabel.Name = "hideInfoLabel";
            this.hideInfoLabel.Size = new System.Drawing.Size(340, 45);
            this.hideInfoLabel.TabIndex = 2;
            this.hideInfoLabel.Text = "• Hide() makes the form invisible but keeps it in memory\r\n• Show() makes the form visible again\r\n• The form is not destroyed, just hidden from view";
            // 
            // hideButton
            // 
            this.hideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.hideButton.Location = new System.Drawing.Point(80, 160);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(100, 35);
            this.hideButton.TabIndex = 3;
            this.hideButton.Text = "Hide Form";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // showButton
            // 
            this.showButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.showButton.Location = new System.Drawing.Point(220, 160);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(100, 35);
            this.showButton.TabIndex = 4;
            this.showButton.Text = "Show Form";
            this.showButton.UseVisualStyleBackColor = true;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic);
            this.instructionLabel.Location = new System.Drawing.Point(30, 210);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(340, 26);
            this.instructionLabel.TabIndex = 5;
            this.instructionLabel.Text = "Note: After clicking Hide, you can bring the form back by clicking\r\nShow from the taskbar or using Alt+Tab to switch between windows.";
            // 
            // HideForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.instructionLabel);
            this.Controls.Add(this.showButton);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.hideInfoLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "HideForm";
            this.Text = "Hide Form";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}