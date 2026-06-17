using MediatR;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Queries
{
    public class OrderTableListQuery : IRequest<List<OrderTable>>
    {
    }
}
