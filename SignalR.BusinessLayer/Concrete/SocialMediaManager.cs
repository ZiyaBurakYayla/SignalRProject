using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Concrete
{
    public class SocialMediaManager : ISocialMediaService
    {
        private readonly ISocialMediaDal _socialmediaDal;

        public SocialMediaManager(ISocialMediaDal socialmediaDal)
        {
            _socialmediaDal = socialmediaDal;
        }

        public void TAdd(SocialMedia t)
        {
            _socialmediaDal.Add(t);
        }

        public void TDelete(SocialMedia t)
        {
            _socialmediaDal.Delete(t);
        }

        public List<SocialMedia> TGetAll()
        {
            return _socialmediaDal.GetAll();
        }

        public SocialMedia TGetById(int id)
        {
            return _socialmediaDal.GetById(id);
        }

        public void TUpdate(SocialMedia t)
        {
            _socialmediaDal.Update(t);
        }
    }
}
