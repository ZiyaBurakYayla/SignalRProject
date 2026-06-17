using SignalR.EntityLayer.Entities;


namespace SignalR.DataAccessLayer.Abstract
{
    public interface IDiscountDal: IGenericDal<Discount>
    {
        void DiscountChangeStatusToTrue(int id);
        void DiscountChangeStatusToFalse(int id);
        List<Discount> GetListDiscountByStatusTrue();
    }
}
