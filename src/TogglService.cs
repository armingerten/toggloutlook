using System;
using System.Collections.Generic;
using System.Linq;
using Toggl.Api.DataObjects;
using Toggl.Api.Services;
using TogglOutlookPlugIn.Categories;

namespace TogglOutlookPlugIn
{
    public class TogglService
    {
        private static TogglService instance;

        private static readonly string togglResponseDateFormat = "MM/dd/yyyy HH:mm:ss";

        private UserServiceAsync userService;
        private WorkspaceServiceAsync workspaceService;
        private TimeEntryServiceAsync timeEntryService;
        private string apiKey;

        private TogglService()
        {
            this.TryEstablishLink(this.ApiKey);
        }

        public event EventHandler<EventArgs> LinkStateChangedEvent;

        public static TogglService Instance
            => instance ?? (instance = new TogglService());

        public string ApiKey
        {
            get => Properties.Settings.Default.ApiKey ?? string.Empty;
            private set
            {
                this.apiKey = value;
                Properties.Settings.Default.ApiKey = apiKey;
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

        public void CreateTimeEntriesIfSlotIsFree(List<(string description, DateTime startTime, DateTime endTime, Category category)> newTimeEntries)
        {
            if (!this.IsLinkEstablished)
            {
                return;
            }

            List<TimeEntry> existingTimeEntries = this.timeEntryService.List().Result;
            newTimeEntries.ForEach(entry =>
            {
                this.TryCreateTimeEntry(existingTimeEntries, entry.description, entry.startTime, entry.endTime, entry.category.ProjectId, entry.category.TagId);
            });
        }

        public bool TryCreateTimeEntry(string description, DateTime startTime, DateTime endTime, Category category)
            => this.TryCreateTimeEntry(this.timeEntryService.List().Result, description, startTime, endTime, category.ProjectId, category.TagId);

        public bool TryCreateTimeEntry(string description, DateTime startTime, DateTime endTime, int projectId, int tagId)
            => this.TryCreateTimeEntry(this.timeEntryService.List().Result, description, startTime, endTime, projectId, tagId);

        private bool TryCreateTimeEntry(List<TimeEntry> existingTimeEntries, string description, DateTime startTime, DateTime endTime, int projectId, int tagId)
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

            if (this.IsAlreadyTimeEntryInSlot(existingTimeEntries, startTime, endTime, projectId))
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


        private bool IsAlreadyTimeEntryInSlot(List<TimeEntry> existingTimeEntries, DateTime startTime, DateTime endTime, int projectId)
            => existingTimeEntries.FirstOrDefault(entry
                => DateTime.ParseExact(entry.Start, togglResponseDateFormat, null) == startTime
                && DateTime.ParseExact(entry.Stop, togglResponseDateFormat, null) == endTime
                && entry.WorkspaceId == this.CurrentWorkspace.Id
                && entry.ProjectId == projectId) != null;
    }
}
