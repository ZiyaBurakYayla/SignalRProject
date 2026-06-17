using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class HighestPriceProductNameHandler : IRequestHandler<HighestPriceProductNameQuery, string>
    {
        private readonly IProductDal _productDal;

        public HighestPriceProductNameHandler(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<string> Handle(HighestPriceProductNameQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_productDal.HighestPriceProductName());
        }
    }
}
