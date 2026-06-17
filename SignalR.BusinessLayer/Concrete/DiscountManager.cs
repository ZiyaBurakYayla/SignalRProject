using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Concrete
{
    public class DiscountManager : IDiscountService
    {
        private readonly IDiscountDal _discountDal;

        public DiscountManager(IDiscountDal discountDal)
        {
            _discountDal = discountDal;
        }

        public void TAdd(Discount t)
        {
            _discountDal.Add(t);
        }

        public void TDelete(Discount t)
        {
            _discountDal.Delete(t);
        }

        public void TDiscountChangeStatusToFalse(int id)
        {
            _discountDal.DiscountChangeStatusToFalse(id);
        }

        public void TDiscountChangeStatusToTrue(int id)
        {
            _discountDal.DiscountChangeStatusToTrue(id);
        }

        public List<Discount> TGetAll()
        {
            return _discountDal.GetAll();
        }

        public Discount TGetById(int id)
        {
            return _discountDal.GetById(id);
        }

        public List<Discount> TGetListDiscountByStatusTrue()
        {
            return _discountDal.GetListDiscountByStatusTrue();
        }

        public void TUpdate(Discount t)
        {
            _discountDal.Update(t);
        }
    }
}
