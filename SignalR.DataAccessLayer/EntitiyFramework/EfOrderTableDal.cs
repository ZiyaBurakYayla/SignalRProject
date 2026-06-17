using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.EntitiyFramework
{
    public class EfOrderTableDal : GenericRepository<OrderTable>, IOrderTableDal
    {
        public EfOrderTableDal(SignalRContext context) : base(context)
        {
        }

        public void ChangeOrderTableStatusToFalse(int id)
        {
            var values = _context.OrderTables.Where(x => x.OrderTableId == id).FirstOrDefault();
            values.Status = false;
            _context.SaveChanges();
        }

        public void ChangeOrderTableStatusToTrue(int id)
        {
            var values = _context.OrderTables.Where(x => x.OrderTableId == id).FirstOrDefault();
            values.Status = true;
            _context.SaveChanges();
        }

        public int OrderTableCount()
        {
            return _context.OrderTables.Count();
        }
    }
}
