using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class TotalOrderCountHandler : IRequestHandler<TotalOrderCountQuery, int>
    {
        private readonly IOrderDal _orderDal;

        public TotalOrderCountHandler(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task<int> Handle(TotalOrderCountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_orderDal.TotalOrderCount());
        }
    }
}
