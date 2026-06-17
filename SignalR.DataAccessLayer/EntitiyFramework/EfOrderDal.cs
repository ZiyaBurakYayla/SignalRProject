using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.EntitiyFramework
{
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        public EfOrderDal(SignalRContext context) : base(context)
        {
        }

        public int ActiveOrderCount()
        {
            return _context.Orders.Count(o => o.Description == "Müşteri Masada");
        }

        public decimal LastOrderPrice()
        {
            return _context.Orders.OrderByDescending(o => o.OrderId).Take(1).Select(o => o.TotalPrice).FirstOrDefault();
        }

        public decimal TodayTotalPrice()
        {
            return _context.Orders.Where(o => o.OrderDate.Date == DateTime.Now.Date && o.Description == "Hesap Kapatıldı").Sum(o => o.TotalPrice);
        }

        public int TotalOrderCount()
        {
            return _context.Orders.Count();
        }
    }
}
