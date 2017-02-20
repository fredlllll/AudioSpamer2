using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AudioSpamer2
{
    public class TrackBarWithAB : TrackBar
    {
        int a=0, b=0;
        public int A {
            get { return a; }
            set { if (value > b) { a = b; b = value; } else { a = value; } }
        }

        public int B
        {
            get { return b; }
            set { if (value < a) { b = a; a = value; } else { b = value; } }
        }

        public new int Maximum
        {
            get { return base.Maximum; }
            set { base.Maximum = value; B = value; if (value < A) { A = value; } }
        }

        public TrackBarWithAB():base(){
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_PAINT
            if (m.Msg == 0x0F)
            {
                using (Graphics lgGraphics = Graphics.FromHwndInternal(m.HWnd))
                    OnPaintOver(new PaintEventArgs(lgGraphics, this.ClientRectangle));
            }
        }

        protected virtual void OnPaintOver(PaintEventArgs e)
        {
            int spaceLeftRight = 12;
            int leftA = (int)((a / (float)Maximum) * (Width - 2 * spaceLeftRight) + spaceLeftRight);
            int leftB = (int)((b / (float)Maximum) * (Width - 2 * spaceLeftRight) + spaceLeftRight);
            e.Graphics.DrawLine(System.Drawing.Pens.Green, new System.Drawing.Point(leftA, 0), new System.Drawing.Point(leftA, Height));
            e.Graphics.DrawLine(System.Drawing.Pens.Red, new System.Drawing.Point(leftB, 0), new System.Drawing.Point(leftB, Height));
            e.Graphics.Flush();
        }
    }
}
