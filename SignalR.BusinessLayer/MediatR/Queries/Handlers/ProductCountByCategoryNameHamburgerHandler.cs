using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class ProductCountByCategoryNameHamburgerHandler : IRequestHandler<ProductCountByCategoryNameHamburgerQuery, int>
    {
        private readonly IProductDal _productDal;

        public ProductCountByCategoryNameHamburgerHandler(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<int> Handle(ProductCountByCategoryNameHamburgerQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_productDal.ProductCountByCategoryNameHamburger());
        }
    }
}
