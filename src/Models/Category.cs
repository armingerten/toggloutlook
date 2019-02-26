using System.Xml.Serialization;

namespace TogglOutlookPlugIn.Models
{
    public class Category
    {
        public Category()
        {
            // Parametersless constructor is required for XML serialization
        }

        public Category(string name, Microsoft.Office.Interop.Outlook.Category outlookCategory)
        {
            this.Name = name;
            this.OutlookCategory = outlookCategory;
        }

        public Category(string name, int projectId, Microsoft.Office.Interop.Outlook.Category outlookCategory)
        {
            this.Name = name;
            this.ProjectId = projectId;
            this.OutlookCategory = outlookCategory;
        }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int ProjectId { get; set; }

        [XmlIgnore]
        public bool IsOutlookOnly => this.ProjectId <= 0;

        [XmlIgnore]
        public bool IsOutlookCategoryMissing => this.OutlookCategory == null;

        [XmlIgnore]
        public Microsoft.Office.Interop.Outlook.Category OutlookCategory { get; set; }
    }
}
