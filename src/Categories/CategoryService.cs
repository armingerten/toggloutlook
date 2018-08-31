using System.Linq;

using Outlook = Microsoft.Office.Interop.Outlook;

namespace TogglOutlookPlugIn.Categories
{
    public class CategoryService
    {
        private static CategoryService instance;

        private CategoryService()
        {
        }

        private Outlook.Categories OutlookCategories => Globals.ThisAddIn?.Application.Session.Categories;

        public static CategoryService Instance
            => instance ?? (instance = new CategoryService());

        public static string CategorySeperator => ";";

        public Category[] Categories => new CategoryList(Properties.Settings.Default.CategoriesString, this.OutlookCategories).Items.ToArray();

        public bool TryAddCategory(int projectId, int tagId, string categoryName)
        {
            if (this.IsInvalidCategoryName(categoryName))
            {
                return false;
            }

            Outlook.Category outlookCategory = EnsureOutlookCategoryExists(categoryName);

            CategoryList categoryList = new CategoryList(Properties.Settings.Default.CategoriesString, this.OutlookCategories);
            categoryList.AddOrUpdate(new Category(categoryName, projectId, tagId, outlookCategory));

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

        private Outlook.Category EnsureOutlookCategoryExists(string categoryName)
        {
            Outlook.Category outlookCategory = this.OutlookCategories[categoryName];
            if (outlookCategory == null)
            {
                outlookCategory = this.OutlookCategories.Add(categoryName, Outlook.OlCategoryColor.olCategoryColorDarkRed);
            }
            return outlookCategory;
        }

        private bool IsInvalidCategoryName(string categoryName)
            => categoryName.Contains(CategorySeperator);
    }
}
