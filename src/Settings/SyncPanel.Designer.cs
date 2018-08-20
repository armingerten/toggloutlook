namespace TogglOutlookPlugIn.Settings
{
    partial class SyncPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelComingSoon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelComingSoon
            // 
            this.labelComingSoon.AutoSize = true;
            this.labelComingSoon.Location = new System.Drawing.Point(12, 14);
            this.labelComingSoon.Name = "labelComingSoon";
            this.labelComingSoon.Size = new System.Drawing.Size(88, 13);
            this.labelComingSoon.TabIndex = 0;
            this.labelComingSoon.Text = "Comming soon ...";
            // 
            // SyncPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelComingSoon);
            this.Name = "SyncPanel";
            this.Size = new System.Drawing.Size(700, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelComingSoon;
    }
}
