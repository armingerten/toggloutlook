using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TogglOutlookPlugIn.Categories
{
    public class CategoryList
    {
        private List<Category> items;

        public CategoryList(string xmlString, Outlook.Categories outlookCategories)
        {
            if (!string.IsNullOrWhiteSpace(xmlString))
            {
                try
                {
                    this.items = new List<Category>(DeserializeItemsFromXmlString(xmlString));
                }
                catch (Exception)
                {
                    // Ignore de-serialization issues for now.
                }
            }

            DecorateAndConcatOutlookCategories(this.items ?? (this.items = new List<Category>()), outlookCategories);
        }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public Category[] Items
        {
            get => this.items.ToArray();
            set => this.items = new List<Category>(value);
        }

        public string ItemsAsXmlString
        {
            get
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Category[]));

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, this.Items.Where(item => !item.IsOutlookOnly).ToArray());
                    return textWriter.ToString();
                }
            }
        }

        public void AddOrUpdate(Category item)
        {
            Category existingItem = this.items.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                this.items.Remove(existingItem);
            }

            this.items.Add(item);
        }

        public void Remove(Category item)
        {
            Category listItem = this.items.FirstOrDefault(i => i.Name == item.Name);

            if (listItem != null)
            {
                this.items.Remove(listItem);
            }
        }

        private static void DecorateAndConcatOutlookCategories(List<Category> categories, Outlook.Categories outlookCategories)
        {
            if (outlookCategories == null)
            {
                return;
            }

            foreach (Outlook.Category outlookCategory in outlookCategories)
            {
                Category category = categories.FirstOrDefault(c => c.Name == outlookCategory.Name);
                if (category != null)
                {
                    category.OutlookCategory = outlookCategory;
                }
                else
                {
                    categories.Add(new Category(outlookCategory.Name, outlookCategory));
                }
            }
        }

        private static Category[] DeserializeItemsFromXmlString(string itemsAsXmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Category[]));
            using (StringReader textReader = new StringReader(itemsAsXmlString))
            {
                return (Category[])xmlSerializer.Deserialize(textReader);
            }
        }
    }
}
