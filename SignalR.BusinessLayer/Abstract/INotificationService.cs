using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface INotificationService : IGenericService<Notification>
    {
        int TNotificationCountByStatusisFalse();
        List<Notification> TGetAllNotificationByFalse();
        void TNotificationChangeToTrue(int id);
        void TNotificationChangeToFalse(int id);
    }
}
