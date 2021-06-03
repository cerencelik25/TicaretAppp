using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicaretApp.Business.Abstract;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.DataAccess.Concrete.EfCore;
using TicaretApp.Entities;

namespace TicaretApp.Business.Concrete
{
    public class KitapManager : IKitapService
    {
        private IKitapDal _kitapDal;

        public KitapManager(IKitapDal kitapDal)
        {
            _kitapDal = kitapDal;
        }

        public bool Create(Kitap entity)
        {
            if (Validate(entity))
            {
                _kitapDal.Create(entity);
                return true;
            }
            return false;
        }

        public void Delete(Kitap entity)
        {
            _kitapDal.Delete(entity);
        }

        public List<Kitap> GetAll()
        {
            return _kitapDal.GetAll();
        }

        public Kitap GetById(int id)
        {
            return _kitapDal.GetById(id);
        }

        public Kitap GetByIdKategoriyeGoreYeni(int id)
        {
            return _kitapDal.GetByIdKategoriyeGoreYeni(id);
        }

        public Kitap GetKitapDetails(int id)
        {
            return _kitapDal.GetKitapDetails(id);
        }

        public List<Kitap> GetKitapKategoriyeGore(string kategori, int page, int pageSize)
        {
            return _kitapDal.GetKitapKategoriyeGore(kategori, page,pageSize);
        }

        public int GetSayiKategoriyeGore(string kategori)
        {
            return _kitapDal.GetSayiKategoriyeGore(kategori);
        }

       

        public void Update(Kitap entity)
        {
            _kitapDal.Update(entity);
        }

        public void Update(Kitap entity, int[] kategoriIds)
        {
            _kitapDal.Update(entity,kategoriIds);
        }

        public string ErrorMessage { get ; set ; }

        public bool Validate(Kitap entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.KitapAdi))
            {
                ErrorMessage += "Kitap ismi girmelisiniz";
                isValid = false;
            }
            return isValid;
        }

        public List<Kitap> GetAramaSonuc(string aranacak)
        {
            return _kitapDal.GetAramaSonuc(aranacak);
        }
    }
}
