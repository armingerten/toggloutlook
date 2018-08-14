using System;
using System.Linq;
using System.Windows.Forms;
using TogglOutlookPlugIn.Categories;
using TogglApi = Toggl.Api.DataObjects;

namespace TogglOutlookPlugIn
{
    public partial class ConfigureTogglForm : Form
    {
        public ConfigureTogglForm()
        {
            this.InitializeComponent();

            this.PopulateForm();
        }

        private CategoryManager CategoryManager => CategoryManager.Instance;

        private TogglService Toggl => TogglService.Instance;

        private void PopulateForm()
        {
            this.textBoxApiKey.Text = this.Toggl.ApiKey;

            this.comboBoxWorkspace.DataSource = this.Toggl.Workspaces;
            this.comboBoxWorkspace.DisplayMember = nameof(TogglApi.Workspace.Name);
            this.comboBoxWorkspace.ValueMember = nameof(TogglApi.Workspace.Id);

            this.PopulateComboBoxesProjectsAndTags();
            this.PopulateListViewCategories();
        }

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
        {
            string name = this.Toggl.Projects.FirstOrDefault(project => project.Id == projectId)?.Name;
            if (name != null)
            {
                return name;
            }
            else if (projectId == default(int))
            {
                return string.Empty;
            }
            else
            {
                return projectId.ToString();
            }
        }

        private string GetTagName(int tagId)
        {
            string name = this.Toggl.Tags.FirstOrDefault(tag => tag.Id == tagId)?.Name;

            if (name != null)
            {
                return name;
            }
            else if (tagId == default(int))
            {
                return string.Empty;
            }
            else
            {
                return tagId.ToString();
            }
        }

        private void OnButtonApplyApiKeyClick(object sender, EventArgs e)
        {
            if (this.Toggl.TryEstablishLink(this.textBoxApiKey.Text))
            {
                this.PopulateForm();
            }
        }

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

        private void OnComboBoxWorkspaceSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxWorkspace.SelectedValue is int workspaceId && this.Toggl.TrySwitchWorkspace(workspaceId))
            {
                this.PopulateComboBoxesProjectsAndTags();
            }
        }

        private void OnToolStripMenuItemDeleteCategoryClick(object sender, EventArgs e)
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
