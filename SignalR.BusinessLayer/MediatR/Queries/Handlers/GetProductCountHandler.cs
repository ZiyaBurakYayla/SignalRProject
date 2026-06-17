using MediatR;
using SignalR.BusinessLayer.MediatR.Queries;
using SignalR.DataAccessLayer.Abstract;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class GetProductCountHandler : IRequestHandler<GetProductCountQuery, int>
    {
        private readonly IProductDal _productDal;

        public GetProductCountHandler(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<int> Handle(GetProductCountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_productDal.ProductCount());
        }
    }
}