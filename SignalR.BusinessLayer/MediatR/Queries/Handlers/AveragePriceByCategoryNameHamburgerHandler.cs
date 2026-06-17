using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class AveragePriceByCategoryNameHamburgerHandler : IRequestHandler<AveragePriceByCategoryNameHamburgerQuery, decimal>
    {
        private readonly IProductDal _productDal;

        public AveragePriceByCategoryNameHamburgerHandler(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<decimal> Handle(AveragePriceByCategoryNameHamburgerQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_productDal.AveragePriceByCategoryNameHamburger());
        }
    }
}
