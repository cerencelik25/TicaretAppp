using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Abstract
{
    public interface IKitapDal:IRepository<Kitap>
    {
        List<Kitap> GetKitapKategoriyeGore(string kategori, int page,int pageSize);
        Kitap GetKitapDetails(int id);
        int GetSayiKategoriyeGore(string kategori);
        List<Kitap> GetAramaSonuc(string aranacak);
        Kitap GetByIdKategoriyeGoreYeni(int id);
        void Update(Kitap entity, int[] kategoriIds);
        
    }
}
