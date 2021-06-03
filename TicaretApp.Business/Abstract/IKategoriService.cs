using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.Business.Abstract
{
    public interface IKategoriService
    {
        Kategori GetById(int id);
        Kategori GetByIdKitabaGore(int id);
        List<Kategori> GetAll();

        void Create(Kategori entity);
        void Delete(Kategori entity);
        void Update(Kategori entity);
        void DeleteFromKategori(int kategoriid, int kitapid);
        
        
    }
}
