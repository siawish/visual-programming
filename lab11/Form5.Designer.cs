namespace Lab_11
{
    partial class Form5
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customBackColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brushThicknessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem t6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem t10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem t20ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem t40ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetParametersToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customBackColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brushThicknessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.t6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.t10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.t20ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.t40ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.colorToolStripMenuItem,
                this.brushThicknessToolStripMenuItem,
                this.resetParametersToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.customColorToolStripMenuItem,
                this.customBackColorToolStripMenuItem
            });
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.colorToolStripMenuItem.Text = "Color";
            // 
            // customColorToolStripMenuItem
            // 
            this.customColorToolStripMenuItem.Name = "customColorToolStripMenuItem";
            this.customColorToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.customColorToolStripMenuItem.Text = "Custom Color";
            this.customColorToolStripMenuItem.Click += new System.EventHandler(this.customColorToolStripMenuItem_Click);
            // 
            // customBackColorToolStripMenuItem
            // 
            this.customBackColorToolStripMenuItem.Name = "customBackColorToolStripMenuItem";
            this.customBackColorToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.customBackColorToolStripMenuItem.Text = "Custom BackColor";
            this.customBackColorToolStripMenuItem.Click += new System.EventHandler(this.customBackColorToolStripMenuItem_Click);
            // 
            // brushThicknessToolStripMenuItem
            // 
            this.brushThicknessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.t6ToolStripMenuItem,
                this.t10ToolStripMenuItem,
                this.t20ToolStripMenuItem,
                this.t40ToolStripMenuItem
            });
            this.brushThicknessToolStripMenuItem.Name = "brushThicknessToolStripMenuItem";
            this.brushThicknessToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.brushThicknessToolStripMenuItem.Text = "Brush Thickness";
            // 
            // t6ToolStripMenuItem
            // 
            this.t6ToolStripMenuItem.Name = "t6ToolStripMenuItem";
            this.t6ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.t6ToolStripMenuItem.Text = "6, 6";
            this.t6ToolStripMenuItem.Click += new System.EventHandler(this.t6ToolStripMenuItem_Click);
            // 
            // t10ToolStripMenuItem
            // 
            this.t10ToolStripMenuItem.Name = "t10ToolStripMenuItem";
            this.t10ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.t10ToolStripMenuItem.Text = "10, 10";
            this.t10ToolStripMenuItem.Click += new System.EventHandler(this.t10ToolStripMenuItem_Click);
            // 
            // t20ToolStripMenuItem
            // 
            this.t20ToolStripMenuItem.Name = "t20ToolStripMenuItem";
            this.t20ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.t20ToolStripMenuItem.Text = "20, 20";
            this.t20ToolStripMenuItem.Click += new System.EventHandler(this.t20ToolStripMenuItem_Click);
            // 
            // t40ToolStripMenuItem
            // 
            this.t40ToolStripMenuItem.Name = "t40ToolStripMenuItem";
            this.t40ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.t40ToolStripMenuItem.Text = "40, 40";
            this.t40ToolStripMenuItem.Click += new System.EventHandler(this.t40ToolStripMenuItem_Click);
            // 
            // resetParametersToolStripMenuItem
            // 
            this.resetParametersToolStripMenuItem.Name = "resetParametersToolStripMenuItem";
            this.resetParametersToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.resetParametersToolStripMenuItem.Text = "Reset Parameters";
            this.resetParametersToolStripMenuItem.Click += new System.EventHandler(this.resetParametersToolStripMenuItem_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form5";
            this.Text = "Drawing";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form5_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form5_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form5_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
