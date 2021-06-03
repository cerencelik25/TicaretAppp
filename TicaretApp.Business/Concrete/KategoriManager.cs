using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Business.Abstract;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.Entities;

namespace TicaretApp.Business.Concrete
{
    public class KategoriManager : IKategoriService
    {
        private IKategoriDal _kategoriDal;
        public KategoriManager(IKategoriDal kategoriDal)
        {
            _kategoriDal = kategoriDal;
        }
        public void Create(Kategori entity)
        {
            _kategoriDal.Create(entity);
        }

        public void Delete(Kategori entity)
        {
            _kategoriDal.Delete(entity);
        }

        public void DeleteFromKategori(int kategoriid, int kitapid)
        {
            _kategoriDal.DeleteFromKategori(kategoriid,kitapid);
        }

      

        public List<Kategori> GetAll()
        {
            return _kategoriDal.GetAll();
        }

        public Kategori GetById(int id)
        {
            return _kategoriDal.GetById(id);
        }

        public Kategori GetByIdKitabaGore(int id)
        {
            return _kategoriDal.GetByIdKitabaGore(id);
        }

        public void Update(Kategori entity)
        {
            _kategoriDal.Update(entity);
        }
    }
}
