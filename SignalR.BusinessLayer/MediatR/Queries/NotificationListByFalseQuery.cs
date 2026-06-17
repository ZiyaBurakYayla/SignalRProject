using MediatR;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Queries
{
    public class NotificationListByFalseQuery : IRequest<List<Notification>>
    {
    }
}
