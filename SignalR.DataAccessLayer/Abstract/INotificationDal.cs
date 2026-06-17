using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface INotificationDal : IGenericDal<Notification>
    {
        int NotificationCountByStatusisFalse();
        List<Notification> GetAllNotificationByFalse();
        void NotificationChangeToTrue(int id);
        void NotificationChangeToFalse(int id);
    }
}
