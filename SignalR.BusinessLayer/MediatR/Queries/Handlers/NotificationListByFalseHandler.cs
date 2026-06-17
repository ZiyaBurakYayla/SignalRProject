using MediatR;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class NotificationListByFalseHandler : IRequestHandler<NotificationListByFalseQuery, List<Notification>>
    {
        private readonly INotificationDal _notificationDal;

        public NotificationListByFalseHandler(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task<List<Notification>> Handle(NotificationListByFalseQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_notificationDal.GetAllNotificationByFalse());
        }
    }
}
