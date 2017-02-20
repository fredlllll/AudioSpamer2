using System.Drawing;
namespace AudioSpamer2
{
    partial class EffectsControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Effects = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Effects);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(250, 361);
            this.tabControl1.TabIndex = 0;
            // 
            // Effects
            // 
            this.Effects.BackgroundImage = global::AudioSpamer2.Properties.Resources.EffectsBG;
            this.Effects.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Effects.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Effects.Location = new System.Drawing.Point(4, 22);
            this.Effects.Name = "Effects";
            this.Effects.Padding = new System.Windows.Forms.Padding(3);
            this.Effects.Size = new System.Drawing.Size(242, 335);
            this.Effects.TabIndex = 0;
            this.Effects.Text = "Effects";
            this.Effects.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Hide Effects";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EffectsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "EffectsControl";
            this.Size = new System.Drawing.Size(248, 398);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Effects;
        private System.Windows.Forms.Button button1;
    }
}
