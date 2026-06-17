using MediatR;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class GetAllNotificationByFalseHandler : IRequestHandler<GetAllNotificationByFalseQuery, List<Notification>>
    {
        private readonly INotificationDal _notificationDal;

        public GetAllNotificationByFalseHandler(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task<List<Notification>> Handle(GetAllNotificationByFalseQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_notificationDal.GetAllNotificationByFalse());
        }
    }
}
