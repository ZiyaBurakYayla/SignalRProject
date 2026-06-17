using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class OrderTableCountHandler : IRequestHandler<OrderTableCountQuery, int>
    {
        private IOrderTableDal _orderTableDal;

        public OrderTableCountHandler(IOrderTableDal orderTableDal)
        {
            _orderTableDal = orderTableDal;
        }

        public async Task<int> Handle(OrderTableCountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_orderTableDal.OrderTableCount());
        }
    }
}
