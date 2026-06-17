using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface ICategoryDal: IGenericDal<Category>
    {
        public int GetCategoryCount();

        int ActiveCategoryCount();
        int PassiveCategoryCount();
    }
}
