using System;
using System.Drawing;
using System.Windows.Forms;
using TogglOutlookPlugIn.Services;

namespace TogglOutlookPlugIn.Settings
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            this.InitializeComponent();

            this.buttonCategoriesNavigation.Visible = this.Toggl.IsLinkEstablished;
            this.buttonSyncNavigation.Visible = this.Toggl.IsLinkEstablished;
            this.Toggl.LinkStateChangedEvent += this.OnTogglLinkEstablishedEvent;
            this.OnButtonAccountNavigationClick(this, EventArgs.Empty);
        }

        ~SettingsDialog()
        {
            this.Toggl.LinkStateChangedEvent -= this.OnTogglLinkEstablishedEvent;
        }

        private TogglService Toggl => TogglService.Instance;

        private void OnTogglLinkEstablishedEvent(object sender, EventArgs e)
        {
            this.buttonCategoriesNavigation.Visible = this.Toggl.IsLinkEstablished;
            this.buttonSyncNavigation.Visible = this.Toggl.IsLinkEstablished;
            this.panelCategories.PopulateComboBoxesProjectsAndTags();
        }

        private void OnButtonAccountNavigationClick(object sender, EventArgs e)
        {
            this.panelAccountSettings.Visible = true;
            this.buttonAccountNavigation.BackColor = Color.WhiteSmoke;
            this.panelCategories.Visible = false;
            this.buttonCategoriesNavigation.BackColor = Color.White;
            this.panelAbout.Visible = false;
            this.buttonAboutNavigation.BackColor = Color.White;
            this.panelSync.Visible = false;
            this.buttonSyncNavigation.BackColor = Color.White;
        }

        private void OnButtonCategoryNavigationClick(object sender, EventArgs e)
        {
            this.panelAccountSettings.Visible = false;
            this.buttonAccountNavigation.BackColor = Color.White;
            this.panelCategories.Visible = true;
            this.buttonCategoriesNavigation.BackColor = Color.WhiteSmoke;
            this.panelAbout.Visible = false;
            this.buttonAboutNavigation.BackColor = Color.White;
            this.panelSync.Visible = false;
            this.buttonSyncNavigation.BackColor = Color.White;
        }

        private void OnButtonAboutNavigationClick(object sender, EventArgs e)
        {
            this.panelAccountSettings.Visible = false;
            this.buttonAccountNavigation.BackColor = Color.White;
            this.panelCategories.Visible = false;
            this.buttonCategoriesNavigation.BackColor = Color.White;
            this.panelAbout.Visible = true;
            this.buttonAboutNavigation.BackColor = Color.WhiteSmoke;
            this.panelSync.Visible = false;
            this.buttonSyncNavigation.BackColor = Color.White;
        }

        private void OnButtonSyncNavigationClick(object sender, EventArgs e)
        {
            this.panelAccountSettings.Visible = false;
            this.buttonAccountNavigation.BackColor = Color.White;
            this.panelCategories.Visible = false;
            this.buttonCategoriesNavigation.BackColor = Color.White;
            this.panelAbout.Visible = false;
            this.buttonAboutNavigation.BackColor = Color.White;
            this.panelSync.Visible = true;
            this.buttonSyncNavigation.BackColor = Color.WhiteSmoke;
        }
    }
}
