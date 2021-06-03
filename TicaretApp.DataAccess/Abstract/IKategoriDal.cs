using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Abstract
{
    public interface IKategoriDal : IRepository<Kategori>
    {
        Kategori GetByIdKitabaGore(int id);
        void DeleteFromKategori(int kategoriid, int kitapid);
       
    }
}
