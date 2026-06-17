using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Abstract
{
    public interface IOrderTableService : IGenericService<OrderTable>
    {
        int TOrderTableCount();
        void TChangeOrderTableStatusToTrue(int id);
        void TChangeOrderTableStatusToFalse(int id);
    }
}
