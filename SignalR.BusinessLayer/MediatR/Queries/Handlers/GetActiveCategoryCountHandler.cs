using MediatR;
using SignalR.DataAccessLayer.Abstract;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class GetActiveCategoryCountHandler : IRequestHandler<GetActiveCategoryCountQuery, int>
    {
        private readonly ICategoryDal _categoryDal;

        public GetActiveCategoryCountHandler(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<int> Handle(GetActiveCategoryCountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_categoryDal.ActiveCategoryCount());
        }
    }
}