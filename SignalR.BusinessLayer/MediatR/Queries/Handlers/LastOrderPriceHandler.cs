using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class LastOrderPriceHandler : IRequestHandler<LastOrderPriceQuery, decimal>
    {
        private readonly IOrderDal _orderDal;

        public LastOrderPriceHandler(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task<decimal> Handle(LastOrderPriceQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_orderDal.LastOrderPrice());
        }
    }
}
