using MediatR;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class OrderTableListHandler : IRequestHandler<OrderTableListQuery, List<OrderTable>>
    {
        private readonly IOrderTableDal _orderTableDal;

        public OrderTableListHandler(IOrderTableDal orderTableDal)
        {
            _orderTableDal = orderTableDal;
        }

        public async Task<List<OrderTable>> Handle(OrderTableListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_orderTableDal.GetAll());
        }
    }
}
