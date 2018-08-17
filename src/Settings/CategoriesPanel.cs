using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TogglApi = Toggl.Api.DataObjects;
using TogglOutlookPlugIn.Categories;

namespace TogglOutlookPlugIn.Settings
{
    public partial class CategoriesPanel : UserControl
    {
        public CategoriesPanel()
        {
            InitializeComponent();

            this.PopulateComboBoxesProjectsAndTags();
            this.PopulateListViewCategories();
        }

        private TogglService Toggl => TogglService.Instance;

        private CategoryManager CategoryManager => CategoryManager.Instance;

        private void PopulateComboBoxesProjectsAndTags()
        {
            // Projects
            this.comboBoxProjects.DataSource = this.Toggl.Projects;
            this.comboBoxProjects.DisplayMember = nameof(TogglApi.Project.Name);
            this.comboBoxProjects.ValueMember = nameof(TogglApi.Project.Id);

            // Tags
            this.comboBoxTags.DataSource = this.Toggl.Tags;
            this.comboBoxTags.DisplayMember = nameof(TogglApi.Tag.Name);
            this.comboBoxTags.ValueMember = nameof(TogglApi.Tag.Id);
        }

        private void PopulateListViewCategories()
        {
            this.listViewCategories.Items.Clear();
            foreach (Category category in this.CategoryManager.Categories)
            {
                ListViewItem listViewItem = new ListViewItem(category.Name)
                {
                    Tag = category
                };

                listViewItem.SubItems.Add(this.GetProjectName(category.ProjectId));
                listViewItem.SubItems.Add(this.GetTagName(category.TagId));
                if (category.IsOutlookOnly)
                {
                    listViewItem.ForeColor = System.Drawing.Color.Gray;
                }
                else if (category.IsOutlookCategoryMissing)
                {
                    listViewItem.ForeColor = System.Drawing.Color.Red;
                }

                this.listViewCategories.Items.Add(listViewItem);
            }
        }

        private string GetProjectName(int projectId)
            => this.Toggl.Projects.FirstOrDefault(project => project.Id == projectId)?.Name
            ?? (projectId == default(int) ? string.Empty : projectId.ToString());

        private string GetTagName(int tagId)
            => this.Toggl.Tags.FirstOrDefault(tag => tag.Id == tagId)?.Name
            ?? (tagId == default(int) ? string.Empty : tagId.ToString());

        private void OnButtonAddCategoryClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBoxCategoryName.Text))
            {
                return;
            }

            if (this.CategoryManager.TryAddCategory(
                (int)this.comboBoxProjects.SelectedValue,
                (int)this.comboBoxTags.SelectedValue,
                this.textBoxCategoryName.Text))
            {
                this.PopulateListViewCategories();
            }
        }

        private void OnToolStripMenuItemDeleteCategoryClick
            (object sender, EventArgs e)
        {
            if (this.listViewCategories.SelectedItems.Count < 1)
            {
                return;
            }

            if ((this.listViewCategories.SelectedItems[0].Tag is Category selectedCategory))
            {
                this.CategoryManager.RemoveCategory(selectedCategory);
                this.PopulateListViewCategories();
            }
        }
    }
}
