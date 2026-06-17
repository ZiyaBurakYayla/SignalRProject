using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IDiscountService : IGenericService<Discount>
    {
        void TDiscountChangeStatusToTrue(int id);
        void TDiscountChangeStatusToFalse(int id);
        List<Discount> TGetListDiscountByStatusTrue();
    }
}
