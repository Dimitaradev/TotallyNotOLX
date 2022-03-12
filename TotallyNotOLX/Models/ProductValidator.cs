namespace TotallyNotOLX.Models
{
    public class ProductValidator : IProductValidator
    {
        public bool CheckProductAligibility(Product product)
        {
            if (product.Name != null && product.SellerId != null &&&& product.DatePosted != null)

                return true;
            else
                return false;
        }
    }

}
