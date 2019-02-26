using System;
using System.Windows.Forms;
using Toggl.Api.DataObjects;
using TogglOutlookPlugIn.Services;

namespace TogglOutlookPlugIn.Settings
{
    public partial class AccountSettingsPanel : UserControl
    {
        public AccountSettingsPanel()
        {
            this.InitializeComponent();

            this.PopulateForm();
        }

        private TogglService Toggl => TogglService.Instance;

        private void OnButtonLinkAccountClick(object sender, EventArgs e)
        {
            if (this.Toggl.TryEstablishLink(this.textBoxApiKey.Text))
            {
                this.PopulateForm();
            }
        }

        private void PopulateForm()
        {
            this.textBoxApiKey.Text = this.Toggl.ApiKey;

            this.comboBoxWorkspace.DataSource = this.Toggl.Workspaces;
            this.comboBoxWorkspace.DisplayMember = nameof(Workspace.Name);
            this.comboBoxWorkspace.ValueMember = nameof(Workspace.Id);
            if (this.Toggl.CurrentWorkspace != null)
            {
                this.comboBoxWorkspace.SelectedValue = this.Toggl.CurrentWorkspace.Id;
            }

            if (this.Toggl.CurrentUser != null)
            {
                this.textBoxName.Text = this.Toggl.CurrentUser.FullName;
                this.textBoxEmail.Text = this.Toggl.CurrentUser.Email;
                this.groupBoxAccountInformation.Visible = true;
            }
            else
            {
                this.groupBoxAccountInformation.Visible = false;
            }
        }

        private void OnComboBoxWorkspaceSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxWorkspace.SelectedValue is int workspaceId)
            {
                this.Toggl.TrySwitchWorkspace(workspaceId);
            }
        }
    }
}
