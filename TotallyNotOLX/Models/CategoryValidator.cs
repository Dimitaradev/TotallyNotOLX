namespace TotallyNotOLX.Models
{
    public class CategoryValidator : ICategoryValidator
    {
        public bool CheckCategoryAligibility(Category category)
        {
            if (category.Name != null && category.Description != null)

                return true;
            else
                return false;
        }
    }
}
