namespace AudioSpamer2
{
    partial class StartOptions
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.micbox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.soundbox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Microphone Input:";
            // 
            // micbox
            // 
            this.micbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.micbox.FormattingEnabled = true;
            this.micbox.Location = new System.Drawing.Point(102, 1);
            this.micbox.Name = "micbox";
            this.micbox.Size = new System.Drawing.Size(168, 21);
            this.micbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sound Output:";
            // 
            // soundbox
            // 
            this.soundbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.soundbox.FormattingEnabled = true;
            this.soundbox.Location = new System.Drawing.Point(358, 1);
            this.soundbox.Name = "soundbox";
            this.soundbox.Size = new System.Drawing.Size(168, 21);
            this.soundbox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(532, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 22);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StartOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.soundbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.micbox);
            this.Controls.Add(this.label1);
            this.Name = "StartOptions";
            this.Size = new System.Drawing.Size(589, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox micbox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox soundbox;
        private System.Windows.Forms.Button button1;
    }
}
