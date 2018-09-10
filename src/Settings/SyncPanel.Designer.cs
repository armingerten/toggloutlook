namespace TogglOutlookPlugIn.Settings
{
    partial class SyncPanel
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
            this.groupBoxSyncOption = new System.Windows.Forms.GroupBox();
            this.radioButtonSyncLastSevenDays = new System.Windows.Forms.RadioButton();
            this.radioButtonSyncCurrentDay = new System.Windows.Forms.RadioButton();
            this.radioButtonNoSync = new System.Windows.Forms.RadioButton();
            this.groupBoxSyncOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSyncOption
            // 
            this.groupBoxSyncOption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSyncOption.Controls.Add(this.radioButtonSyncLastSevenDays);
            this.groupBoxSyncOption.Controls.Add(this.radioButtonSyncCurrentDay);
            this.groupBoxSyncOption.Controls.Add(this.radioButtonNoSync);
            this.groupBoxSyncOption.Location = new System.Drawing.Point(12, 8);
            this.groupBoxSyncOption.Name = "groupBoxSyncOption";
            this.groupBoxSyncOption.Size = new System.Drawing.Size(676, 106);
            this.groupBoxSyncOption.TabIndex = 1;
            this.groupBoxSyncOption.TabStop = false;
            this.groupBoxSyncOption.Text = "Sync options";
            // 
            // radioButtonSyncLastSevenDays
            // 
            this.radioButtonSyncLastSevenDays.AutoSize = true;
            this.radioButtonSyncLastSevenDays.Location = new System.Drawing.Point(14, 73);
            this.radioButtonSyncLastSevenDays.Name = "radioButtonSyncLastSevenDays";
            this.radioButtonSyncLastSevenDays.Size = new System.Drawing.Size(159, 17);
            this.radioButtonSyncLastSevenDays.TabIndex = 4;
            this.radioButtonSyncLastSevenDays.TabStop = true;
            this.radioButtonSyncLastSevenDays.Text = "Synchronize last seven days";
            this.radioButtonSyncLastSevenDays.UseVisualStyleBackColor = true;
            this.radioButtonSyncLastSevenDays.CheckedChanged += new System.EventHandler(this.OnRadioButtonSyncCheckedChanged);
            // 
            // radioButtonSyncCurrentDay
            // 
            this.radioButtonSyncCurrentDay.AutoSize = true;
            this.radioButtonSyncCurrentDay.Location = new System.Drawing.Point(14, 50);
            this.radioButtonSyncCurrentDay.Name = "radioButtonSyncCurrentDay";
            this.radioButtonSyncCurrentDay.Size = new System.Drawing.Size(139, 17);
            this.radioButtonSyncCurrentDay.TabIndex = 3;
            this.radioButtonSyncCurrentDay.TabStop = true;
            this.radioButtonSyncCurrentDay.Text = "Synchronize current day";
            this.radioButtonSyncCurrentDay.UseVisualStyleBackColor = true;
            this.radioButtonSyncCurrentDay.CheckedChanged += new System.EventHandler(this.OnRadioButtonSyncCheckedChanged);
            // 
            // radioButtonNoSync
            // 
            this.radioButtonNoSync.AutoSize = true;
            this.radioButtonNoSync.Location = new System.Drawing.Point(14, 27);
            this.radioButtonNoSync.Name = "radioButtonNoSync";
            this.radioButtonNoSync.Size = new System.Drawing.Size(115, 17);
            this.radioButtonNoSync.TabIndex = 2;
            this.radioButtonNoSync.TabStop = true;
            this.radioButtonNoSync.Text = "No synchronization";
            this.radioButtonNoSync.UseVisualStyleBackColor = true;
            this.radioButtonNoSync.CheckedChanged += new System.EventHandler(this.OnRadioButtonSyncCheckedChanged);
            // 
            // SyncPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxSyncOption);
            this.Name = "SyncPanel";
            this.Size = new System.Drawing.Size(700, 400);
            this.groupBoxSyncOption.ResumeLayout(false);
            this.groupBoxSyncOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSyncOption;
        private System.Windows.Forms.RadioButton radioButtonSyncLastSevenDays;
        private System.Windows.Forms.RadioButton radioButtonSyncCurrentDay;
        private System.Windows.Forms.RadioButton radioButtonNoSync;
    }
}
