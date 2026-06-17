using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IOrderTableDal : IGenericDal<OrderTable>
    {
        int OrderTableCount();
        void ChangeOrderTableStatusToTrue(int id);
        void ChangeOrderTableStatusToFalse(int id);
    }
}
