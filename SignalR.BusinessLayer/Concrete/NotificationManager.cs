using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public void TAdd(Notification t)
        {
            _notificationDal.Add(t);
        }

        public void TDelete(Notification t)
        {
            _notificationDal.Delete(t);
        }

        public List<Notification> TGetAll()
        {
            return _notificationDal.GetAll();
        }

        public List<Notification> TGetAllNotificationByFalse()
        {
           return _notificationDal.GetAllNotificationByFalse();
        }

        public Notification TGetById(int id)
        {
            return _notificationDal.GetById(id);
        }

        public void TNotificationChangeToFalse(int id)
        {
            _notificationDal.NotificationChangeToFalse(id);
        }

        public void TNotificationChangeToTrue(int id)
        {
            _notificationDal.NotificationChangeToTrue(id);
        }

        public int TNotificationCountByStatusisFalse()
        {
            return _notificationDal.NotificationCountByStatusisFalse();
        }

        public void TUpdate(Notification t)
        {
            _notificationDal.Update(t);
        }
    }
}
