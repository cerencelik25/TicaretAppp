using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Business.Abstract;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.Entities;

namespace TicaretApp.Business.Concrete
{
    public class SiparisManager : ISiparisService
    {
        private ISiparisDal _siparisDal;
        public SiparisManager(ISiparisDal siparisDal)
        {
            _siparisDal = siparisDal;
        }
        public void Create(Siparis entity)
        {
            _siparisDal.Create(entity);
        }

        public List<Siparis> GetSiparis(string userId)
        {
            return _siparisDal.GetSiparis(userId);
        }
    }
}
