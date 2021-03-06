﻿using System.Drawing;
namespace AudioSpamer2
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSoundOptions = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.grpMicrophone = new System.Windows.Forms.GroupBox();
            this.pitchControlsMicrophone = new AudioSpamer2.PitchControls();
            this.lstSpams = new System.Windows.Forms.ListView();
            this.btnAddNewSpam = new System.Windows.Forms.Button();
            this.btnRemoveSelectedSpam = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnReverse = new System.Windows.Forms.Button();
            this.btnSetA = new System.Windows.Forms.Button();
            this.btnSetB = new System.Windows.Forms.Button();
            this.btnPlayAToB = new System.Windows.Forms.Button();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnEffects = new System.Windows.Forms.Button();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.volumeControls = new System.Windows.Forms.SplitContainer();
            this.volumeSpam = new AudioSpamer2.Controls.NamedVolumeController();
            this.volumeMicrophone = new AudioSpamer2.Controls.NamedVolumeController();
            this.timeline = new AudioSpamer2.TrackBarWithAB();
            this.pitchControlsSpam = new AudioSpamer2.PitchControls();
            this.grpMicrophone.SuspendLayout();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeControls)).BeginInit();
            this.volumeControls.Panel1.SuspendLayout();
            this.volumeControls.Panel2.SuspendLayout();
            this.volumeControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSoundOptions
            // 
            this.btnSoundOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSoundOptions.Location = new System.Drawing.Point(3, 3);
            this.btnSoundOptions.Name = "btnSoundOptions";
            this.btnSoundOptions.Size = new System.Drawing.Size(83, 26);
            this.btnSoundOptions.TabIndex = 1;
            this.btnSoundOptions.Text = "Soundoptions";
            this.btnSoundOptions.UseVisualStyleBackColor = true;
            this.btnSoundOptions.Click += new System.EventHandler(this.btnSoundOptions_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Spam:";
            // 
            // grpMicrophone
            // 
            this.grpMicrophone.Controls.Add(this.pitchControlsMicrophone);
            this.grpMicrophone.Location = new System.Drawing.Point(12, 32);
            this.grpMicrophone.Name = "grpMicrophone";
            this.grpMicrophone.Size = new System.Drawing.Size(275, 138);
            this.grpMicrophone.TabIndex = 11;
            this.grpMicrophone.TabStop = false;
            this.grpMicrophone.Text = "Microphone:";
            // 
            // pitchControlsMicrophone
            // 
            this.pitchControlsMicrophone.Location = new System.Drawing.Point(6, 19);
            this.pitchControlsMicrophone.Name = "pitchControlsMicrophone";
            this.pitchControlsMicrophone.PitchEnabled = true;
            this.pitchControlsMicrophone.Size = new System.Drawing.Size(261, 110);
            this.pitchControlsMicrophone.SpeedEnabled = true;
            this.pitchControlsMicrophone.TabIndex = 7;
            this.pitchControlsMicrophone.TempoEnabled = false;
            // 
            // lstSpams
            // 
            this.lstSpams.AllowDrop = true;
            this.lstSpams.BackgroundImage = global::AudioSpamer2.Properties.Resources.ListBG;
            this.lstSpams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSpams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstSpams.LabelEdit = true;
            this.lstSpams.Location = new System.Drawing.Point(293, 32);
            this.lstSpams.MultiSelect = false;
            this.lstSpams.Name = "lstSpams";
            this.lstSpams.ShowGroups = false;
            this.lstSpams.Size = new System.Drawing.Size(513, 438);
            this.lstSpams.TabIndex = 12;
            this.lstSpams.TileSize = new System.Drawing.Size(180, 32);
            this.lstSpams.UseCompatibleStateImageBehavior = false;
            this.lstSpams.ItemActivate += new System.EventHandler(this.lstSpams_ItemActivate);
            // 
            // btnAddNewSpam
            // 
            this.btnAddNewSpam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewSpam.Location = new System.Drawing.Point(840, 3);
            this.btnAddNewSpam.Name = "btnAddNewSpam";
            this.btnAddNewSpam.Size = new System.Drawing.Size(102, 26);
            this.btnAddNewSpam.TabIndex = 13;
            this.btnAddNewSpam.Text = "Add new Spam";
            this.btnAddNewSpam.UseVisualStyleBackColor = true;
            this.btnAddNewSpam.Click += new System.EventHandler(this.btnAddNewSpam_Click);
            // 
            // btnRemoveSelectedSpam
            // 
            this.btnRemoveSelectedSpam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveSelectedSpam.Location = new System.Drawing.Point(724, 3);
            this.btnRemoveSelectedSpam.Name = "btnRemoveSelectedSpam";
            this.btnRemoveSelectedSpam.Size = new System.Drawing.Size(110, 26);
            this.btnRemoveSelectedSpam.TabIndex = 14;
            this.btnRemoveSelectedSpam.Text = "Remove Selected";
            this.btnRemoveSelectedSpam.UseVisualStyleBackColor = true;
            this.btnRemoveSelectedSpam.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // ofd
            // 
            this.ofd.SupportMultiDottedExtensions = true;
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(12, 335);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(87, 29);
            this.btnReverse.TabIndex = 15;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnSetA
            // 
            this.btnSetA.ForeColor = System.Drawing.Color.Green;
            this.btnSetA.Location = new System.Drawing.Point(12, 370);
            this.btnSetA.Name = "btnSetA";
            this.btnSetA.Size = new System.Drawing.Size(87, 29);
            this.btnSetA.TabIndex = 16;
            this.btnSetA.Text = "Set A";
            this.btnSetA.UseVisualStyleBackColor = true;
            this.btnSetA.Click += new System.EventHandler(this.btnSetA_Click);
            // 
            // btnSetB
            // 
            this.btnSetB.ForeColor = System.Drawing.Color.Red;
            this.btnSetB.Location = new System.Drawing.Point(105, 370);
            this.btnSetB.Name = "btnSetB";
            this.btnSetB.Size = new System.Drawing.Size(87, 29);
            this.btnSetB.TabIndex = 17;
            this.btnSetB.Text = "Set B";
            this.btnSetB.UseVisualStyleBackColor = true;
            this.btnSetB.Click += new System.EventHandler(this.btnSetB_Click);
            // 
            // btnPlayAToB
            // 
            this.btnPlayAToB.Location = new System.Drawing.Point(198, 370);
            this.btnPlayAToB.Name = "btnPlayAToB";
            this.btnPlayAToB.Size = new System.Drawing.Size(87, 29);
            this.btnPlayAToB.TabIndex = 18;
            this.btnPlayAToB.Text = "Play A-B";
            this.btnPlayAToB.UseVisualStyleBackColor = true;
            this.btnPlayAToB.Click += new System.EventHandler(this.btnPlayAToB_Click);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(12, 405);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(87, 29);
            this.btnPlayPause.TabIndex = 19;
            this.btnPlayPause.Text = "Play";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.buttonPlayPause_Click);
            // 
            // btnEffects
            // 
            this.btnEffects.Location = new System.Drawing.Point(105, 335);
            this.btnEffects.Name = "btnEffects";
            this.btnEffects.Size = new System.Drawing.Size(87, 29);
            this.btnEffects.TabIndex = 21;
            this.btnEffects.Text = "Effects";
            this.btnEffects.UseVisualStyleBackColor = true;
            this.btnEffects.Click += new System.EventHandler(this.btnEffects_Click);
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Checked = true;
            this.chkLoop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLoop.Location = new System.Drawing.Point(113, 412);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(50, 17);
            this.chkLoop.TabIndex = 22;
            this.chkLoop.Text = "Loop";
            this.chkLoop.UseVisualStyleBackColor = true;
            this.chkLoop.CheckedChanged += new System.EventHandler(this.chkLoop_CheckedChanged);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.volumeControls);
            this.topPanel.Controls.Add(this.btnSoundOptions);
            this.topPanel.Controls.Add(this.btnAddNewSpam);
            this.topPanel.Controls.Add(this.btnRemoveSelectedSpam);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(945, 32);
            this.topPanel.TabIndex = 23;
            // 
            // volumeControls
            // 
            this.volumeControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeControls.Location = new System.Drawing.Point(92, 0);
            this.volumeControls.Name = "volumeControls";
            // 
            // volumeControls.Panel1
            // 
            this.volumeControls.Panel1.Controls.Add(this.volumeMicrophone);
            // 
            // volumeControls.Panel2
            // 
            this.volumeControls.Panel2.Controls.Add(this.volumeSpam);
            this.volumeControls.Size = new System.Drawing.Size(626, 32);
            this.volumeControls.SplitterDistance = 309;
            this.volumeControls.TabIndex = 17;
            // 
            // volumeSpam
            // 
            this.volumeSpam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.volumeSpam.Label = "Spam Volume";
            this.volumeSpam.Location = new System.Drawing.Point(0, 0);
            this.volumeSpam.MinimumSize = new System.Drawing.Size(77, 0);
            this.volumeSpam.Name = "volumeSpam";
            this.volumeSpam.Size = new System.Drawing.Size(313, 32);
            this.volumeSpam.TabIndex = 16;
            this.volumeSpam.Value = 0.5F;
            // 
            // volumeMicrophone
            // 
            this.volumeMicrophone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.volumeMicrophone.Label = "Microphone Volume";
            this.volumeMicrophone.Location = new System.Drawing.Point(0, 0);
            this.volumeMicrophone.MinimumSize = new System.Drawing.Size(106, 0);
            this.volumeMicrophone.Name = "volumeMicrophone";
            this.volumeMicrophone.Size = new System.Drawing.Size(309, 32);
            this.volumeMicrophone.TabIndex = 15;
            this.volumeMicrophone.Value = 0.5F;
            // 
            // timeline
            // 
            this.timeline.A = 0;
            this.timeline.B = 1000;
            this.timeline.Location = new System.Drawing.Point(12, 476);
            this.timeline.Maximum = 1000;
            this.timeline.Name = "timeline";
            this.timeline.Size = new System.Drawing.Size(794, 45);
            this.timeline.TabIndex = 20;
            this.timeline.Scroll += new System.EventHandler(this.timeline_Scroll);
            // 
            // pitchControlsSpam
            // 
            this.pitchControlsSpam.Location = new System.Drawing.Point(18, 189);
            this.pitchControlsSpam.Name = "pitchControlsSpam";
            this.pitchControlsSpam.PitchEnabled = true;
            this.pitchControlsSpam.Size = new System.Drawing.Size(261, 155);
            this.pitchControlsSpam.SpeedEnabled = true;
            this.pitchControlsSpam.TabIndex = 8;
            this.pitchControlsSpam.TempoEnabled = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 563);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.chkLoop);
            this.Controls.Add(this.btnEffects);
            this.Controls.Add(this.timeline);
            this.Controls.Add(this.btnPlayPause);
            this.Controls.Add(this.btnPlayAToB);
            this.Controls.Add(this.btnSetB);
            this.Controls.Add(this.btnSetA);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.lstSpams);
            this.Controls.Add(this.grpMicrophone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pitchControlsSpam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Audiospamer Version 2";
            this.grpMicrophone.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.volumeControls.Panel1.ResumeLayout(false);
            this.volumeControls.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.volumeControls)).EndInit();
            this.volumeControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeline)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSoundOptions;
        private PitchControls pitchControlsMicrophone;
        private PitchControls pitchControlsSpam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpMicrophone;
        private System.Windows.Forms.ListView lstSpams;
        private System.Windows.Forms.Button btnAddNewSpam;
        private System.Windows.Forms.Button btnRemoveSelectedSpam;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Button btnSetA;
        private System.Windows.Forms.Button btnSetB;
        private System.Windows.Forms.Button btnPlayAToB;
        private System.Windows.Forms.Button btnPlayPause;
        private TrackBarWithAB timeline;
        private System.Windows.Forms.Button btnEffects;
        private System.Windows.Forms.CheckBox chkLoop;
        private System.Windows.Forms.Panel topPanel;
        private Controls.NamedVolumeController volumeMicrophone;
        private Controls.NamedVolumeController volumeSpam;
        private System.Windows.Forms.SplitContainer volumeControls;
    }
}

