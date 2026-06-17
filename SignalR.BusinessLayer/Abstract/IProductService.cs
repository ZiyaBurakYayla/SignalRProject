using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        List<Product> TGetProductsByCategories();
        int TProductCount();
        int TProductCountByCategoryNameDrinks();
        int TProductCountByCategoryNameHamburger();
        decimal TAverageProductPrice();
        string TLowestPriceProductName();
        string THighestPriceProductName();
        decimal TAveragePriceByCategoryNameHamburger();
        List<Product> TGetLast9ProductsListWithCategory();
    }
}
