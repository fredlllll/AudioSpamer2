using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;
using System.IO;
using AudioSpamer2.Effects;

namespace AudioSpamer2
{
    public partial class Form1 : Form
    {
        String[] bassreg = { "frederik.gelder@freenet.de", "2X16373726163723" };
        StartOptions StartOptions1;

        ReplayMic rm;

        public SoundFile currentsound = null;

        Size oldsize;
        IniFile ini;
        System.Timers.Timer barUpdater = new System.Timers.Timer(100);

        EffectsControl effects;
        public Form1(bool initialMode,IniFile ini)
        {
            this.ini = ini;
            this.initialMode = initialMode;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            BassNet.Registration(bassreg[0], bassreg[1]);
            InitializeComponent();
            if (System.IO.File.Exists("listbg.png"))
            {
                this.listView1.BackgroundImage = new Bitmap("listbg.png");
            }

            AudioEffect[] aeffects = new AudioEffect[]{
                new AutoWah(),
                new Chorus(),
                new Distortion(),
                new Echo1(),
                new Echo2(),
                new Echo3(),
                new Flanger(),
                new LPF(),
                new Phaser(),
                new Reverb()
            };
            this.effects = new EffectsControl();
            effects.SetEffectsAndForm1(this, aeffects);
            this.Controls.Add(effects);


            this.StartOptions1 = new AudioSpamer2.StartOptions(ini);
            // 
            // StartOptions1
            // 
            this.StartOptions1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartOptions1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartOptions1.Location = new System.Drawing.Point(0, 0);
            this.StartOptions1.Margin = new System.Windows.Forms.Padding(1);
            this.StartOptions1.Name = "StartOptions1";
            this.StartOptions1.Size = new System.Drawing.Size(600, 25);
            this.StartOptions1.TabIndex = 0;
            this.StartOptions1.OKClick += new AudioSpamer2.StartOptions.voidHandler(this.StartOptions1_OKClick);
            this.StartOptions1.TotallyHidden += new StartOptions.voidHandler(StartOptions1_TotallyHidden);
            this.Controls.Add(this.StartOptions1);
            StartOptions1.BringToFront();
            resizeTimer.Elapsed += new System.Timers.ElapsedEventHandler(resizeTimer_Elapsed);
            this.Resize += new EventHandler(Form1_Resize);

            if (initialMode)
            {
                oldsize = this.ClientSize;
                this.ClientSize = StartOptions1.Size;
            }
            else
            {
                StartOptions1.Width = this.ClientSize.Width;
                StartOptions1.Hide();
            }

            Bass.BASS_Init(StartOptions1.SelectedOutput, Global.defaultSampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            rm = new ReplayMic(StartOptions1.SelectedInput);
            pitchControls1.SetSoundChannel(rm.SoundChannel);
            pitchControls1.trackBar1.Minimum = 100;

            //ini input from here
            int temp = 0;
            if (!int.TryParse(ini.GetProperty("InputVolume"), out temp))
            {
                temp = 0;
            }
            hScrollBar1.Value = temp;
            if (!int.TryParse(ini.GetProperty("SpamVolume"), out temp))
            {
                temp = hScrollBar2.Maximum-9;
            }
            hScrollBar2.Value = temp;

            //rest
            barUpdater.Elapsed += new System.Timers.ElapsedEventHandler(barUpdater_Elapsed);
            barUpdater.Start();

            pitchControls1.LoadFrom(ini);
            pitchControls2.LoadFrom(ini);

            try
            {
                StreamReader sr = new StreamReader(new FileStream(spamfile, FileMode.Open));
                while (true)
                {
                    try
                    {
                        String[] sa = sr.ReadLine().Split('|');
                        if (File.Exists(sa[1]))
                        {
                            ListViewItem l = new ListViewItem(sa[0]);
                            l.Tag = sa[1];
                            listView1.Items.Add(l);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
                sr.Close();
            }
            catch(FileNotFoundException)
            {
                //dont care if it doesnt exists. listview will just stay empty
            }

            apply();
            effects.BringToFront();
            effects.Left = ClientSize.Width;
        }

        void apply()
        {
            hScrollBar1_Scroll(null, null);
            hScrollBar2_Scroll(null, null);
        }

        void barUpdater_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (currentsound != null)
            {
                bar.Value = (int)(bar.Maximum * currentsound.sc.PercentagePlayed);
                currentsound.CheckForEndAndReplay();
            }
        }

        void bar_Scroll(object sender, System.EventArgs e)
        {
            if (currentsound != null)
            {
                currentsound.sc.StreamPosition = (long)(((double)bar.Value / bar.Maximum) * currentsound.sc.StreamLength);
            }
        }

        public const String spamfile = "spams.txt";

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pitchControls1.Save(ini);
            pitchControls2.Save(ini);

            //save list
            
            if (System.IO.File.Exists(spamfile))
            {
                System.IO.File.Delete(spamfile);
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.File.Create(spamfile));
            foreach (ListViewItem item in listView1.Items)
            {
                sw.WriteLine(item.Text + "|" + item.Tag.ToString());
            }
            sw.Close();

            ini.Flush();
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            StartOptions1.Width = this.ClientSize.Width;
        }

        void resizeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            fWidth += wmod;
            fHeight += hmod;
            this.ClientSize = new Size((int)fWidth, (int)fHeight);
            effects.Left = ClientSize.Width;
            if (this.ClientSize.Height > oldsize.Height || this.ClientSize.Width > oldsize.Width)
            {
                this.ClientSize = oldsize;
                resizeTimer.Stop();
                return;
            }
        }
        bool initialMode;
        System.Timers.Timer resizeTimer = new System.Timers.Timer(10);
        float fHeight, fWidth;
        float hmod, wmod;
        void StartOptions1_TotallyHidden()
        {
            if (initialMode)
            {
                initialMode = false;
                fHeight = this.ClientSize.Height;
                fWidth = this.ClientSize.Width;
                hmod = Math.Abs(oldsize.Height - fHeight)/75.0f;
                wmod = Math.Abs(oldsize.Width - fWidth) / 75.0f;
                resizeTimer.Start();
            }
        }

        void StartOptions1_OKClick()
        {
            if (StartOptions1.micbox.SelectedItem != null)
            {
                ini.SetProperty("Input", StartOptions1.micbox.SelectedItem.ToString());
            }
            if (StartOptions1.soundbox.SelectedItem != null)
            {
                ini.SetProperty("Output", StartOptions1.soundbox.SelectedItem.ToString());
            }
            ini.Flush();

            long pos = 0;
            if (currentsound != null)
            {
                pos = currentsound.sc.StreamPosition;
            }

            Bass.BASS_Free();
            Global.outputDeviceIndex = StartOptions1.SelectedOutput;
            Bass.BASS_Init(Global.outputDeviceIndex, Global.defaultSampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            rm.STOP();
            Global.inputDeviceIndex = StartOptions1.SelectedInput;
            rm = new ReplayMic(Global.inputDeviceIndex);
            pitchControls1.SetSoundChannel(rm.SoundChannel);

            if (currentsound != null)
            {
                SetCurrentSound(new SoundFile(currentsound.path), pos);
            }

            apply();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartOptions1.Show();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            ini.SetProperty("InputVolume", hScrollBar1.Value.ToString());
            rm.Volume = hScrollBar1.Value / 100.0f;
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            ini.SetProperty("SpamVolume", hScrollBar2.Value.ToString());
            if (currentsound != null)
            {
                currentsound.sc.Volume = hScrollBar2.Value / 100.0f;
            }
        }

        private void buttonPlayPause_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                if (currentsound.sc.playing)
                {
                    currentsound.sc.Pause();
                    buttonPlayPause.Text = "Play";
                }
                else
                {
                    currentsound.sc.Play();
                    buttonPlayPause.Text = "Pause";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String[] sa = Un4seen.Bass.Bass.SupportedStreamExtensions.Split(';');
            ofd.Filter = "";
            for (int i = 0; i < sa.Length; i++)
            {
                if (i == 0)
                {
                    ofd.Filter += sa[i] + "|" + sa[i];
                }
                else
                {
                    ofd.Filter += "|" + sa[i] + "|" + sa[i];
                }
            }
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ListViewItem l = new ListViewItem(ofd.SafeFileName);
                l.Tag = ofd.FileName;
                listView1.Items.Add(l);
            }
        }

        void listView1_ItemActivate(object sender, System.EventArgs e)
        {
            String path = (String)listView1.SelectedItems[0].Tag;
            SetCurrentSound(new SoundFile(path));
            /*bool playMarks = false;
            bool reversed = false;
            if (currentsound != null)
            {
                playMarks = currentsound.playMarks;
                reversed = currentsound.reversed;
                currentsound.Free();
                currentsound.ClearSoundChannelChanged();
            }
            currentsound = new SoundFile(path);
            currentsound.SoundChannelChanged += new SoundFile.SoundChannelChangedHandler(currentsound_SoundChannelChanged);
            pitchControls2.SetSoundChannel(currentsound.sc);
            hScrollBar2_Scroll(null, null);
            buttonPlayPause_Click(null, null);

            if (playMarks)
            {
                currentsound.PlayMarks(MarkA, MarkB);
            }
            if (reversed)
            {
                currentsound.Reverse();
            }*/
        }

        SoundFile.SoundChannelChangedHandler handler;
        public void SetCurrentSound(SoundFile file,long pos = 0)
        {
            bool playMarks = false;
            bool reversed = false;

            if (currentsound != null)
            {
                playMarks = currentsound.playMarks;
                reversed = currentsound.reversed;
                currentsound.Free();
                currentsound.SoundChannelChanged -= handler;
            }
            currentsound = file;
            handler = new SoundFile.SoundChannelChangedHandler(currentsound_SoundChannelChanged);
            currentsound.SoundChannelChanged += handler;
            pitchControls2.SetSoundChannel(currentsound.sc);
            hScrollBar2_Scroll(null, null);
            buttonPlayPause_Click(null, null);



            if (MarkA > currentsound.sc.StreamLength)
            {
                MarkA = currentsound.sc.StreamLength;
            }
            currentsound.A = MarkA;
            if (MarkB > currentsound.sc.StreamLength)
            {
                MarkB = currentsound.sc.StreamLength;
            }
            currentsound.B = MarkB;
            float percA = currentsound.A / (float)currentsound.sc.StreamLength;
            bar.A = (int)(percA * bar.Maximum);
            float percB = currentsound.B / (float)currentsound.sc.StreamLength;
            bar.B = (int)(percB * bar.Maximum);

            if (MarkA == MarkB)
            {
                MarkA = 0;
                bar.A = 0;
                MarkB = currentsound.sc.StreamLength;
                bar.B = bar.Maximum;
            }
            if (playMarks)
            {
                currentsound.PlayMarks();
            }
            if (reversed)
            {
                currentsound.Reverse();
            }
            currentsound.sc.StreamPosition = pos;
            currentsound.Loop = checkBox1.Checked;
            bar.Refresh();
            effects.ReApply();
        }

        void currentsound_SoundChannelChanged(SoundChannel c)
        {
            pitchControls2.SetSoundChannel(c);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                currentsound.Reverse();
            }
        }

        Int64 MarkA, MarkB;
        private void button5_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                MarkA = currentsound.sc.StreamPosition;
                currentsound.A = MarkA;
                bar.A = bar.Value;
                bar.Refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                MarkB = currentsound.sc.StreamPosition;
                currentsound.B = MarkB;
                bar.B = bar.Value;
                bar.Refresh();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                if (currentsound.playMarks)
                {
                    button7.Text = "Play A-B";
                    currentsound.StopPlayingMarks();
                }
                else
                {
                    button7.Text = "Stop A-B";
                    currentsound.A = MarkA;
                    currentsound.B = MarkB;
                    currentsound.PlayMarks();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                currentsound.Loop = checkBox1.Checked;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            effects.Show();
        }

    }
}
