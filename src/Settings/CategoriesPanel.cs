using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TogglOutlookPlugIn.Models;
using TogglOutlookPlugIn.Services;
using TogglApi = Toggl.Api.DataObjects;

namespace TogglOutlookPlugIn.Settings
{
    public partial class CategoriesPanel : UserControl
    {
        public CategoriesPanel()
        {
            this.InitializeComponent();

            this.PopulateComboBoxesProjectsAndTags();
            this.PopulateListViewCategories();
        }

        private TogglService Toggl => TogglService.Instance;

        private CategoryService CategoryService => CategoryService.Instance;

        public void PopulateComboBoxesProjectsAndTags()
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
            foreach (Category category in this.CategoryService.Categories)
            {
                ListViewItem listViewItem = new ListViewItem(category.Name)
                {
                    Tag = category
                };

                listViewItem.SubItems.Add(this.GetProjectName(category.ProjectId));
                listViewItem.SubItems.Add(this.GetTagName(category.TagId));
                if (category.IsOutlookOnly)
                {
                    listViewItem.ForeColor = Color.Gray;
                }
                else if (category.IsOutlookCategoryMissing)
                {
                    listViewItem.ForeColor = Color.Red;
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

        private bool TryGetSelectedCategory(out Category selectedCategory)
        {
            if (this.listViewCategories.SelectedItems.Count == 1
                && this.listViewCategories.SelectedItems[0].Tag is Category selectedItemTag)
            {
                selectedCategory = selectedItemTag;
                return true;
            }
            else
            {
                selectedCategory = null;
                return false;
            }
        }

        private void OnButtonAddCategoryClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBoxCategoryName.Text))
            {
                return;
            }

            if (this.CategoryService.TryAddCategory(
                (int)this.comboBoxProjects.SelectedValue,
                (int)this.comboBoxTags.SelectedValue,
                this.textBoxCategoryName.Text))
            {
                this.PopulateListViewCategories();
            }
        }

        private void OnToolStripMenuItemDeleteCategoryClick(object sender, EventArgs e)
        {
            if (this.TryGetSelectedCategory(out Category selectedCategory))
            {
                this.CategoryService.RemoveCategory(selectedCategory);
                this.PopulateListViewCategories();
            }
        }

        private void OnListViewCategoriesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TryGetSelectedCategory(out Category selectedCategory))
            {
                this.textBoxCategoryName.Text = selectedCategory.Name;

                if (!selectedCategory.IsOutlookOnly)
                {
                    int projectIndex = this.comboBoxProjects.FindStringExact(this.GetProjectName(selectedCategory.ProjectId));
                    if (projectIndex > -1)
                    {
                        this.comboBoxProjects.SelectedIndex = projectIndex;
                    }

                    int tagIndex = this.comboBoxTags.FindStringExact(this.GetTagName(selectedCategory.TagId));
                    if (tagIndex > -1)
                    {
                        this.comboBoxTags.SelectedIndex = tagIndex;
                    }
                }
            }
        }
    }
}
