using System;
using System.Windows.Forms;

namespace LabExercise5
{
    public partial class TableCalculator : Form
    {
        private TextBox numberTextBox;
        private Label numberLabel;
        private Button computeButton;
        private ListBox tableListBox;

        public TableCalculator()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.numberLabel = new System.Windows.Forms.Label();
            this.computeButton = new System.Windows.Forms.Button();
            this.tableListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Location = new System.Drawing.Point(30, 30);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(58, 17);
            this.numberLabel.TabIndex = 0;
            this.numberLabel.Text = "Number";
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point(100, 27);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(100, 22);
            this.numberTextBox.TabIndex = 1;
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
            // tableListBox
            // 
            this.tableListBox.FormattingEnabled = true;
            this.tableListBox.ItemHeight = 16;
            this.tableListBox.Location = new System.Drawing.Point(30, 70);
            this.tableListBox.Name = "tableListBox";
            this.tableListBox.Size = new System.Drawing.Size(270, 260);
            this.tableListBox.TabIndex = 3;
            // 
            // TableCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 350);
            this.Controls.Add(this.tableListBox);
            this.Controls.Add(this.computeButton);
            this.Controls.Add(this.numberTextBox);
            this.Controls.Add(this.numberLabel);
            this.Name = "TableCalculator";
            this.Text = "Table Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void computeButton_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(numberTextBox.Text);
                tableListBox.Items.Clear();

                for (int i = 1; i <= 10; i++)
                {
                    int result = number * i;
                    tableListBox.Items.Add($"{number} x {i} = {result}");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid integer number.", "Input Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}