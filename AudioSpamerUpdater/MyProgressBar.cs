using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AudioSpamer2
{
    public partial class MyProgressBar : UserControl
    {
        int val = 0;
        public int Value{
            get { return val; }
            set { val = value; Refresh(); }
        }
        public int Maximum = 100;
        public MyProgressBar()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            float perc = (float)(Value)/Maximum;
            e.Graphics.FillRectangle(Brushes.Green, new Rectangle(0, 0, (int)(this.Width * perc), this.Height));
        }
    }
}
