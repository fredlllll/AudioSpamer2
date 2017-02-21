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
    public partial class MainForm : Form
    {
        const string bassRegMail = "frederik.gelder@freenet.de";
        const string bassRegCode = "2X16373726163723";
        StartOptions spamerStartOptions;

        ReplayMic replayMic;

        public SoundFile currentsound = null;

        Size oldsize;
        IniFile ini;
        System.Timers.Timer barUpdater = new System.Timers.Timer(100);

        EffectsControl effects;
        public MainForm(bool initialMode,IniFile ini)
        {
            this.ini = ini;
            this.initialMode = initialMode;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            BassNet.Registration(bassRegMail, bassRegCode);
            InitializeComponent();
            if (System.IO.File.Exists("listbg.png"))
            {
                this.lstSpams.BackgroundImage = new Bitmap("listbg.png");
            }

            AudioEffect[] aeffects = new AudioEffect[]{
                new AutoWah(),
                new Chorus(),
                new Distortion(),
                new Echo1(),
                new Echo2(),
                new Echo3(),
                //new Flanger(),
                new LPF(),
                new Phaser(),
                new Reverb()
            };
            this.effects = new EffectsControl();
            effects.SetEffectsAndForm1(this, aeffects);
            this.Controls.Add(effects);


            this.spamerStartOptions = new AudioSpamer2.StartOptions(ini);
            // 
            // StartOptions1
            // 
            this.spamerStartOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spamerStartOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spamerStartOptions.Location = new System.Drawing.Point(0, 0);
            this.spamerStartOptions.Margin = new System.Windows.Forms.Padding(1);
            this.spamerStartOptions.Name = "StartOptions1";
            this.spamerStartOptions.Size = new System.Drawing.Size(600, 25);
            this.spamerStartOptions.TabIndex = 0;
            this.spamerStartOptions.OKClick += new AudioSpamer2.StartOptions.voidHandler(this.StartOptions1_OKClick);
            this.spamerStartOptions.TotallyHidden += new StartOptions.voidHandler(StartOptions1_TotallyHidden);
            this.Controls.Add(this.spamerStartOptions);
            spamerStartOptions.BringToFront();
            resizeTimer.Elapsed += new System.Timers.ElapsedEventHandler(resizeTimer_Elapsed);
            this.Resize += new EventHandler(Form1_Resize);

            if (initialMode)
            {
                oldsize = this.ClientSize;
                this.ClientSize = spamerStartOptions.Size;
            }
            else
            {
                spamerStartOptions.Width = this.ClientSize.Width;
                spamerStartOptions.Hide();
            }

            Bass.BASS_Init(spamerStartOptions.SelectedOutput, Global.defaultSampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            replayMic = new ReplayMic(spamerStartOptions.SelectedInput);
            pitchControlsMicrophone.SetSoundChannel(replayMic.SoundChannel);
            pitchControlsMicrophone.trackBar1.Minimum = 100;

            //ini input from here
            int temp = 0;
            if (!int.TryParse(ini.GetProperty("InputVolume"), out temp))
            {
                temp = 0;
            }
            volumeMicrophone.Value = temp;
            if (!int.TryParse(ini.GetProperty("SpamVolume"), out temp))
            {
                temp = volumeSpam.Maximum-9;
            }
            volumeSpam.Value = temp;

            //rest
            barUpdater.Elapsed += new System.Timers.ElapsedEventHandler(barUpdater_Elapsed);
            barUpdater.Start();

            pitchControlsMicrophone.LoadFrom(ini);
            pitchControlsSpam.LoadFrom(ini);

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
                            lstSpams.Items.Add(l);
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
            volumeMicrophone_Scroll(null, null);
            volumeSpam_Scroll(null, null);
        }

        void barUpdater_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(InvokeRequired)
            {
                barUpdater_Elapsed();
                return;
            }
            barUpdater_Elapsed();
        }

        void barUpdater_Elapsed() {
            if(currentsound != null)
            {
                timeline.Value = (int)(timeline.Maximum * currentsound.sc.PercentagePlayed);
                currentsound.CheckForEndAndReplay();
            }
        }

        void timeline_Scroll(object sender, System.EventArgs e)
        {
            if (currentsound != null)
            {
                currentsound.sc.StreamPosition = (long)(((double)timeline.Value / timeline.Maximum) * currentsound.sc.StreamLength);
            }
        }

        public const String spamfile = "spams.txt";

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pitchControlsMicrophone.Save(ini);
            pitchControlsSpam.Save(ini);

            //save list
            
            if (System.IO.File.Exists(spamfile))
            {
                System.IO.File.Delete(spamfile);
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.File.Create(spamfile));
            foreach (ListViewItem item in lstSpams.Items)
            {
                sw.WriteLine(item.Text + "|" + item.Tag.ToString());
            }
            sw.Close();

            ini.Flush();
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            spamerStartOptions.Width = this.ClientSize.Width;
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
            if (spamerStartOptions.micbox.SelectedItem != null)
            {
                ini.SetProperty("Input", spamerStartOptions.micbox.SelectedItem.ToString());
            }
            if (spamerStartOptions.soundbox.SelectedItem != null)
            {
                ini.SetProperty("Output", spamerStartOptions.soundbox.SelectedItem.ToString());
            }
            ini.Flush();

            long pos = 0;
            if (currentsound != null)
            {
                pos = currentsound.sc.StreamPosition;
            }

            Bass.BASS_Free();
            Global.outputDeviceIndex = spamerStartOptions.SelectedOutput;
            Bass.BASS_Init(Global.outputDeviceIndex, Global.defaultSampleRate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            replayMic.STOP();
            Global.inputDeviceIndex = spamerStartOptions.SelectedInput;
            replayMic = new ReplayMic(Global.inputDeviceIndex);
            pitchControlsMicrophone.SetSoundChannel(replayMic.SoundChannel);

            if (currentsound != null)
            {
                SetCurrentSound(new SoundFile(currentsound.path), pos);
            }

            apply();
        }

        private void btnSoundOptions_Click(object sender, EventArgs e)
        {
            spamerStartOptions.Show();
        }

        private void volumeMicrophone_Scroll(object sender, ScrollEventArgs e)
        {
            ini.SetProperty("InputVolume", volumeMicrophone.Value.ToString());
            replayMic.Volume = volumeMicrophone.Value / 100.0f;
        }

        private void volumeSpam_Scroll(object sender, ScrollEventArgs e)
        {
            ini.SetProperty("SpamVolume", volumeSpam.Value.ToString());
            if (currentsound != null)
            {
                currentsound.sc.Volume = volumeSpam.Value / 100.0f;
            }
        }

        private void buttonPlayPause_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                if (currentsound.sc.playing)
                {
                    currentsound.sc.Pause();
                    btnPlayPause.Text = "Play";
                }
                else
                {
                    currentsound.sc.Play();
                    btnPlayPause.Text = "Pause";
                }
            }
        }

        private void btnAddNewSpam_Click(object sender, EventArgs e)
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
                lstSpams.Items.Add(l);
            }
        }

        void lstSpams_ItemActivate(object sender, System.EventArgs e)
        {
            String path = (String)lstSpams.SelectedItems[0].Tag;
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
            pitchControlsSpam.SetSoundChannel(currentsound.sc);
            volumeSpam_Scroll(null, null);
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
            timeline.A = (int)(percA * timeline.Maximum);
            float percB = currentsound.B / (float)currentsound.sc.StreamLength;
            timeline.B = (int)(percB * timeline.Maximum);

            if (MarkA == MarkB)
            {
                MarkA = 0;
                timeline.A = 0;
                MarkB = currentsound.sc.StreamLength;
                timeline.B = timeline.Maximum;
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
            currentsound.Loop = chkLoop.Checked;
            timeline.Refresh();
            effects.ReApply();
        }

        void currentsound_SoundChannelChanged(SoundChannel c)
        {
            pitchControlsSpam.SetSoundChannel(c);
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                currentsound.Reverse();
            }
        }

        Int64 MarkA, MarkB;
        private void btnSetA_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                MarkA = currentsound.sc.StreamPosition;
                currentsound.A = MarkA;
                timeline.A = timeline.Value;
                timeline.Refresh();
            }
        }

        private void btnSetB_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                MarkB = currentsound.sc.StreamPosition;
                currentsound.B = MarkB;
                timeline.B = timeline.Value;
                timeline.Refresh();
            }
        }

        private void btnPlayAToB_Click(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                if (currentsound.playMarks)
                {
                    btnPlayAToB.Text = "Play A-B";
                    currentsound.StopPlayingMarks();
                }
                else
                {
                    btnPlayAToB.Text = "Stop A-B";
                    currentsound.A = MarkA;
                    currentsound.B = MarkB;
                    currentsound.PlayMarks();
                }
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lstSpams.SelectedItems.Count > 0)
            {
                lstSpams.Items.Remove(lstSpams.SelectedItems[0]);
            }
        }

        private void chkLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (currentsound != null)
            {
                currentsound.Loop = chkLoop.Checked;
            }
        }

        private void btnEffects_Click(object sender, EventArgs e)
        {
            effects.Show();
        }

    }
}
