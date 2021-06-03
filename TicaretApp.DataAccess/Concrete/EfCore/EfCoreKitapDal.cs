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
    public class EfCoreKitapDal : EfCoreGenericRepositery<Kitap, TicaretContext>, IKitapDal
    {
        public void Create(Kitap entity, int[] kategoriIds)
        {
            using (var context = new TicaretContext())
            {
                var kitap = context.Kitaps
                    .Include(i => i.KitapKategoris)
                    .FirstOrDefault(i => i.KitapId == entity.KitapId);

                if (kitap != null)
                {
                    kitap.KitapAdi = entity.KitapAdi;
                    kitap.ImageUrl = entity.ImageUrl;
                    kitap.Fiyati = entity.Fiyati;
                    kitap.Aciklama = entity.Aciklama;
                    kitap.Yayinyili = entity.Yayinyili;
                    kitap.StokAdedi = entity.StokAdedi;
                    kitap.KitapKategoris = kategoriIds.Select(catid => new KitapKategori()
                    {
                        KategoriId = catid,
                        KitapId = entity.KitapId

                    }).ToList();
                    context.SaveChanges();
                }
            }
        }

        public List<Kitap> GetAramaSonuc(string aranacak)
        {
            using (var context = new TicaretContext())
            {
                var kitaps = context
                    .Kitaps
                    .AsQueryable()
                    .Where(i => i.KitapAdi.Contains(aranacak) || i.Aciklama.Contains(aranacak));
                return kitaps.ToList();
            }
        }

        public Kitap GetByIdKategoriyeGoreYeni(int id)
        {
            using (var context = new TicaretContext())
            {
                return context.Kitaps
                    .Where(i => i.KitapId == id)
                    .Include(i => i.KitapKategoris)
                    .ThenInclude(i => i.Kategori)
                    .FirstOrDefault();
            }
        }

        public Kitap GetKitapDetails(int id)
        {
            using (var context =new TicaretContext())
            {
                return context.Kitaps
                    .Where(i => i.KitapId == id)
                    .Include(i => i.KitapKategoris)
                    .ThenInclude(i => i.Kategori)
                    .FirstOrDefault();
            }
        }

        public List<Kitap> GetKitapKategoriyeGore(string kategori, int page,int pageSize)
        {
            using (var context = new TicaretContext())
            {
                var kitaps = context.Kitaps.AsQueryable();
                if (!string.IsNullOrEmpty(kategori))
                {
                    kitaps = kitaps
                        .Include(i => i.KitapKategoris)
                        .ThenInclude(i => i.Kategori)
                        .Where(i => i.KitapKategoris.Any(a => a.Kategori.KategoriAdi.ToLower() == kategori.ToLower()));
                }
                return kitaps.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }

        public List<Kitap> GetPopulerKitaps()
        {
            throw new NotImplementedException();
        }

        public int GetSayiKategoriyeGore(string kategori)
        {
            using (var context = new TicaretContext())
            {
                var kitaps = context.Kitaps.AsQueryable();
                if (!string.IsNullOrEmpty(kategori))
                {
                    kitaps = kitaps
                        .Include(i => i.KitapKategoris)
                        .ThenInclude(i => i.Kategori)
                        .Where(i => i.KitapKategoris.Any(a => a.Kategori.KategoriAdi.ToLower() == kategori.ToLower()));
                }
                return kitaps.Count();
            }
        }

        public void Update(Kitap entity, int[] kategoriIds)
        {
            using (var context = new TicaretContext())
            {
                var kitap = context.Kitaps
                    .Include(i=>i.KitapKategoris)
                    .FirstOrDefault(i=>i.KitapId==entity.KitapId);

                if (kitap !=null)
                {
                   kitap.KitapAdi = entity.KitapAdi;
                   kitap.ImageUrl = entity.ImageUrl;
                   kitap.Fiyati =   entity.Fiyati;
                   kitap.Aciklama = entity.Aciklama;
                   kitap.Yayinyili= entity.Yayinyili;
                   kitap.StokAdedi= entity.StokAdedi;
                    kitap.KitapKategoris = kategoriIds.Select(catid=>new KitapKategori() { 
                        KategoriId=catid,
                        KitapId= entity.KitapId

                    }).ToList();
                    context.SaveChanges();
                }
            }
        }
    }
}
