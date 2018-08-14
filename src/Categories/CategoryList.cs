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
                    List<Category> categories = new List<Category>(DeserializeItemsFromXmlString(xmlString));
                    DecorateAndConcatOutlookCategories(categories, outlookCategories);

                    this.items = categories;
                    return;
                }
                catch (Exception)
                {
                }
            }
            this.items = new List<Category>();
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

        public void Add(Category item)
        {
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
