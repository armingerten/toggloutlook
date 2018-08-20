namespace TogglOutlookPlugIn.Settings
{
    partial class SettingsDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.buttonSyncNavigation = new System.Windows.Forms.Button();
            this.buttonCategoriesNavigation = new System.Windows.Forms.Button();
            this.buttonAboutNavigation = new System.Windows.Forms.Button();
            this.buttonAccountNavigation = new System.Windows.Forms.Button();
            this.panelCategories = new TogglOutlookPlugIn.Settings.CategoriesPanel();
            this.panelAbout = new TogglOutlookPlugIn.Settings.AboutPanel();
            this.panelAccountSettings = new TogglOutlookPlugIn.Settings.AccountSettingsPanel();
            this.panelSync = new TogglOutlookPlugIn.Settings.SyncPanel();
            this.panelNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNavigation
            // 
            this.panelNavigation.BackColor = System.Drawing.Color.White;
            this.panelNavigation.Controls.Add(this.buttonSyncNavigation);
            this.panelNavigation.Controls.Add(this.buttonCategoriesNavigation);
            this.panelNavigation.Controls.Add(this.buttonAboutNavigation);
            this.panelNavigation.Controls.Add(this.buttonAccountNavigation);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavigation.Location = new System.Drawing.Point(0, 0);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(784, 60);
            this.panelNavigation.TabIndex = 0;
            // 
            // buttonSyncNavigation
            // 
            this.buttonSyncNavigation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSyncNavigation.FlatAppearance.BorderSize = 0;
            this.buttonSyncNavigation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSyncNavigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSyncNavigation.Image = global::TogglOutlookPlugIn.Properties.Resources.reload32;
            this.buttonSyncNavigation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSyncNavigation.Location = new System.Drawing.Point(153, 0);
            this.buttonSyncNavigation.Name = "buttonSyncNavigation";
            this.buttonSyncNavigation.Size = new System.Drawing.Size(75, 58);
            this.buttonSyncNavigation.TabIndex = 20;
            this.buttonSyncNavigation.Text = "Sync";
            this.buttonSyncNavigation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSyncNavigation.UseVisualStyleBackColor = true;
            this.buttonSyncNavigation.Visible = false;
            this.buttonSyncNavigation.Click += new System.EventHandler(this.OnButtonSyncNavigationClick);
            // 
            // buttonCategoriesNavigation
            // 
            this.buttonCategoriesNavigation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCategoriesNavigation.FlatAppearance.BorderSize = 0;
            this.buttonCategoriesNavigation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCategoriesNavigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCategoriesNavigation.Image = global::TogglOutlookPlugIn.Properties.Resources.category32;
            this.buttonCategoriesNavigation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonCategoriesNavigation.Location = new System.Drawing.Point(78, 0);
            this.buttonCategoriesNavigation.Name = "buttonCategoriesNavigation";
            this.buttonCategoriesNavigation.Size = new System.Drawing.Size(75, 58);
            this.buttonCategoriesNavigation.TabIndex = 19;
            this.buttonCategoriesNavigation.Text = "Categories";
            this.buttonCategoriesNavigation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCategoriesNavigation.UseVisualStyleBackColor = true;
            this.buttonCategoriesNavigation.Visible = false;
            this.buttonCategoriesNavigation.Click += new System.EventHandler(this.OnButtonCategoryNavigationClick);
            // 
            // buttonAboutNavigation
            // 
            this.buttonAboutNavigation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAboutNavigation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAboutNavigation.FlatAppearance.BorderSize = 0;
            this.buttonAboutNavigation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAboutNavigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAboutNavigation.Image = global::TogglOutlookPlugIn.Properties.Resources.info32;
            this.buttonAboutNavigation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAboutNavigation.Location = new System.Drawing.Point(706, 0);
            this.buttonAboutNavigation.Name = "buttonAboutNavigation";
            this.buttonAboutNavigation.Size = new System.Drawing.Size(75, 58);
            this.buttonAboutNavigation.TabIndex = 18;
            this.buttonAboutNavigation.Text = "About";
            this.buttonAboutNavigation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAboutNavigation.UseVisualStyleBackColor = true;
            this.buttonAboutNavigation.Click += new System.EventHandler(this.OnButtonAboutNavigationClick);
            // 
            // buttonAccountNavigation
            // 
            this.buttonAccountNavigation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAccountNavigation.FlatAppearance.BorderSize = 0;
            this.buttonAccountNavigation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAccountNavigation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAccountNavigation.Image = global::TogglOutlookPlugIn.Properties.Resources.user32;
            this.buttonAccountNavigation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAccountNavigation.Location = new System.Drawing.Point(3, 0);
            this.buttonAccountNavigation.Name = "buttonAccountNavigation";
            this.buttonAccountNavigation.Size = new System.Drawing.Size(75, 58);
            this.buttonAccountNavigation.TabIndex = 16;
            this.buttonAccountNavigation.Text = "Account";
            this.buttonAccountNavigation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAccountNavigation.UseVisualStyleBackColor = true;
            this.buttonAccountNavigation.Click += new System.EventHandler(this.OnButtonAccountNavigationClick);
            // 
            // panelCategories
            // 
            this.panelCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCategories.Location = new System.Drawing.Point(0, 60);
            this.panelCategories.Name = "panelCategories";
            this.panelCategories.Size = new System.Drawing.Size(784, 401);
            this.panelCategories.TabIndex = 4;
            // 
            // panelAbout
            // 
            this.panelAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAbout.Location = new System.Drawing.Point(0, 60);
            this.panelAbout.Name = "panelAbout";
            this.panelAbout.Size = new System.Drawing.Size(784, 401);
            this.panelAbout.TabIndex = 3;
            // 
            // panelAccountSettings
            // 
            this.panelAccountSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAccountSettings.Location = new System.Drawing.Point(0, 60);
            this.panelAccountSettings.Name = "panelAccountSettings";
            this.panelAccountSettings.Size = new System.Drawing.Size(784, 401);
            this.panelAccountSettings.TabIndex = 1;
            // 
            // panelSync
            // 
            this.panelSync.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSync.Location = new System.Drawing.Point(0, 60);
            this.panelSync.Name = "panelSync";
            this.panelSync.Size = new System.Drawing.Size(784, 401);
            this.panelSync.TabIndex = 5;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.panelAccountSettings);
            this.Controls.Add(this.panelSync);
            this.Controls.Add(this.panelCategories);
            this.Controls.Add(this.panelAbout);
            this.Controls.Add(this.panelNavigation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "SettingsDialog";
            this.Text = "Configure Toggl integration";
            this.panelNavigation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button buttonAccountNavigation;
        private System.Windows.Forms.Button buttonAboutNavigation;
        private System.Windows.Forms.Button buttonCategoriesNavigation;
        private AccountSettingsPanel panelAccountSettings;
        private AboutPanel panelAbout;
        private CategoriesPanel panelCategories;
        private System.Windows.Forms.Button buttonSyncNavigation;
        private SyncPanel panelSync;
    }
}