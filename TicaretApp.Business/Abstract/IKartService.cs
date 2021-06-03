using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.Business.Abstract
{
    public interface IKartService
    {
        void InitializeCart(string userId);
        Kart GetKartUserIdyeGore(string userId);

        void SepeteEkle(string userId, int kitapid, int adet);
        void SepettenSil(string userId, int kitapId);
        void ClearKart(string kartId);
    }
}

