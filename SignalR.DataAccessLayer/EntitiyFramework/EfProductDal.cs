using Microsoft.EntityFrameworkCore;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.EntitiyFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public EfProductDal(SignalRContext context) : base(context)
        {
        }

        public decimal AveragePriceByCategoryNameHamburger()
        {
            return _context.Products.Where(z => z.CategoryId == (_context.Categories.Where(y => y.CategoryName =="Hamburger")
            .Select(t => t.CategoryId).FirstOrDefault())).Average(p => p.ProductPrice);
        }

        public decimal AverageProductPrice()
        {
            return _context.Products.Average(p => p.ProductPrice);
        }

        public List<Product> GetLast9ProductsListWithCategory()
        {
            return _context.Products.Take(9).Include(p => p.Category).ToList();
        }

        public List<Product> GetProductsByCategories()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public string HighestPriceProductName()
        {
            return _context.Products.Where(z => z.ProductPrice == (_context.Products.Max(y => y.ProductPrice)))
                .Select(a => a.ProductName).FirstOrDefault();
        }

        public string LowestPriceProductName()
        {
            return _context.Products.Where(z => z.ProductPrice == (_context.Products.Min(y => y.ProductPrice)))
     .Select(a => a.ProductName).FirstOrDefault();
        }

        public int ProductCount()
        {
            return _context.Products.Count();
        }

        public int ProductCountByCategoryNameDrinks()
        {
            return _context.Products.Where(z => z.CategoryId == (_context.Categories.Where(y => y.CategoryName == "İçecekler")
            .Select(t => t.CategoryId)
            .FirstOrDefault())).Count();
        }

        public int ProductCountByCategoryNameHamburger()
        {
            return _context.Products.Where(z => z.CategoryId == (_context.Categories.Where(y => y.CategoryName == "Hamburger")
            .Select(t => t.CategoryId)
            .FirstOrDefault())).Count();
        }
    }
}
