using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.EntitiyFramework
{
    public class EfDiscountDal : GenericRepository<Discount>, IDiscountDal
    {
        public EfDiscountDal(SignalRContext context) : base(context)
        {
        }

        public void DiscountChangeStatusToFalse(int id)
        {
            var value = _context.Discounts.Find(id);
            if (value != null)
            {
                value.Status = false;
                _context.SaveChanges();
            }
        }

        public void DiscountChangeStatusToTrue(int id)
        {
            var value = _context.Discounts.Find(id);
            if (value != null)
            {
                value.Status = true;
                _context.SaveChanges();
            }
        }

        public List<Discount> GetListDiscountByStatusTrue()
        {
            return _context.Discounts.Where(z => z.Status == true).ToList();
        }
    }
}
