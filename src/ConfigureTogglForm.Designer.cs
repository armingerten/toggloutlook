namespace TogglOutlookPlugIn
{
    partial class ConfigureTogglForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureTogglForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.comboBoxWorkspace = new System.Windows.Forms.ComboBox();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.labelApiKey = new System.Windows.Forms.Label();
            this.labelWorkspace = new System.Windows.Forms.Label();
            this.buttonApplyApiKey = new System.Windows.Forms.Button();
            this.listViewCategories = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripListViewCategories = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCategories = new System.Windows.Forms.Label();
            this.labelNewCategory = new System.Windows.Forms.Label();
            this.comboBoxProjects = new System.Windows.Forms.ComboBox();
            this.comboBoxTags = new System.Windows.Forms.ComboBox();
            this.textBoxCategoryName = new System.Windows.Forms.TextBox();
            this.buttonAddCategory = new System.Windows.Forms.Button();
            this.linkLabelUrlToggl = new System.Windows.Forms.LinkLabel();
            this.contextMenuStripListViewCategories.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Location = new System.Drawing.Point(697, 464);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // comboBoxWorkspace
            // 
            this.comboBoxWorkspace.FormattingEnabled = true;
            this.comboBoxWorkspace.Location = new System.Drawing.Point(94, 38);
            this.comboBoxWorkspace.Name = "comboBoxWorkspace";
            this.comboBoxWorkspace.Size = new System.Drawing.Size(200, 21);
            this.comboBoxWorkspace.TabIndex = 2;
            this.comboBoxWorkspace.SelectedIndexChanged += new System.EventHandler(this.OnComboBoxWorkspaceSelectedIndexChanged);
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(94, 12);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(200, 20);
            this.textBoxApiKey.TabIndex = 3;
            // 
            // labelApiKey
            // 
            this.labelApiKey.AutoSize = true;
            this.labelApiKey.Location = new System.Drawing.Point(12, 15);
            this.labelApiKey.Name = "labelApiKey";
            this.labelApiKey.Size = new System.Drawing.Size(45, 13);
            this.labelApiKey.TabIndex = 4;
            this.labelApiKey.Text = "API Key";
            // 
            // labelWorkspace
            // 
            this.labelWorkspace.AutoSize = true;
            this.labelWorkspace.Location = new System.Drawing.Point(12, 41);
            this.labelWorkspace.Name = "labelWorkspace";
            this.labelWorkspace.Size = new System.Drawing.Size(62, 13);
            this.labelWorkspace.TabIndex = 5;
            this.labelWorkspace.Text = "Workspace";
            // 
            // buttonApplyApiKey
            // 
            this.buttonApplyApiKey.Location = new System.Drawing.Point(300, 12);
            this.buttonApplyApiKey.Name = "buttonApplyApiKey";
            this.buttonApplyApiKey.Size = new System.Drawing.Size(75, 23);
            this.buttonApplyApiKey.TabIndex = 6;
            this.buttonApplyApiKey.Text = "Apply";
            this.buttonApplyApiKey.UseVisualStyleBackColor = true;
            this.buttonApplyApiKey.Click += new System.EventHandler(this.OnButtonApplyApiKeyClick);
            // 
            // listViewCategories
            // 
            this.listViewCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderProject,
            this.columnHeaderTag});
            this.listViewCategories.ContextMenuStrip = this.contextMenuStripListViewCategories;
            this.listViewCategories.FullRowSelect = true;
            this.listViewCategories.GridLines = true;
            this.listViewCategories.Location = new System.Drawing.Point(94, 67);
            this.listViewCategories.MultiSelect = false;
            this.listViewCategories.Name = "listViewCategories";
            this.listViewCategories.Size = new System.Drawing.Size(678, 301);
            this.listViewCategories.TabIndex = 7;
            this.listViewCategories.UseCompatibleStateImageBehavior = false;
            this.listViewCategories.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderProject
            // 
            this.columnHeaderProject.Text = "Project";
            this.columnHeaderProject.Width = 200;
            // 
            // columnHeaderTag
            // 
            this.columnHeaderTag.Text = "Tag / Label";
            this.columnHeaderTag.Width = 200;
            // 
            // contextMenuStripListViewCategories
            // 
            this.contextMenuStripListViewCategories.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteCategory});
            this.contextMenuStripListViewCategories.Name = "contextMenuStripListViewCategories";
            this.contextMenuStripListViewCategories.Size = new System.Drawing.Size(157, 26);
            // 
            // toolStripMenuItemDeleteCategory
            // 
            this.toolStripMenuItemDeleteCategory.Name = "toolStripMenuItemDeleteCategory";
            this.toolStripMenuItemDeleteCategory.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemDeleteCategory.Text = "Delete category";
            this.toolStripMenuItemDeleteCategory.Click += new System.EventHandler(this.OnToolStripMenuItemDeleteCategoryClick);
            // 
            // labelCategories
            // 
            this.labelCategories.AutoSize = true;
            this.labelCategories.Location = new System.Drawing.Point(12, 67);
            this.labelCategories.Name = "labelCategories";
            this.labelCategories.Size = new System.Drawing.Size(57, 13);
            this.labelCategories.TabIndex = 8;
            this.labelCategories.Text = "Categories";
            // 
            // labelNewCategory
            // 
            this.labelNewCategory.AutoSize = true;
            this.labelNewCategory.Location = new System.Drawing.Point(12, 387);
            this.labelNewCategory.Name = "labelNewCategory";
            this.labelNewCategory.Size = new System.Drawing.Size(76, 13);
            this.labelNewCategory.TabIndex = 9;
            this.labelNewCategory.Text = "New category:";
            // 
            // comboBoxProjects
            // 
            this.comboBoxProjects.FormattingEnabled = true;
            this.comboBoxProjects.Location = new System.Drawing.Point(94, 410);
            this.comboBoxProjects.Name = "comboBoxProjects";
            this.comboBoxProjects.Size = new System.Drawing.Size(200, 21);
            this.comboBoxProjects.TabIndex = 10;
            // 
            // comboBoxTags
            // 
            this.comboBoxTags.FormattingEnabled = true;
            this.comboBoxTags.Location = new System.Drawing.Point(94, 437);
            this.comboBoxTags.Name = "comboBoxTags";
            this.comboBoxTags.Size = new System.Drawing.Size(200, 21);
            this.comboBoxTags.TabIndex = 11;
            // 
            // textBoxCategoryName
            // 
            this.textBoxCategoryName.Location = new System.Drawing.Point(94, 384);
            this.textBoxCategoryName.Name = "textBoxCategoryName";
            this.textBoxCategoryName.Size = new System.Drawing.Size(200, 20);
            this.textBoxCategoryName.TabIndex = 12;
            // 
            // buttonAddCategory
            // 
            this.buttonAddCategory.Location = new System.Drawing.Point(94, 464);
            this.buttonAddCategory.Name = "buttonAddCategory";
            this.buttonAddCategory.Size = new System.Drawing.Size(200, 23);
            this.buttonAddCategory.TabIndex = 13;
            this.buttonAddCategory.Text = "Add category";
            this.buttonAddCategory.UseVisualStyleBackColor = true;
            this.buttonAddCategory.Click += new System.EventHandler(this.OnButtonAddCategoryClick);
            // 
            // linkLabelUrlToggl
            // 
            this.linkLabelUrlToggl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelUrlToggl.AutoSize = true;
            this.linkLabelUrlToggl.Location = new System.Drawing.Point(683, 15);
            this.linkLabelUrlToggl.Name = "linkLabelUrlToggl";
            this.linkLabelUrlToggl.Size = new System.Drawing.Size(89, 13);
            this.linkLabelUrlToggl.TabIndex = 14;
            this.linkLabelUrlToggl.TabStop = true;
            this.linkLabelUrlToggl.Text = "https://toggl.com";
            // 
            // ConfigureTogglForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(784, 499);
            this.Controls.Add(this.linkLabelUrlToggl);
            this.Controls.Add(this.buttonAddCategory);
            this.Controls.Add(this.textBoxCategoryName);
            this.Controls.Add(this.comboBoxTags);
            this.Controls.Add(this.comboBoxProjects);
            this.Controls.Add(this.labelNewCategory);
            this.Controls.Add(this.labelCategories);
            this.Controls.Add(this.listViewCategories);
            this.Controls.Add(this.buttonApplyApiKey);
            this.Controls.Add(this.labelWorkspace);
            this.Controls.Add(this.labelApiKey);
            this.Controls.Add(this.textBoxApiKey);
            this.Controls.Add(this.comboBoxWorkspace);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureTogglForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Configure Toggl";
            this.contextMenuStripListViewCategories.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ComboBox comboBoxWorkspace;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label labelApiKey;
        private System.Windows.Forms.Label labelWorkspace;
        private System.Windows.Forms.Button buttonApplyApiKey;
        private System.Windows.Forms.ListView listViewCategories;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.Label labelCategories;
        private System.Windows.Forms.ColumnHeader columnHeaderProject;
        private System.Windows.Forms.ColumnHeader columnHeaderTag;
        private System.Windows.Forms.Label labelNewCategory;
        private System.Windows.Forms.ComboBox comboBoxProjects;
        private System.Windows.Forms.ComboBox comboBoxTags;
        private System.Windows.Forms.TextBox textBoxCategoryName;
        private System.Windows.Forms.Button buttonAddCategory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListViewCategories;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteCategory;
        private System.Windows.Forms.LinkLabel linkLabelUrlToggl;
    }
}