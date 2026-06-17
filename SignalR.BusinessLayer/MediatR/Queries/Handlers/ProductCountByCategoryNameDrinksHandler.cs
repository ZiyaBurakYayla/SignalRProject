using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class ProductCountByCategoryNameDrinksHandler : IRequestHandler<ProductCountByCategoryNameDrinksQuery, int>
    {
        private readonly IProductDal _productDal;

        public ProductCountByCategoryNameDrinksHandler(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<int> Handle(ProductCountByCategoryNameDrinksQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_productDal.ProductCountByCategoryNameDrinks());
        }
    }
}
