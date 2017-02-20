using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using AudioSpamer2.Effects.stuff;
using AudioSpamer2.Effects;

namespace AudioSpamer2
{
    public partial class EffectsControl : UserControl
    {
        public EffectsControl()
        {
            InitializeComponent();
            if (System.IO.File.Exists("EffectsBG.png"))
            {
                this.Effects.BackgroundImage = new Bitmap("EffectsBG.png");
            }
            hideTimer.Elapsed += new System.Timers.ElapsedEventHandler(hideTimer_Elapsed);
            showTimer.Elapsed += new System.Timers.ElapsedEventHandler(showTimer_Elapsed);
        }

        Form1 f;
        AudioEffect[] effects;
        public void SetEffectsAndForm1(Form1 f, AudioSpamer2.Effects.AudioEffect[] effects)
        {
            this.f = f;
            this.effects = effects;
            TabPage first = tabControl1.TabPages["Effects"];
            for (int i = 0; i < effects.Length; i++)
            {
                TabPage tp = new TabPage(effects[i].Name);
                effects[i].Page = tp;
                Type type = effects[i].GetType();
                PropertyInfo[] props = type.GetProperties();
                for (int j = 0; j < props.Length; j++)
                {
                    object[] oa = props[j].GetCustomAttributes(typeof(AudioSpamer2.Effects.stuff.EffectProp),false);
                    if (oa.Length > 0)
                    {
                        EffectProp ep = (EffectProp)oa[0];
                        TrackBar bar = new TrackBar();
                        bar.Minimum = (int)(ep.minValue * ep.resolution);
                        bar.Maximum = (int)(ep.maxValue * ep.resolution);
                        bar.Value = (int)(ep.defaultValue * ep.resolution);
                        bar.Width = 230;
                        Label l = new Label();
                        l.AutoSize = true;
                        l.Text = ep.Label + ": " + ep.defaultValue;
                        bar.Tag = new object[] { effects[i], props[j], ep, l };
                        EventHandler eh = new EventHandler(delegate(object sender,EventArgs args)
                        {
                            TrackBar tb = (TrackBar)sender;
                            EffectProp eprop = (EffectProp)((object[])tb.Tag)[2];
                            float val = tb.Value / (float)eprop.resolution;
                            Label lab = (Label)((object[])tb.Tag)[3];
                            lab.Text = eprop.Label + ": " + val;
                            PropertyInfo propinfo = (PropertyInfo)((object[])tb.Tag)[1];
                            AudioEffect ae = (AudioEffect)((object[])tb.Tag)[0];
                            if (eprop.isInt)
                            {
                                propinfo.SetValue(ae, (int)val, null);
                            }
                            else
                            {
                                propinfo.SetValue(ae, val, null);
                            }
                        });
                        bar.Scroll += eh;
                        bar.ValueChanged += eh;
                        bar.MouseUp += new MouseEventHandler(delegate(object sender,MouseEventArgs args)
                        {
                            if (args.Button == System.Windows.Forms.MouseButtons.Middle)
                            {
                                TrackBar tb = (TrackBar)sender;
                                EffectProp eprop = (EffectProp)((object[])tb.Tag)[2];
                                tb.Value = (int)eprop.defaultValue;
                            }
                        });
                        l.Left = 3;
                        l.Top = 3 + j * bar.Height;
                        bar.Left = 3;
                        bar.Top = 3 + l.Height + j * bar.Height;
                        tp.Controls.Add(l);
                        tp.Controls.Add(bar);
                        l.BringToFront();
                    }
                }

                CheckBox check = new CheckBox();
                check.Text = effects[i].Name;
                check.Left = 3;
                check.Top = 3 + i * check.Height;
                check.Tag = effects[i];
                first.Controls.Add(check);
                check.CheckedChanged += new EventHandler(check_CheckedChanged);
            }
        }

        void check_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            AudioSpamer2.Effects.AudioEffect effect = (AudioSpamer2.Effects.AudioEffect)cb.Tag;
            effect.enabled = cb.Checked;
            if (cb.Checked)
            {
                this.tabControl1.TabPages.Add(effect.Page);
                if (f.currentsound != null)
                {
                    effect.ApplyToSoundFile(f.currentsound);
                }
            }
            else
            {
                this.tabControl1.TabPages.Remove(effect.Page);
                if (f.currentsound != null)
                {
                    effect.RemoveFromSoundFile();
                }
            }
        }

        System.Timers.Timer hideTimer= new System.Timers.Timer(10);
        System.Timers.Timer showTimer= new System.Timers.Timer(10);
        public new void Hide()
        {
            showTimer.Stop();
            hideTimer.Start();
        }

        public new void Show()
        {
            hideTimer.Stop();
            showTimer.Start();
        }

        void showTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.Left <= (this.ParentForm.ClientSize.Width - this.Width))
            {
                this.Left = this.ParentForm.ClientSize.Width - this.Width;
                showTimer.Stop();
            }
            else
            {
                this.Left -= 4;
            }
        }

        void hideTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.Left >= this.ParentForm.ClientSize.Width)
            {
                this.Left = this.ParentForm.ClientSize.Width;
                hideTimer.Stop();
            }
            else
            {
                this.Left += 4;
            }
        }


        public void ReApply()
        {
            for (int i = 0; i < effects.Length; i++)
            {
                if (effects[i].enabled && f.currentsound != null)
                {
                    effects[i].ApplyToSoundFile(f.currentsound);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
