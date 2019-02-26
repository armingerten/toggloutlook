using System;
using System.Collections.Generic;
using System.Timers;
using TogglOutlookPlugIn.Models;
using TogglOutlookPlugIn.Services;

namespace TogglOutlookPlugIn.Synchronization
{
    public class SynchronizationService
    {
        private static SynchronizationService instance;
        private static readonly double timeoutTimerFrequencyInMs = 1 * 60 * 1000d;

        private readonly Timer synchronizationTimer;
        private SyncOption syncOption;
        private bool isOutlookAuthority;

        public SynchronizationService()
        {
            this.syncOption = (SyncOption)Properties.Settings.Default.SyncOption;
            this.isOutlookAuthority = Properties.Settings.Default.IsOutlookAuthority;

            this.synchronizationTimer = new Timer(timeoutTimerFrequencyInMs);
            this.synchronizationTimer.Elapsed += this.OnSynchronizationTimerElapsed;
        }

        public static SynchronizationService Instance
            => instance ?? (instance = new SynchronizationService());

        public SyncOption SynchronizationOption
        {
            get => this.syncOption;
            set => this.AdaptScheduling(value);
        }

        public bool IsOutlookAuthority
        {
            get => this.isOutlookAuthority;
            set
            {
                this.isOutlookAuthority = value;
                Properties.Settings.Default.IsOutlookAuthority = value;
                Properties.Settings.Default.Save();
            }
        }

        public void Start()
        {
            if (!this.synchronizationTimer.Enabled && this.syncOption != SyncOption.NoSync)
            {
                this.synchronizationTimer.Start();
            }
        }

        private TogglService Toggl
            => TogglService.Instance;

        private void AdaptScheduling(SyncOption syncOption)
        {
            this.syncOption = syncOption;
            Properties.Settings.Default.SyncOption = (int)syncOption;
            Properties.Settings.Default.Save();

            switch (syncOption)
            {
                case SyncOption.SyncCurrentDay:
                case SyncOption.SyncLastSevenDays:
                    if (!this.synchronizationTimer.Enabled)
                    {
                        this.synchronizationTimer.Start();
                    }
                    return;

                default:
                    this.synchronizationTimer.Stop();
                    break;
            }
        }

        public void SynchronizeWithToggl()
        {
            DateTime startTime;
            DateTime endTime = DateTime.Now.Date.AddDays(1);
            switch (this.syncOption)
            {
                case SyncOption.SyncCurrentDay:
                    startTime = DateTime.Now.Date;
                    break;
                case SyncOption.SyncLastSevenDays:
                    startTime = DateTime.Now.Date.AddDays(-6);
                    break;
                default:
                    return;
            }

            List<CategorizedAppointment> categorizedAppointments = Calendar.GetAppointmentsBetween(startTime, endTime);
            if (this.IsOutlookAuthority)
            {
                this.Toggl.CreateOrUpdateAppointments(categorizedAppointments, startTime, endTime);
            }
            else
            {
                this.Toggl.CreateAppointmentsIfSlotIsFree(categorizedAppointments);
            }
        }

        private void OnSynchronizationTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.synchronizationTimer.Stop();
                this.SynchronizeWithToggl();
            }
            catch (Exception)
            {
                // No logging in place currently
            }
            finally
            {
                this.synchronizationTimer.Start();
            }
        }
    }
}
