namespace Lab_11
{
    partial class Form4
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label charLabel;
        private System.Windows.Forms.Label keyInfoLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.charLabel = new System.Windows.Forms.Label();
            this.keyInfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // charLabel
            // 
            this.charLabel.AutoSize = true;
            this.charLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.charLabel.Location = new System.Drawing.Point(25, 20);
            this.charLabel.Name = "charLabel";
            this.charLabel.Size = new System.Drawing.Size(107, 21);
            this.charLabel.TabIndex = 0;
            this.charLabel.Text = "Key pressed: ";
            // 
            // keyInfoLabel
            // 
            this.keyInfoLabel.AutoSize = true;
            this.keyInfoLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.keyInfoLabel.Location = new System.Drawing.Point(25, 60);
            this.keyInfoLabel.Name = "keyInfoLabel";
            this.keyInfoLabel.Size = new System.Drawing.Size(0, 20);
            this.keyInfoLabel.TabIndex = 1;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.keyInfoLabel);
            this.Controls.Add(this.charLabel);
            this.Name = "Form4";
            this.Text = "Key Demo";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDemo_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyDemo_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyDemo_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
