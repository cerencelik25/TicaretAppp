using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Concrete.EfCore
{
    public class EfCoreSipraisDal : EfCoreGenericRepositery<Siparis, TicaretContext>, ISiparisDal
    {
        public List<Siparis> GetSiparis(string userId)
        {
            using (var contex = new TicaretContext())
            {
                var siparis = contex.Siparis
                        .Include(i => i.SiparisItems)
                        .ThenInclude(i => i.Kitap)
                        .AsQueryable();
                if (!string.IsNullOrEmpty(userId))
                {
                    siparis = siparis.Where(i => i.UserId == userId);
                }
                return siparis.ToList();
            }
        }
    }
}
