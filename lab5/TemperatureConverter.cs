using System;
using System.Windows.Forms;

namespace LabExercise5
{
    public partial class TemperatureConverter : Form
    {
        private TextBox fahrenheitTextBox;
        private Label fahrenheitLabel;
        private Label celsiusLabel;
        private Button convertButton;

        public TemperatureConverter()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.fahrenheitTextBox = new System.Windows.Forms.TextBox();
            this.fahrenheitLabel = new System.Windows.Forms.Label();
            this.celsiusLabel = new System.Windows.Forms.Label();
            this.convertButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fahrenheitLabel
            // 
            this.fahrenheitLabel.AutoSize = true;
            this.fahrenheitLabel.Location = new System.Drawing.Point(30, 30);
            this.fahrenheitLabel.Name = "fahrenheitLabel";
            this.fahrenheitLabel.Size = new System.Drawing.Size(150, 17);
            this.fahrenheitLabel.TabIndex = 0;
            this.fahrenheitLabel.Text = "Temperature in Fahrenheit";
            // 
            // fahrenheitTextBox
            // 
            this.fahrenheitTextBox.Location = new System.Drawing.Point(200, 27);
            this.fahrenheitTextBox.Name = "fahrenheitTextBox";
            this.fahrenheitTextBox.Size = new System.Drawing.Size(100, 22);
            this.fahrenheitTextBox.TabIndex = 1;
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(320, 25);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(80, 25);
            this.convertButton.TabIndex = 2;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // celsiusLabel
            // 
            this.celsiusLabel.AutoSize = true;
            this.celsiusLabel.Location = new System.Drawing.Point(30, 80);
            this.celsiusLabel.Name = "celsiusLabel";
            this.celsiusLabel.Size = new System.Drawing.Size(200, 17);
            this.celsiusLabel.TabIndex = 3;
            this.celsiusLabel.Text = "The temperature in Celsius is: ";
            // 
            // TemperatureConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 150);
            this.Controls.Add(this.celsiusLabel);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.fahrenheitTextBox);
            this.Controls.Add(this.fahrenheitLabel);
            this.Name = "TemperatureConverter";
            this.Text = "Temperature Converter";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            try
            {
                double fahrenheit = Convert.ToDouble(fahrenheitTextBox.Text);
                double celsius = (fahrenheit - 32) * (5.0 / 9.0);
                
                celsiusLabel.Text = $"The temperature in Celsius is: {celsius:F2}";
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid temperature in Fahrenheit.", "Input Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}