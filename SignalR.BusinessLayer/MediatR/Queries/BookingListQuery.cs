using MediatR;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Queries
{
    public class BookingListQuery : IRequest<List<Booking>>
    {
    }
}
