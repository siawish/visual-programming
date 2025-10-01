using System;
using System.Windows.Forms;

namespace LabExercise5
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Create a main menu form to select exercises
            Application.Run(new MainMenuForm());
        }
    }

    public partial class MainMenuForm : Form
    {
        private Button exercise1Button;
        private Button exercise2Button;
        private Button exercise3Button;
        private Button exercise4Button;
        private Button exercise5Button;
        private Label titleLabel;

        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.exercise1Button = new System.Windows.Forms.Button();
            this.exercise2Button = new System.Windows.Forms.Button();
            this.exercise3Button = new System.Windows.Forms.Button();
            this.exercise4Button = new System.Windows.Forms.Button();
            this.exercise5Button = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(100, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(200, 26);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Lab Exercise 5 Menu";
            // 
            // exercise1Button
            // 
            this.exercise1Button.Location = new System.Drawing.Point(150, 80);
            this.exercise1Button.Name = "exercise1Button";
            this.exercise1Button.Size = new System.Drawing.Size(200, 30);
            this.exercise1Button.TabIndex = 1;
            this.exercise1Button.Text = "Exercise 1: Form Load/Close";
            this.exercise1Button.UseVisualStyleBackColor = true;
            this.exercise1Button.Click += new System.EventHandler(this.exercise1Button_Click);
            // 
            // exercise2Button
            // 
            this.exercise2Button.Location = new System.Drawing.Point(150, 120);
            this.exercise2Button.Name = "exercise2Button";
            this.exercise2Button.Size = new System.Drawing.Size(200, 30);
            this.exercise2Button.TabIndex = 2;
            this.exercise2Button.Text = "Exercise 2: Hide Form";
            this.exercise2Button.UseVisualStyleBackColor = true;
            this.exercise2Button.Click += new System.EventHandler(this.exercise2Button_Click);
            // 
            // exercise3Button
            // 
            this.exercise3Button.Location = new System.Drawing.Point(150, 160);
            this.exercise3Button.Name = "exercise3Button";
            this.exercise3Button.Size = new System.Drawing.Size(200, 30);
            this.exercise3Button.TabIndex = 3;
            this.exercise3Button.Text = "Exercise 3: Circle Calculator";
            this.exercise3Button.UseVisualStyleBackColor = true;
            this.exercise3Button.Click += new System.EventHandler(this.exercise3Button_Click);
            // 
            // exercise4Button
            // 
            this.exercise4Button.Location = new System.Drawing.Point(150, 200);
            this.exercise4Button.Name = "exercise4Button";
            this.exercise4Button.Size = new System.Drawing.Size(200, 30);
            this.exercise4Button.TabIndex = 4;
            this.exercise4Button.Text = "Exercise 4: Table Calculator";
            this.exercise4Button.UseVisualStyleBackColor = true;
            this.exercise4Button.Click += new System.EventHandler(this.exercise4Button_Click);
            // 
            // exercise5Button
            // 
            this.exercise5Button.Location = new System.Drawing.Point(150, 240);
            this.exercise5Button.Name = "exercise5Button";
            this.exercise5Button.Size = new System.Drawing.Size(200, 30);
            this.exercise5Button.TabIndex = 5;
            this.exercise5Button.Text = "Exercise 5: Temperature Converter";
            this.exercise5Button.UseVisualStyleBackColor = true;
            this.exercise5Button.Click += new System.EventHandler(this.exercise5Button_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 320);
            this.Controls.Add(this.exercise5Button);
            this.Controls.Add(this.exercise4Button);
            this.Controls.Add(this.exercise3Button);
            this.Controls.Add(this.exercise2Button);
            this.Controls.Add(this.exercise1Button);
            this.Controls.Add(this.titleLabel);
            this.Name = "MainMenuForm";
            this.Text = "Lab Exercise 5 - Visual Programming";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void exercise1Button_Click(object sender, EventArgs e)
        {
            FormLoadClose form1 = new FormLoadClose();
            form1.Show();
        }

        private void exercise2Button_Click(object sender, EventArgs e)
        {
            HideForm form2 = new HideForm();
            form2.Show();
        }

        private void exercise3Button_Click(object sender, EventArgs e)
        {
            CircleCalculator form3 = new CircleCalculator();
            form3.Show();
        }

        private void exercise4Button_Click(object sender, EventArgs e)
        {
            TableCalculator form4 = new TableCalculator();
            form4.Show();
        }

        private void exercise5Button_Click(object sender, EventArgs e)
        {
            TemperatureConverter form5 = new TemperatureConverter();
            form5.Show();
        }
    }
}