using System;
using System.Windows.Forms;
using TogglOutlookPlugIn.Synchronization;

namespace TogglOutlookPlugIn.Settings
{
    public partial class SyncPanel : UserControl
    {
        private bool isInitializing;

        public SyncPanel()
        {
            this.isInitializing = true;

            InitializeComponent();
            InitializeRadioButtons();

            this.isInitializing = false;
        }

        private void InitializeRadioButtons()
        {
            switch (SynchronizationService.Instance.SynchronizationOption)
            {
                case SyncOption.SyncCurrentDay:
                    this.radioButtonSyncCurrentDay.Checked = true;
                    return;

                case SyncOption.SyncLastSevenDays:
                    this.radioButtonSyncLastSevenDays.Checked = true;
                    return;

                default:
                    this.radioButtonNoSync.Checked = true;
                    return;
            }

        }

        private void OnRadioButtonSyncCheckedChanged(object sender, EventArgs e)
        {
            if (this.isInitializing)
            {
                return;
            }

            if (this.radioButtonSyncCurrentDay.Checked)
            {
                SynchronizationService.Instance.SynchronizationOption = SyncOption.SyncCurrentDay;
            }
            else if (this.radioButtonSyncLastSevenDays.Checked)
            {
                SynchronizationService.Instance.SynchronizationOption = SyncOption.SyncLastSevenDays;
            }
            else
            {
                SynchronizationService.Instance.SynchronizationOption = SyncOption.NoSync;
            }
        }
    }
}
