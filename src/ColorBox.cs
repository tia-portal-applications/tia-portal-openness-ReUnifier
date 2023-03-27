using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ColorHelper
{
    class ColorBox:Control
    {
        public ColorBox() : base()
        {
            SetStyles();
        }
        //Color attribute of border
        //private Color _borderColor = Color.FromArgb(65, 173, 236);
        private Color _borderColor = ColorTranslator.FromHtml("&HC0C0C0");
        [DefaultValue(typeof(Color), "65, 173, 236")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                base.Invalidate();
            }
        }

        protected override Size DefaultSize
        {
            get { return new Size(16, 16); }
        }

        private void SetStyles()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw, true);
            base.UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = ClientRectangle;
            using (SolidBrush brush = new SolidBrush(base.BackColor))
            {
                g.FillRectangle(brush,rect);
            }

            ControlPaint.DrawBorder(g, rect, _borderColor, ButtonBorderStyle.Solid);

            rect.Inflate(-1, -1);//Reduce area
            ControlPaint.DrawBorder(g,rect, Color.White,ButtonBorderStyle.Solid);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            BorderColor = Color.FromArgb(65, 173, 255);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BorderColor = ColorTranslator.FromHtml("&HC0C0C0");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ColorBox
            // 
            this.Margin = new System.Windows.Forms.Padding(2);
            this.ResumeLayout(false);

        }
    }
}
