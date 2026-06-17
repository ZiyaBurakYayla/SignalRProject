using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
namespace SignalR.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _ProductDal;

        public ProductManager(IProductDal ProductDal)
        {
            _ProductDal = ProductDal;
        }

        public void TAdd(Product t)
        {
            _ProductDal.Add(t);
        }

        public decimal TAveragePriceByCategoryNameHamburger()
        {
            return _ProductDal.AveragePriceByCategoryNameHamburger();
        }

        public decimal TAverageProductPrice()
        {
            return _ProductDal.AverageProductPrice();
        }

        public void TDelete(Product t)
        {
            _ProductDal.Delete(t);
        }

        public List<Product> TGetAll()
        {
            return _ProductDal.GetAll();
        }

        public Product TGetById(int id)
        {
            return _ProductDal.GetById(id);
        }

        public List<Product> TGetLast9ProductsListWithCategory()
        {
            return _ProductDal.GetLast9ProductsListWithCategory();
        }

        public List<Product> TGetProductsByCategories()
        {
            return _ProductDal.GetProductsByCategories();
        }

        public string THighestPriceProductName()
        {
            return _ProductDal.HighestPriceProductName();
        }

        public string TLowestPriceProductName()
        {
            return _ProductDal.LowestPriceProductName();
        }

        public int TProductCount()
        {
            return _ProductDal.ProductCount();
        }

        public int TProductCountByCategoryNameDrinks()
        {
            return _ProductDal.ProductCountByCategoryNameDrinks();
        }

        public int TProductCountByCategoryNameHamburger()
        {
            return _ProductDal.ProductCountByCategoryNameHamburger();
        }

        public void TUpdate(Product t)
        {
            _ProductDal.Update(t);
        }
    }
}
