using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.Business.Abstract
{
    public interface ISiparisService
    {
        void Create(Siparis entity);
        List<Siparis> GetSiparis(string userId);
    }
}
