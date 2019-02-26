namespace TogglOutlookPlugIn.Settings
{
    partial class CategoriesPanel
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxCategories = new System.Windows.Forms.GroupBox();
            this.listViewCategories = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripListViewCategories = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxNewCategory = new System.Windows.Forms.GroupBox();
            this.labelProject = new System.Windows.Forms.Label();
            this.labelCategoryName = new System.Windows.Forms.Label();
            this.buttonAddCategory = new System.Windows.Forms.Button();
            this.textBoxCategoryName = new System.Windows.Forms.TextBox();
            this.comboBoxProjects = new System.Windows.Forms.ComboBox();
            this.groupBoxCategories.SuspendLayout();
            this.contextMenuStripListViewCategories.SuspendLayout();
            this.groupBoxNewCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCategories
            // 
            this.groupBoxCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCategories.Controls.Add(this.listViewCategories);
            this.groupBoxCategories.Location = new System.Drawing.Point(12, 8);
            this.groupBoxCategories.Name = "groupBoxCategories";
            this.groupBoxCategories.Size = new System.Drawing.Size(676, 294);
            this.groupBoxCategories.TabIndex = 16;
            this.groupBoxCategories.TabStop = false;
            this.groupBoxCategories.Text = "All Categories";
            // 
            // listViewCategories
            // 
            this.listViewCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderProject,
            this.columnHeaderTag});
            this.listViewCategories.ContextMenuStrip = this.contextMenuStripListViewCategories;
            this.listViewCategories.FullRowSelect = true;
            this.listViewCategories.GridLines = true;
            this.listViewCategories.Location = new System.Drawing.Point(6, 19);
            this.listViewCategories.MultiSelect = false;
            this.listViewCategories.Name = "listViewCategories";
            this.listViewCategories.Size = new System.Drawing.Size(664, 269);
            this.listViewCategories.TabIndex = 8;
            this.listViewCategories.TabStop = false;
            this.listViewCategories.UseCompatibleStateImageBehavior = false;
            this.listViewCategories.View = System.Windows.Forms.View.Details;
            this.listViewCategories.SelectedIndexChanged += new System.EventHandler(this.OnListViewCategoriesSelectedIndexChanged);
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
            this.toolStripMenuItemDeleteCategory.Image = global::TogglOutlookPlugIn.Properties.Resources.trash32;
            this.toolStripMenuItemDeleteCategory.Name = "toolStripMenuItemDeleteCategory";
            this.toolStripMenuItemDeleteCategory.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItemDeleteCategory.Text = "Delete category";
            this.toolStripMenuItemDeleteCategory.Click += new System.EventHandler(this.OnToolStripMenuItemDeleteCategoryClick);
            // 
            // groupBoxNewCategory
            // 
            this.groupBoxNewCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNewCategory.Controls.Add(this.labelProject);
            this.groupBoxNewCategory.Controls.Add(this.labelCategoryName);
            this.groupBoxNewCategory.Controls.Add(this.buttonAddCategory);
            this.groupBoxNewCategory.Controls.Add(this.textBoxCategoryName);
            this.groupBoxNewCategory.Controls.Add(this.comboBoxProjects);
            this.groupBoxNewCategory.Location = new System.Drawing.Point(12, 308);
            this.groupBoxNewCategory.Name = "groupBoxNewCategory";
            this.groupBoxNewCategory.Size = new System.Drawing.Size(676, 80);
            this.groupBoxNewCategory.TabIndex = 17;
            this.groupBoxNewCategory.TabStop = false;
            this.groupBoxNewCategory.Text = "New Category";
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Location = new System.Drawing.Point(16, 48);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(40, 13);
            this.labelProject.TabIndex = 19;
            this.labelProject.Text = "Project";
            // 
            // labelCategoryName
            // 
            this.labelCategoryName.AutoSize = true;
            this.labelCategoryName.Location = new System.Drawing.Point(16, 22);
            this.labelCategoryName.Name = "labelCategoryName";
            this.labelCategoryName.Size = new System.Drawing.Size(35, 13);
            this.labelCategoryName.TabIndex = 18;
            this.labelCategoryName.Text = "Name";
            // 
            // buttonAddCategory
            // 
            this.buttonAddCategory.Location = new System.Drawing.Point(534, 43);
            this.buttonAddCategory.Name = "buttonAddCategory";
            this.buttonAddCategory.Size = new System.Drawing.Size(136, 23);
            this.buttonAddCategory.TabIndex = 17;
            this.buttonAddCategory.Text = "Add category";
            this.buttonAddCategory.UseVisualStyleBackColor = true;
            this.buttonAddCategory.Click += new System.EventHandler(this.OnButtonAddCategoryClick);
            // 
            // textBoxCategoryName
            // 
            this.textBoxCategoryName.Location = new System.Drawing.Point(95, 19);
            this.textBoxCategoryName.Name = "textBoxCategoryName";
            this.textBoxCategoryName.Size = new System.Drawing.Size(433, 20);
            this.textBoxCategoryName.TabIndex = 16;
            // 
            // comboBoxProjects
            // 
            this.comboBoxProjects.FormattingEnabled = true;
            this.comboBoxProjects.Location = new System.Drawing.Point(95, 45);
            this.comboBoxProjects.Name = "comboBoxProjects";
            this.comboBoxProjects.Size = new System.Drawing.Size(433, 21);
            this.comboBoxProjects.TabIndex = 14;
            // 
            // CategoriesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxNewCategory);
            this.Controls.Add(this.groupBoxCategories);
            this.Name = "CategoriesPanel";
            this.Size = new System.Drawing.Size(700, 400);
            this.groupBoxCategories.ResumeLayout(false);
            this.contextMenuStripListViewCategories.ResumeLayout(false);
            this.groupBoxNewCategory.ResumeLayout(false);
            this.groupBoxNewCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCategories;
        private System.Windows.Forms.GroupBox groupBoxNewCategory;
        private System.Windows.Forms.Button buttonAddCategory;
        private System.Windows.Forms.TextBox textBoxCategoryName;
        private System.Windows.Forms.ComboBox comboBoxProjects;
        private System.Windows.Forms.Label labelCategoryName;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.ListView listViewCategories;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderProject;
        private System.Windows.Forms.ColumnHeader columnHeaderTag;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListViewCategories;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteCategory;
    }
}
