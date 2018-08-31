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
            set
            {
                this.syncOption = value;
                this.AdaptScheduling();

                Properties.Settings.Default.SyncOption = (int)value;
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

        private void AdaptScheduling()
        {
            switch (this.syncOption)
            {
                case SyncOption.SyncCurrentDay:
                case SyncOption.SyncCurrentWeek:
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
            Calendar.GetAppointsmentsForDay(DateTime.Now).ForEach(appointment =>
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
