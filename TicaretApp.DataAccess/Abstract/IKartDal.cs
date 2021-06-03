using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Abstract
{
    public interface IKartDal : IRepository<Kart>
    {
        Kart GetByUserId(string userId);
        void SepettenSil(int kartId, int kitapId);
        void ClearKart(string kartId);
    }
}
