using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        public void TAdd(Basket t)
        {
            _basketDal.Add(t);
        }

        public void TDelete(Basket t)
        {
            _basketDal.Delete(t);
        }

        public List<Basket> TGetAll()
        {
            return _basketDal.GetAll();
        }

        public List<Basket> TGetBasketByOrderTableNumber(int tableNumber)
        {
            return _basketDal.GetBasketByOrderTableNumber(tableNumber);
        }

        public Basket TGetById(int id)
        {
            return _basketDal.GetById(id);
        }

        public void TUpdate(Basket t)
        {
            _basketDal.Update(t);
        }
    }
}
