using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Concrete.EfCore
{
    public class EfCoreKartDal : EfCoreGenericRepositery<Kart, TicaretContext>, IKartDal
    {
        public override void Update(Kart entity)
        {
            using (var context = new TicaretContext())
            {
                context.Karts.Update(entity);
                context.SaveChanges();

            }
        }


        public Kart GetByUserId(string userId)
        {
            using (var context = new TicaretContext())
            {
                return context
                            .Karts
                            .Include(i => i.KartItems)
                            .ThenInclude(i => i.Kitap)
                            .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void SepettenSil(int kartId, int kitapId)
        {
            using (var context = new TicaretContext())
            {
                var cmd = @"delete  from  KartItem where KartId={0} And KitapId={1}";
                context.Database.ExecuteSqlCommand(cmd, kartId, kitapId);
            }
        }

        public void ClearKart(string kartId)
        {
            using (var context = new TicaretContext())
            {
                var cmd = @"delete  from  KartItem where KartId={0}";
                context.Database.ExecuteSqlCommand(cmd, kartId);
            }
        }
    }
}
