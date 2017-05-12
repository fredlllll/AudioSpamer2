using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;

namespace AudioSpamer2
{
    public partial class StartOptions : UserControl
    {
        public BASS_DEVICEINFO[] InputDevices;
        public BASS_DEVICEINFO[] OutputDevices;
        public StartOptions()
        {
            InitializeComponent();
            InputDevices = Bass.BASS_RecordGetDeviceInfos();
            OutputDevices = Bass.BASS_GetDeviceInfos();
            int indextoselect = 0;
            String value = Program.Config.Get("Input");
            for (int i = 0; i < InputDevices.Length; i++)
            {
                micbox.Items.Add(InputDevices[i]);
                if (InputDevices[i].ToString().Equals(value))
                {
                    indextoselect = i;
                }
            }
            if (InputDevices.Length > 0)
            {
                micbox.SelectedIndex = indextoselect;
            }
            value = Program.Config.Get("Output");
            for (int i = 0; i < OutputDevices.Length; i++)
            {
                soundbox.Items.Add(OutputDevices[i]);
                if (OutputDevices[i].ToString().Equals(value))
                {
                    indextoselect = i;
                }
            }
            if (OutputDevices.Length > 0)
            {
                soundbox.SelectedIndex = indextoselect;
            }
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            showTimer.Elapsed += new System.Timers.ElapsedEventHandler(showTimer_Elapsed);
        }

        public int SelectedInput
        {
            get { return indexOf((BASS_DEVICEINFO)micbox.SelectedItem, InputDevices); }
        }

        public int SelectedOutput
        {
            get { return indexOf((BASS_DEVICEINFO)soundbox.SelectedItem, OutputDevices); }
        }

        public int indexOf(BASS_DEVICEINFO di, BASS_DEVICEINFO[] dia)
        {
            for (int i = 0; i < dia.Length;i++ )
            {
                if (di == dia[i])
                {
                    return i;
                }
            }
            return 0;
        }

        void showTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.Top >= 0)
            {
                showTimer.Stop();
                return;
            }
            this.Top = this.Top + 1;
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (-this.Top > this.Height)
            {
                t.Stop();
                if (TotallyHidden != null)
                {
                    TotallyHidden();
                }
                return;
            }
            this.Top = this.Top - 1;
        }
        public delegate void voidHandler();
        public event voidHandler OKClick;
        public event voidHandler TotallyHidden;

        public new void Show()
        {
            showTimer.Start();
            t.Stop();
        }

        public new void Hide()
        {
            t.Start();
            showTimer.Stop();
        }

        System.Timers.Timer t = new System.Timers.Timer(10);
        System.Timers.Timer showTimer = new System.Timers.Timer(10);

        private void button1_Click(object sender, EventArgs e)
        {
            if (OKClick != null)
            {
                OKClick();
            }
            Hide();
        }
    }
}
