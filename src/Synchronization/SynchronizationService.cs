using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Timers;

namespace TogglOutlookPlugIn.Synchronization
{
    public class SynchronizationService
    {
        private static SynchronizationService instance;
        private static readonly double timeoutTimerFrequencyInMs = 10 * 60 * 1000d;

        private readonly Timer synchronizationTimer;
        private SyncOption syncOption;

        public SynchronizationService()
        {
            this.syncOption = (SyncOption)Properties.Settings.Default.SyncOption;

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

        private void SynchronizeWithToggl()
        {
            var newTimeEntries = new List<(string description, DateTime startTime, DateTime endTime, Categories.Category category)>();

            List<AppointmentItem> appointmentItems;
            switch (this.syncOption)
            {
                case SyncOption.SyncCurrentDay:
                    appointmentItems = Calendar.GetAppointmentsForDay(DateTime.Now);
                    break;
                case SyncOption.SyncLastSevenDays:
                    appointmentItems = Calendar.GetAppointmentsForLastSevenDays(DateTime.Now);
                    break;
                default:
                    appointmentItems = new List<AppointmentItem>(0);
                    break;
            }

            appointmentItems.ForEach(appointment =>
            {
                if (Calendar.TryParseCategory(appointment, out Categories.Category category))
                {
                    newTimeEntries.Add((appointment.Subject, appointment.Start, appointment.End, category));
                }
            });

            this.Toggl.CreateTimeEntriesIfSlotIsFree(newTimeEntries);
        }

        private void OnSynchronizationTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.synchronizationTimer.Stop();
                this.SynchronizeWithToggl();
            }
            catch (System.Exception)
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
