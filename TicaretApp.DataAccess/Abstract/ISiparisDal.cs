using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Abstract
{
    public interface ISiparisDal : IRepository<Siparis>
    {
        List<Siparis> GetSiparis(string userId);
    }
}
