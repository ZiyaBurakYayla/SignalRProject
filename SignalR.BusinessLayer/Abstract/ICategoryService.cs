using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        public int TGetCategoryCount();
        public int TActiveCategoryCount();
        public int TPassiveCategoryCount();
    }
}
