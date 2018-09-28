using System;
using System.Collections.Generic;
using System.Linq;
using Toggl.Api.DataObjects;
using Toggl.Api.Services;
using TogglOutlookPlugIn.Models;

namespace TogglOutlookPlugIn.Services
{
    public class TogglService
    {
        private static TogglService instance;

        private UserServiceAsync userService;
        private WorkspaceServiceAsync workspaceService;
        private TimeEntryServiceAsync timeEntryService;
        private string apiKey;

        private TogglService() => this.TryEstablishLink(this.ApiKey);

        public event EventHandler<EventArgs> LinkStateChangedEvent;

        public static TogglService Instance
            => instance ?? (instance = new TogglService());

        public string ApiKey
        {
            get => Properties.Settings.Default.ApiKey ?? string.Empty;
            private set
            {
                this.apiKey = value;
                Properties.Settings.Default.ApiKey = this.apiKey;
                Properties.Settings.Default.Save();
            }
        }

        public bool IsLinkEstablished { get; private set; }

        public List<Project> Projects { get; private set; } = new List<Project>();

        public List<Tag> Tags { get; private set; } = new List<Tag>();

        public List<Workspace> Workspaces { get; private set; } = new List<Workspace>();

        public Workspace CurrentWorkspace { get; private set; }

        public User CurrentUser { get; private set; }

        public bool TryEstablishLink(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return false;
            }

            try
            {
                this.ApiKey = apiKey;

                this.workspaceService = new WorkspaceServiceAsync(apiKey);
                this.timeEntryService = new TimeEntryServiceAsync(apiKey);
                this.userService = new UserServiceAsync(apiKey);

                this.Workspaces = this.workspaceService.List().Result;
                this.CurrentWorkspace = this.Workspaces.First();
                this.CurrentUser = this.userService.GetCurrent().Result;
                this.Projects = this.workspaceService.Projects(this.CurrentWorkspace.Id).Result;
                this.Tags = this.workspaceService.Tags(this.CurrentWorkspace.Id).Result;

                this.IsLinkEstablished = true;
                this.LinkStateChangedEvent?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                this.ApiKey = string.Empty;

                this.CurrentWorkspace = null;
                this.CurrentUser = null;
                this.Workspaces.Clear();
                this.Projects.Clear();
                this.Tags.Clear();

                this.IsLinkEstablished = false;
                this.LinkStateChangedEvent?.Invoke(this, EventArgs.Empty);
            }

            return this.IsLinkEstablished;
        }

        public bool TrySwitchWorkspace(int workspaceId)
        {
            if (!this.IsLinkEstablished)
            {
                return false;
            }
            if (workspaceId == this.CurrentWorkspace.Id)
            {
                return true;
            }

            Workspace targetWorkspace = this.Workspaces.FirstOrDefault(w => workspaceId == w.Id);
            if (targetWorkspace == null)
            {
                return false;
            }

            try
            {
                this.CurrentWorkspace = targetWorkspace;
                this.Projects = this.workspaceService.Projects(this.CurrentWorkspace.Id).Result;
                this.Tags = this.workspaceService.Tags(this.CurrentWorkspace.Id).Result;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CreateOrUpdateAppointments(List<CategorizedAppointment> appointments)
        {
            if (!this.IsLinkEstablished)
            {
                return;
            }

            List<TimeEntry> existingTimeEntries = this.timeEntryService.List().Result;

            // Determine and delete conflicting entries
            long[] conflictingTimeEntries = this.GetConflictingTimeEntryIds(existingTimeEntries, appointments);
            if (conflictingTimeEntries.Length > 0)
            {
                this.timeEntryService.Delete(conflictingTimeEntries).Wait();
            }

            // Create specified entries
            this.CreateAppointmentsIfSlotIsFree(appointments);
        }

        public void CreateAppointmentsIfSlotIsFree(List<CategorizedAppointment> appointments)
        {
            if (!this.IsLinkEstablished)
            {
                return;
            }

            List<TimeEntry> existingTimeEntries = this.timeEntryService.List().Result;
            appointments.ForEach(appointment =>
            {
                this.TryCreateAppointment(existingTimeEntries, appointment);
            });
        }

        public bool TryCreateAppointment(CategorizedAppointment categorizedAppointment)
            => this.TryCreateAppointment(this.timeEntryService.List().Result, categorizedAppointment);

        public bool TryCreateAppointment(string description, DateTime startTime, DateTime endTime, int projectId, int tagId)
            => this.TryCreateAppointment(this.timeEntryService.List().Result, description, startTime, endTime, projectId, tagId);

        private bool TryCreateAppointment(List<TimeEntry> existingTimeEntries, CategorizedAppointment ca)
            => this.TryCreateAppointment(existingTimeEntries, ca.Description, ca.StartTime, ca.EndTime, ca.Category.ProjectId, ca.Category.TagId);

        private bool TryCreateAppointment(List<TimeEntry> existingTimeEntries, string description, DateTime startTime, DateTime endTime, int projectId, int tagId)
        {
            if (!this.IsLinkEstablished)
            {
                return false;
            }

            Project project = this.Projects.FirstOrDefault(p => p.Id == projectId);
            Tag tag = this.Tags.FirstOrDefault(t => t.Id == tagId);

            if (project == null || tag == null)
            {
                return false;
            }

            if (existingTimeEntries.Any(timeEntry => timeEntry.IsOverlappingWith(startTime, endTime)))
            {
                return false;
            }

            try
            {
                TimeEntry timeEntry = new TimeEntry
                {
                    Description = description,
                    CreatedWith = "outlook",
                    ProjectId = project.Id,
                    TagNames = new List<string>(new[] { tag.Name }),
                    Start = startTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    Duration = (long)(endTime - startTime).TotalSeconds,
                    WorkspaceId = this.CurrentWorkspace.Id,
                };

                this.timeEntryService.Add(timeEntry).Wait();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private long[] GetConflictingTimeEntryIds(List<TimeEntry> existingTimeEntries, List<CategorizedAppointment> appointments)
        {
            List<long> conflictingTimeEntryIds = new List<long>();
            appointments.ForEach(appointment =>
            {
                conflictingTimeEntryIds.AddRange(
                    existingTimeEntries
                        .Where(timeEntry => timeEntry.IsConflictingWith(appointment))
                        .Select(timeEntry => (long)timeEntry.Id));
            });

            return conflictingTimeEntryIds.ToArray();
        }
    }
}
