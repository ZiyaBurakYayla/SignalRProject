using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public void TAdd(OrderDetail t)
        {
            _orderDetailDal.Add(t);
        }

        public void TDelete(OrderDetail t)
        {
            _orderDetailDal.Delete(t);
        }

        public List<OrderDetail> TGetAll()
        {
            return _orderDetailDal.GetAll();
        }

        public OrderDetail TGetById(int id)
        {
            return _orderDetailDal.GetById(id);
        }

        public void TUpdate(OrderDetail t)
        {
            _orderDetailDal.Update(t);
        }
    }
}
