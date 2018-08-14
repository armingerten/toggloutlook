using System.Linq;

using Outlook = Microsoft.Office.Interop.Outlook;

namespace TogglOutlookPlugIn.Categories
{
    public class CategoryManager
    {
        private static CategoryManager instance;

        private CategoryManager()
        {
        }

        private Outlook.Categories OutlookCategories => Globals.ThisAddIn.Application.Session.Categories;

        public static CategoryManager Instance
            => instance ?? (instance = new CategoryManager());

        public static string CategorySeperator => ";";

        public Category[] Categories => new CategoryList(Properties.Settings.Default.CategoriesString, this.OutlookCategories).Items.ToArray();

        public bool TryAddCategory(int projectId, int tagId, string categoryName)
        {
            Outlook.Category outlookCategory = this.OutlookCategories[categoryName];

            if (outlookCategory != null)
            {
                return false;
            }
            if (categoryName.Contains(CategorySeperator))
            {
                return false;
            }

            this.OutlookCategories.Add(categoryName, Outlook.OlCategoryColor.olCategoryColorDarkRed);
            CategoryList categoryList = new CategoryList(Properties.Settings.Default.CategoriesString, this.OutlookCategories);

            categoryList.Add(new Category(categoryName, projectId, tagId, outlookCategory));

            Properties.Settings.Default.CategoriesString = categoryList.ItemsAsXmlString;
            Properties.Settings.Default.Save();

            return true;
        }

        public void RemoveCategory(Category selectedCategory)
        {
            CategoryList categoryList = new CategoryList(Properties.Settings.Default.CategoriesString, this.OutlookCategories);
            categoryList.Remove(selectedCategory);

            if (!selectedCategory.IsOutlookCategoryMissing)
            {
                this.OutlookCategories.Remove(selectedCategory.Name);
            }

            Properties.Settings.Default.CategoriesString = categoryList.ItemsAsXmlString;
            Properties.Settings.Default.Save();
        }
    }
}
