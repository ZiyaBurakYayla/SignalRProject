using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class ActiveOrderCountHandler : IRequestHandler<ActiveOrderCountQuery, int>
    {
        private readonly IOrderDal _orderDal;

        public ActiveOrderCountHandler(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task<int> Handle(ActiveOrderCountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_orderDal.ActiveOrderCount());
        }
    }
}
