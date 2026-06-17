using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.EntitiyFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        public EfBookingDal(SignalRContext context) : base(context)
        {
        }

        public void BookingStatusApproved(int id)
        {
            var value = _context.Bookings.Find(id);
            if (value != null)
            {
                value.Description = "Rezervasyon Onaylandı";
                _context.SaveChanges();
            }
            return;
        }

        public void BookingStatusCancelled(int id)
        {
            var value = _context.Bookings.Find(id);
            if (value != null)
            {
                value.Description = "Rezervasyon İptal Edildi";
                _context.SaveChanges();
            }
            return;
        }
    }
}
