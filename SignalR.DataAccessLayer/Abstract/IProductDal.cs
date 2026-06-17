using SignalR.EntityLayer.Entities;


namespace SignalR.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> GetProductsByCategories();
        int ProductCount();
        int ProductCountByCategoryNameHamburger();
        int ProductCountByCategoryNameDrinks();
        decimal AverageProductPrice();
        string LowestPriceProductName();
        string HighestPriceProductName();
        decimal AveragePriceByCategoryNameHamburger();
        List<Product> GetLast9ProductsListWithCategory();
    }
}
