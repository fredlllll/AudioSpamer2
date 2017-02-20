using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AudioSpamer2
{
    public partial class PitchControls : UserControl
    {
        public PitchControls()
        {
            InitializeComponent();

            Apply();
        }

        void Apply()
        {
            if (trackBar1.Enabled)
            {
                trackBar1_Scroll(null, null);
            }
            if (trackBar2.Enabled)
            {
                trackBar2_Scroll(null, null);
            }
            if (trackBar3.Enabled)
            {
                trackBar3_Scroll(null, null);
            }
        }

        public void LoadFrom(IniFile ini)
        {
            int temp = 0;
            if (ini != null)
            {
                if (trackBar1.Enabled &&int.TryParse(ini.GetProperty(this.Name + trackBar1.Name), out temp))
                {
                    trackBar1.Value = temp;
                }
                if (trackBar2.Enabled && int.TryParse(ini.GetProperty(this.Name + trackBar2.Name), out temp))
                {
                    trackBar2.Value = temp;
                }
                if (trackBar3.Enabled && int.TryParse(ini.GetProperty(this.Name + trackBar3.Name), out temp))
                {
                    trackBar3.Value = temp;
                }
            }
        }

        public void Save(IniFile ini)
        {
            if (trackBar1.Enabled)
                ini.SetProperty(this.Name + trackBar1.Name, trackBar1.Value.ToString());
            if (trackBar2.Enabled)
                ini.SetProperty(this.Name + trackBar2.Name, trackBar2.Value.ToString());
            if (trackBar3.Enabled)
                ini.SetProperty(this.Name + trackBar3.Name, trackBar3.Value.ToString());
        }

        public int SpeedDefault = 100, PitchDefault = 0, TempoDefault = 0;

        public bool SpeedEnabled
        {
            get { return trackBar1.Enabled; }
            set
            {
                if (!value && trackBar1.Enabled)
                {
                    this.Controls.Remove(trackBar1); this.Controls.Remove(label1);
                }
                else if (value && !trackBar1.Enabled)
                {
                    this.Controls.Add(trackBar1); this.Controls.Add(label1);
                }
                trackBar1.Enabled = value;
                Reorder();
            }
        }

        public bool PitchEnabled
        {
            get { return trackBar2.Enabled; }
            set
            {

                if (!value && trackBar2.Enabled)
                {
                    this.Controls.Remove(trackBar2); this.Controls.Remove(label2);
                }
                else if (value && !trackBar2.Enabled)
                {
                    this.Controls.Add(trackBar2); this.Controls.Add(label2);
                }
                trackBar2.Enabled = value;
                Reorder();
            }
        }

        public bool TempoEnabled
        {
            get { return trackBar3.Enabled; }
            set
            {

                if (!value && trackBar3.Enabled)
                {
                    this.Controls.Remove(trackBar3); this.Controls.Remove(label3);
                }
                else if (value && !trackBar3.Enabled)
                {
                    this.Controls.Add(trackBar3); this.Controls.Add(label3);
                }
                trackBar3.Enabled = value;
                Reorder();
            }
        }

        void Reorder()
        {
            int count = -1;
            if (trackBar1.Enabled)
            {
                count++;
            }
            label1.Top = 3 + count * 45;
            label1.BringToFront();
            trackBar1.Top = 20 + count * 45;
            if (trackBar2.Enabled)
            {
                count++;
            }
            label2.Top = 3 + count * 45;
            label2.BringToFront();
            trackBar2.Top = 20 + count * 45;
            if (trackBar3.Enabled)
            {
                count++;
            }
            label3.Top = 3 + count * 45;
            label3.BringToFront();
            trackBar3.Top = 20 + count * 45;
            this.Height = 20 + (count + 1) * 45;
        }

        SoundChannel sc;

        public void SetSoundChannel(SoundChannel sc = null)
        {
            this.sc = sc;
            Apply();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Speed: " + String.Format("{0:0.00}", trackBar1.Value / 100.0f);
            if (sc != null)
            {
                sc.Speed = trackBar1.Value / 100.0f;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Pitch: " + String.Format("{0:0.00}", trackBar2.Value / 100.0f);
            if (sc != null)
            {
                sc.Pitch = trackBar2.Value / 100.0f;
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label3.Text = "Tempo: " + String.Format("{0:0}", trackBar3.Value);
            if (sc != null)
            {
                sc.Tempo = trackBar3.Value;
            }
        }

        void trackBar1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                trackBar1.Value = SpeedDefault;
            }
        }
        void trackBar2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                trackBar2.Value = PitchDefault;
            }
        }
        void trackBar3_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                trackBar3.Value = TempoDefault;
            }
        }
    }
}
