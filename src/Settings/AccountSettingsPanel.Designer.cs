namespace TogglOutlookPlugIn.Settings
{
    partial class AccountSettingsPanel
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
            this.panelAccount = new System.Windows.Forms.Panel();
            this.groupBoxAccountSetup = new System.Windows.Forms.GroupBox();
            this.labelWorkspace = new System.Windows.Forms.Label();
            this.labelApiKey = new System.Windows.Forms.Label();
            this.comboBoxWorkspace = new System.Windows.Forms.ComboBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.buttonLinkAccount = new System.Windows.Forms.Button();
            this.groupBoxAccountInformation = new System.Windows.Forms.GroupBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.panelAccount.SuspendLayout();
            this.groupBoxAccountSetup.SuspendLayout();
            this.groupBoxAccountInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAccount
            // 
            this.panelAccount.Controls.Add(this.groupBoxAccountSetup);
            this.panelAccount.Controls.Add(this.groupBoxAccountInformation);
            this.panelAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAccount.Location = new System.Drawing.Point(0, 0);
            this.panelAccount.Name = "panelAccount";
            this.panelAccount.Size = new System.Drawing.Size(700, 400);
            this.panelAccount.TabIndex = 2;
            // 
            // groupBoxAccountSetup
            // 
            this.groupBoxAccountSetup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAccountSetup.Controls.Add(this.labelWorkspace);
            this.groupBoxAccountSetup.Controls.Add(this.labelApiKey);
            this.groupBoxAccountSetup.Controls.Add(this.comboBoxWorkspace);
            this.groupBoxAccountSetup.Controls.Add(this.labelDescription);
            this.groupBoxAccountSetup.Controls.Add(this.textBoxApiKey);
            this.groupBoxAccountSetup.Controls.Add(this.buttonLinkAccount);
            this.groupBoxAccountSetup.Location = new System.Drawing.Point(12, 8);
            this.groupBoxAccountSetup.Name = "groupBoxAccountSetup";
            this.groupBoxAccountSetup.Size = new System.Drawing.Size(676, 117);
            this.groupBoxAccountSetup.TabIndex = 16;
            this.groupBoxAccountSetup.TabStop = false;
            this.groupBoxAccountSetup.Text = "Account set-up";
            // 
            // labelWorkspace
            // 
            this.labelWorkspace.AutoSize = true;
            this.labelWorkspace.Location = new System.Drawing.Point(16, 80);
            this.labelWorkspace.Name = "labelWorkspace";
            this.labelWorkspace.Size = new System.Drawing.Size(62, 13);
            this.labelWorkspace.TabIndex = 6;
            this.labelWorkspace.Text = "Workspace";
            // 
            // labelApiKey
            // 
            this.labelApiKey.AutoSize = true;
            this.labelApiKey.Location = new System.Drawing.Point(16, 54);
            this.labelApiKey.Name = "labelApiKey";
            this.labelApiKey.Size = new System.Drawing.Size(45, 13);
            this.labelApiKey.TabIndex = 5;
            this.labelApiKey.Text = "API Key";
            // 
            // comboBoxWorkspace
            // 
            this.comboBoxWorkspace.FormattingEnabled = true;
            this.comboBoxWorkspace.Location = new System.Drawing.Point(95, 77);
            this.comboBoxWorkspace.Name = "comboBoxWorkspace";
            this.comboBoxWorkspace.Size = new System.Drawing.Size(300, 21);
            this.comboBoxWorkspace.TabIndex = 4;
            this.comboBoxWorkspace.SelectedIndexChanged += new System.EventHandler(this.OnComboBoxWorkspaceSelectedIndexChanged);
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.AutoEllipsis = true;
            this.labelDescription.Location = new System.Drawing.Point(16, 25);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(649, 23);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "Please enter a valid API key and select a workspace:";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(95, 51);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(300, 20);
            this.textBoxApiKey.TabIndex = 3;
            // 
            // buttonLinkAccount
            // 
            this.buttonLinkAccount.Location = new System.Drawing.Point(401, 49);
            this.buttonLinkAccount.Name = "buttonLinkAccount";
            this.buttonLinkAccount.Size = new System.Drawing.Size(130, 23);
            this.buttonLinkAccount.TabIndex = 2;
            this.buttonLinkAccount.Text = "Link account";
            this.buttonLinkAccount.UseVisualStyleBackColor = true;
            this.buttonLinkAccount.Click += new System.EventHandler(this.OnButtonLinkAccountClick);
            // 
            // groupBoxAccountInformation
            // 
            this.groupBoxAccountInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAccountInformation.Controls.Add(this.textBoxEmail);
            this.groupBoxAccountInformation.Controls.Add(this.textBoxName);
            this.groupBoxAccountInformation.Controls.Add(this.labelEmail);
            this.groupBoxAccountInformation.Controls.Add(this.labelName);
            this.groupBoxAccountInformation.Location = new System.Drawing.Point(12, 131);
            this.groupBoxAccountInformation.Name = "groupBoxAccountInformation";
            this.groupBoxAccountInformation.Size = new System.Drawing.Size(676, 257);
            this.groupBoxAccountInformation.TabIndex = 15;
            this.groupBoxAccountInformation.TabStop = false;
            this.groupBoxAccountInformation.Text = "Account information";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(16, 51);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 8;
            this.labelEmail.Text = "Email:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(16, 25);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Enabled = false;
            this.textBoxName.Location = new System.Drawing.Point(95, 22);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(300, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Enabled = false;
            this.textBoxEmail.Location = new System.Drawing.Point(95, 48);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(300, 20);
            this.textBoxEmail.TabIndex = 12;
            // 
            // AccountSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAccount);
            this.Name = "AccountSettingsPanel";
            this.Size = new System.Drawing.Size(700, 400);
            this.panelAccount.ResumeLayout(false);
            this.groupBoxAccountSetup.ResumeLayout(false);
            this.groupBoxAccountSetup.PerformLayout();
            this.groupBoxAccountInformation.ResumeLayout(false);
            this.groupBoxAccountInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelAccount;
        private System.Windows.Forms.GroupBox groupBoxAccountSetup;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Button buttonLinkAccount;
        private System.Windows.Forms.GroupBox groupBoxAccountInformation;
        private System.Windows.Forms.ComboBox comboBoxWorkspace;
        private System.Windows.Forms.Label labelApiKey;
        private System.Windows.Forms.Label labelWorkspace;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxEmail;
    }
}
