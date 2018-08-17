using System;
using System.Drawing;
using System.Windows.Forms;

namespace TogglOutlookPlugIn.Settings
{
    public partial class AboutPanel : UserControl
    {
        public AboutPanel()
        {
            InitializeComponent();
        }

        private void OnAboutPanelResize(object sender, EventArgs e)
        {
            int panelLocationX = (this.Size.Width - this.groupBoxAbout.Width) / 2;
            int panelLocationY = (this.Size.Height - this.groupBoxAbout.Size.Height) / 2;

            this.groupBoxAbout.Location = new Point(panelLocationX, panelLocationY);
        }
    }
}
