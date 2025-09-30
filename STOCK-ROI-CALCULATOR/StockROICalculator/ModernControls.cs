using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StockROICalculator
{
    public class ModernButton : Button
    {
        private bool isHovered = false;
        private Color hoverColor;
        private Color normalColor;

        public ModernButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | 
                    ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Cursor = Cursors.Hand;
            normalColor = BackColor;
            hoverColor = ControlPaint.Light(BackColor, 0.2f);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = GetRoundedRectangle(rect, 8))
            {
                Color currentColor = isHovered ? hoverColor : normalColor;
                using (SolidBrush brush = new SolidBrush(currentColor))
                {
                    g.FillPath(brush, path);
                }

                // Add subtle shadow
                if (!isHovered)
                {
                    Rectangle shadowRect = new Rectangle(2, 2, Width - 1, Height - 1);
                    using (GraphicsPath shadowPath = GetRoundedRectangle(shadowRect, 8))
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                    {
                        g.FillPath(shadowBrush, shadowPath);
                    }
                }
            }

            // Draw text
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor, 
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                normalColor = value;
                hoverColor = ControlPaint.Light(value, 0.2f);
            }
        }
    }

    public class ModernTextBox : TextBox
    {
        private bool isValid = true;
        private string validationMessage = "";

        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                BackColor = isValid ? Color.White : Color.FromArgb(254, 242, 242);
                Invalidate();
            }
        }

        public string ValidationMessage
        {
            get => validationMessage;
            set => validationMessage = value;
        }

        public ModernTextBox()
        {
            BorderStyle = BorderStyle.FixedSingle;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;
        }
    }

    public class ModernComboBox : ComboBox
    {
        public ModernComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            Font = new Font("Segoe UI", 10F);
            ItemHeight = 30;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            
            Color textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected 
                ? Color.White : Color.FromArgb(30, 41, 59);
            
            using (SolidBrush brush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(Items[e.Index].ToString(), Font, brush, 
                    new PointF(e.Bounds.X + 8, e.Bounds.Y + 6));
            }
            
            e.DrawFocusRectangle();
        }
    }

    public class ModernGroupBox : GroupBox
    {
        public ModernGroupBox()
        {
            Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            ForeColor = Color.FromArgb(30, 41, 59);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 10, Width - 1, Height - 11);
            using (GraphicsPath path = GetRoundedRectangle(rect, 12))
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    g.FillPath(brush, path);
                }
                
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 2))
                {
                    g.DrawPath(pen, path);
                }
            }

            // Draw title
            SizeF textSize = g.MeasureString(Text, Font);
            Rectangle textRect = new Rectangle(15, 0, (int)textSize.Width + 10, (int)textSize.Height);
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.FillRectangle(brush, textRect);
            }
            
            using (SolidBrush brush = new SolidBrush(ForeColor))
            {
                g.DrawString(Text, Font, brush, new PointF(20, 2));
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }
}