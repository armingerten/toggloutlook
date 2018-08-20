namespace TogglOutlookPlugIn.Settings
{
    partial class AboutPanel
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
            this.groupBoxAbout = new System.Windows.Forms.GroupBox();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.linkLabelToggl = new System.Windows.Forms.LinkLabel();
            this.labelLinks = new System.Windows.Forms.Label();
            this.linkGraphicsFrom = new System.Windows.Forms.LinkLabel();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelAuthorValue = new System.Windows.Forms.Label();
            this.groupBoxAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAbout
            // 
            this.groupBoxAbout.Controls.Add(this.linkLabelGithub);
            this.groupBoxAbout.Controls.Add(this.linkLabelToggl);
            this.groupBoxAbout.Controls.Add(this.labelLinks);
            this.groupBoxAbout.Controls.Add(this.linkGraphicsFrom);
            this.groupBoxAbout.Controls.Add(this.labelAuthor);
            this.groupBoxAbout.Controls.Add(this.labelAuthorValue);
            this.groupBoxAbout.Location = new System.Drawing.Point(160, 138);
            this.groupBoxAbout.Name = "groupBoxAbout";
            this.groupBoxAbout.Size = new System.Drawing.Size(380, 124);
            this.groupBoxAbout.TabIndex = 7;
            this.groupBoxAbout.TabStop = false;
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Location = new System.Drawing.Point(156, 76);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(218, 13);
            this.linkLabelGithub.TabIndex = 5;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = "https://github.com/armingerten/toggloutlook";
            // 
            // linkLabelToggl
            // 
            this.linkLabelToggl.AutoSize = true;
            this.linkLabelToggl.Location = new System.Drawing.Point(156, 58);
            this.linkLabelToggl.Name = "linkLabelToggl";
            this.linkLabelToggl.Size = new System.Drawing.Size(89, 13);
            this.linkLabelToggl.TabIndex = 4;
            this.linkLabelToggl.TabStop = true;
            this.linkLabelToggl.Text = "https://toggl.com";
            // 
            // labelLinks
            // 
            this.labelLinks.AutoSize = true;
            this.labelLinks.Location = new System.Drawing.Point(16, 58);
            this.labelLinks.Name = "labelLinks";
            this.labelLinks.Size = new System.Drawing.Size(35, 13);
            this.labelLinks.TabIndex = 1;
            this.labelLinks.Text = "Links:";
            // 
            // linkGraphicsFrom
            // 
            this.linkGraphicsFrom.AutoSize = true;
            this.linkGraphicsFrom.Location = new System.Drawing.Point(156, 94);
            this.linkGraphicsFrom.Name = "linkGraphicsFrom";
            this.linkGraphicsFrom.Size = new System.Drawing.Size(112, 13);
            this.linkGraphicsFrom.TabIndex = 0;
            this.linkGraphicsFrom.TabStop = true;
            this.linkGraphicsFrom.Text = "www.iconarchive.com";
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(16, 24);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(41, 13);
            this.labelAuthor.TabIndex = 2;
            this.labelAuthor.Text = "Author:";
            // 
            // labelAuthorValue
            // 
            this.labelAuthorValue.AutoSize = true;
            this.labelAuthorValue.Location = new System.Drawing.Point(156, 24);
            this.labelAuthorValue.Name = "labelAuthorValue";
            this.labelAuthorValue.Size = new System.Drawing.Size(47, 13);
            this.labelAuthorValue.TabIndex = 3;
            this.labelAuthorValue.Text = "Armin G.";
            // 
            // AboutPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAbout);
            this.Name = "AboutPanel";
            this.Size = new System.Drawing.Size(700, 400);
            this.Resize += new System.EventHandler(this.OnAboutPanelResize);
            this.groupBoxAbout.ResumeLayout(false);
            this.groupBoxAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAbout;
        private System.Windows.Forms.Label labelLinks;
        private System.Windows.Forms.LinkLabel linkGraphicsFrom;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelAuthorValue;
        private System.Windows.Forms.LinkLabel linkLabelGithub;
        private System.Windows.Forms.LinkLabel linkLabelToggl;
    }
}
