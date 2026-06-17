using MediatR;
using SignalR.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.MediatR.Queries.Handlers
{
    public class GetPassiveCategoryCountHandler : IRequestHandler<GetPassiveCategoryCountQuery, int>
    {
        private readonly ICategoryDal _categoryDal;

        public GetPassiveCategoryCountHandler(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<int> Handle(GetPassiveCategoryCountQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_categoryDal.PassiveCategoryCount());
        }
    }
}
