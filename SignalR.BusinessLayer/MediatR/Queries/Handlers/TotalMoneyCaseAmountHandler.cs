using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class TotalMoneyCaseAmountHandler : IRequestHandler<TotalMoneyCaseAmountQuery, decimal>
    {
        private readonly IMoneyCaseDal _moneyCaseDal;

        public TotalMoneyCaseAmountHandler(IMoneyCaseDal moneyCaseDal)
        {
            _moneyCaseDal = moneyCaseDal;
        }

        public async Task<decimal> Handle(TotalMoneyCaseAmountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_moneyCaseDal.TotalMoneyCaseAmount());
        }
    }
}
