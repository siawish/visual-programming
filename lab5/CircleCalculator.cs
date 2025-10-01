using System;
using System.Windows.Forms;

namespace LabExercise5
{
    public partial class CircleCalculator : Form
    {
        private TextBox radiusTextBox;
        private Label radiusLabel;
        private Label areaLabel;
        private Label circumferenceLabel;
        private Button computeButton;

        public CircleCalculator()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.radiusTextBox = new System.Windows.Forms.TextBox();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.areaLabel = new System.Windows.Forms.Label();
            this.circumferenceLabel = new System.Windows.Forms.Label();
            this.computeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(30, 30);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(50, 17);
            this.radiusLabel.TabIndex = 0;
            this.radiusLabel.Text = "Radius";
            // 
            // radiusTextBox
            // 
            this.radiusTextBox.Location = new System.Drawing.Point(100, 27);
            this.radiusTextBox.Name = "radiusTextBox";
            this.radiusTextBox.Size = new System.Drawing.Size(100, 22);
            this.radiusTextBox.TabIndex = 1;
            // 
            // computeButton
            // 
            this.computeButton.Location = new System.Drawing.Point(220, 25);
            this.computeButton.Name = "computeButton";
            this.computeButton.Size = new System.Drawing.Size(80, 25);
            this.computeButton.TabIndex = 2;
            this.computeButton.Text = "Compute";
            this.computeButton.UseVisualStyleBackColor = true;
            this.computeButton.Click += new System.EventHandler(this.computeButton_Click);
            // 
            // areaLabel
            // 
            this.areaLabel.AutoSize = true;
            this.areaLabel.Location = new System.Drawing.Point(30, 80);
            this.areaLabel.Name = "areaLabel";
            this.areaLabel.Size = new System.Drawing.Size(200, 17);
            this.areaLabel.TabIndex = 3;
            this.areaLabel.Text = "The area of a circle is=";
            // 
            // circumferenceLabel
            // 
            this.circumferenceLabel.AutoSize = true;
            this.circumferenceLabel.Location = new System.Drawing.Point(30, 110);
            this.circumferenceLabel.Name = "circumferenceLabel";
            this.circumferenceLabel.Size = new System.Drawing.Size(200, 17);
            this.circumferenceLabel.TabIndex = 4;
            this.circumferenceLabel.Text = "The circumference of a circle is=";
            // 
            // CircleCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.circumferenceLabel);
            this.Controls.Add(this.areaLabel);
            this.Controls.Add(this.computeButton);
            this.Controls.Add(this.radiusTextBox);
            this.Controls.Add(this.radiusLabel);
            this.Name = "CircleCalculator";
            this.Text = "Circle Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void computeButton_Click(object sender, EventArgs e)
        {
            try
            {
                double radius = Convert.ToDouble(radiusTextBox.Text);
                double area = Math.PI * radius * radius;
                double circumference = 2 * Math.PI * radius;

                areaLabel.Text = $"The area of a circle is={area:F2}";
                circumferenceLabel.Text = $"The circumference of a circle is={circumference:F2}";
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid number for radius.", "Input Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}