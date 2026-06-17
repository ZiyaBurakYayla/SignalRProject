using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Concrete
{
    public class OrderTableManager : IOrderTableService
    {
        private readonly IOrderTableDal _orderTableDal;

        public OrderTableManager(IOrderTableDal orderTableDal)
        {
            _orderTableDal = orderTableDal;
        }

        public void TAdd(OrderTable t)
        {
            _orderTableDal.Add(t);
        }

        public void TChangeOrderTableStatusToFalse(int id)
        {
            _orderTableDal.ChangeOrderTableStatusToFalse(id);
        }

        public void TChangeOrderTableStatusToTrue(int id)
        {
            _orderTableDal.ChangeOrderTableStatusToTrue(id);
        }

        public void TDelete(OrderTable t)
        {
            _orderTableDal.Delete(t);
        }

        public List<OrderTable> TGetAll()
        {
            return _orderTableDal.GetAll();
        }

        public OrderTable TGetById(int id)
        {
            return _orderTableDal.GetById(id);
        }

        public int TOrderTableCount()
        {
            return _orderTableDal.OrderTableCount();
        }

        public void TUpdate(OrderTable t)
        {
            _orderTableDal.Update(t);
        }
    }
}
