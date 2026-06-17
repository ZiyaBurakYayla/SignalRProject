using MediatR;
using SignalR.BusinessLayer.MediatR.Queries;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Handlers
{
    public class BookingListHandler : IRequestHandler<BookingListQuery, List<Booking>>
    {
        private readonly IBookingDal _bookingDal;

        public BookingListHandler(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public async Task<List<Booking>> Handle(BookingListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_bookingDal.GetAll());
        }
    }
}
