using MediatR;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Queries
{
    public class GetAllNotificationByFalseQuery : IRequest<List<Notification>>
    {
    }
}
