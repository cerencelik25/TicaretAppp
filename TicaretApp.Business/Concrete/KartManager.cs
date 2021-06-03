using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Business.Abstract;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.Entities;

namespace TicaretApp.Business.Concrete
{
    public class KartManager : IKartService
    {
        private IKartDal _kartDal;
        public KartManager(IKartDal kartDal)
        {
            _kartDal = kartDal;
        }
        public void SepeteEkle(string userId, int kitapid, int adet)
        {
            var kart = GetKartUserIdyeGore(userId);
            if (kart !=null)
            {
                var index = kart.KartItems.FindIndex(i => i.KitapId == kitapid);

                if (index<0)
                {
                    kart.KartItems.Add(new KartItem(){
                        KitapId=kitapid,
                        Adet=adet,
                        KartId=kart.KartId
                    });
                }
                else
                {
                    kart.KartItems[index].Adet += adet;
                }
                _kartDal.Update(kart);
            }
        } 

        public Kart GetKartUserIdyeGore(string userId)
        {
            return _kartDal.GetByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _kartDal.Create(new Kart() { UserId=userId});
        }

        public void SepettenSil(string userId, int kitapId)
        {
            var kart = GetKartUserIdyeGore(userId);
            if (kart != null)
            {

                _kartDal.SepettenSil(kart.KartId,kitapId);
            }
        }

        public void ClearKart(string kartId)
        {
            _kartDal.ClearKart(kartId);
        }
    }
}
