using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioSpamer2.Controls
{
    public partial class NamedVolumeController : UserControl
    {
        public event ScrollEventHandler VolumeScroll;
        public event EventHandler VolumeChanged;

        public float Value
        {
            get
            {
                return (float)volumeSlider.Value/volumeSlider.Maximum;
            }
            set
            {
                float val = Math.Max(0, value);
                val = Math.Min(1, value);
                volumeSlider.Value = (int)(val * volumeSlider.Maximum);
            }
        }

        string label;
        public string Label
        {
            get { return label; }
            set {
                label = value;
                lblName.Text = label+":";
                var size = TextRenderer.MeasureText(lblName.Text, lblName.Font);
                lblName.Width = size.Width;
                var controlSize = MinimumSize;
                controlSize.Width = size.Width + 2;
                MinimumSize = controlSize;
                volumeSlider.Left = size.Width + 1;
                volumeSlider.Width = ClientSize.Width - volumeSlider.Left;
            }
        }

        public NamedVolumeController()
        {
            InitializeComponent();

            volumeSlider.Scroll += VolumeSlider_Scroll;
            volumeSlider.ValueChanged += VolumeSlider_ValueChanged;
        }

        private void VolumeSlider_ValueChanged(object sender, EventArgs e)
        {
            VolumeChanged?.Invoke(this, e);
        }

        private void VolumeSlider_Scroll(object sender, ScrollEventArgs e)
        {
            VolumeScroll?.Invoke(this, e);
        }
    }
}
