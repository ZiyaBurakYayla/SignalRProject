using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class NotificationCountByStatusisFalseHandler : IRequestHandler<NotificationCountByStatusisFalseQuery, int>
    {
        private readonly INotificationDal _notificationDal;

        public NotificationCountByStatusisFalseHandler(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task<int> Handle(NotificationCountByStatusisFalseQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_notificationDal.NotificationCountByStatusisFalse());
        }
    }
}
