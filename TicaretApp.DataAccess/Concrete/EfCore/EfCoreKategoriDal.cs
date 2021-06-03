
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Concrete.EfCore
{
    public class EfCoreKategoriDal : EfCoreGenericRepositery<Kategori, TicaretContext>, IKategoriDal
    {
        public void DeleteFromKategori(int kategoriid, int kitapid)
        {
            using (var context = new TicaretContext())
            {
                var sql = @"delete  from  KitapKategori where KategoriId={0} And KitapId={1}";
                context.Database.ExecuteSqlCommand(sql, kategoriid, kitapid);
            }
        }

        public Kategori GetByIdKitabaGore(int id)
        {
            using (var context = new TicaretContext())
            {
                return context.Kategoris
                    .Where(i => i.KategoriId == id)
                    .Include(i => i.KitapKategoris)
                    .ThenInclude(i => i.Kitap)
                    .FirstOrDefault();
            }
        }
    }
}
