using MediatR;
using SignalR.BusinessLayer.MediatR.Queries;
using SignalR.DataAccessLayer.Abstract; 

namespace SignalR.BusinessLayer.MediatR.Handlers
{
    public class GetCategoryCountHandler : IRequestHandler<GetCategoryCountQuery, int>
    {
        private readonly ICategoryDal _categoryDal;

        public GetCategoryCountHandler(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<int> Handle(GetCategoryCountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_categoryDal.GetCategoryCount());
        }
    }
}