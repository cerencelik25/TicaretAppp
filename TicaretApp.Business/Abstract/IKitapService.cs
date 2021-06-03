using System;
using System.Collections.Generic;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.Business.Abstract
{
    public interface IKitapService:IValidator<Kitap>
    {
        Kitap GetById(int id);
        Kitap GetKitapDetails(int id);
        List<Kitap> GetAll();
        List<Kitap> GetAramaSonuc(string aranacak);
        List<Kitap> GetKitapKategoriyeGore(string kategori,int page, int pageSize);
        Kitap GetByIdKategoriyeGoreYeni(int id);
        int GetSayiKategoriyeGore(string kategori);

        bool Create(Kitap entity);
        void Delete(Kitap entity);
        void Update(Kitap entity);
        void Update(Kitap entity, int[] kategoriIds);
        
      
    }
}
