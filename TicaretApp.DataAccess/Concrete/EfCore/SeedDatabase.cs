using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicaretApp.Entities;

namespace TicaretApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new TicaretContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Kategoris.Count() == 0)
                {
                    context.Kategoris.AddRange(Kategoris);
                }
                if(context.Kitaps.Count() == 0)
                {
                    context.Kitaps.AddRange(Kitaps);
                    context.AddRange(KitapKategori);
                }
                context.SaveChanges();
            }
        }
        private static Kategori[] Kategoris = {
            new Kategori() {KategoriAdi="Türk Roman"},
            new Kategori() {KategoriAdi="Dünya Roman"},
            new Kategori() {KategoriAdi="Türk Şiir"},
            new Kategori() {KategoriAdi="Dünya Şiir"}
        };

        private static Kitap[] Kitaps =
        {
           new Kitap() {KitapAdi="Buyuk Saat", Fiyati=18, ImageUrl="buyuksaat.jpg", Aciklama="<p>Güzel Kitap1</p>"},
           new Kitap() {KitapAdi="Camdaki Kız", Fiyati=30, ImageUrl="camdakikiz.png", Aciklama="<p>Güzel Kitap2</p>"},
           new Kitap() {KitapAdi="Hayvan Çiftliği", Fiyati=15, ImageUrl="hayvanciftligi.jpg", Aciklama="<p>Güzel Kitap3</p>"},
           new Kitap() {KitapAdi="Sen Aydınlatırsın Geceyi", Fiyati=21, ImageUrl="senaydinlatirsingeceyi.jpg", Aciklama="<p>Güzel Kitap4</p>"},
        };
        private static KitapKategori[] KitapKategori =
        {
            new KitapKategori(){ Kitap=Kitaps[0],Kategori = Kategoris[0]},
            new KitapKategori(){ Kitap=Kitaps[3],Kategori = Kategoris[1]},
            new KitapKategori(){ Kitap=Kitaps[2],Kategori = Kategoris[1]},
            new KitapKategori(){ Kitap=Kitaps[3],Kategori = Kategoris[3]},
            new KitapKategori(){ Kitap=Kitaps[3],Kategori = Kategoris[0]},
            new KitapKategori(){ Kitap=Kitaps[3],Kategori = Kategoris[2]},
            new KitapKategori(){ Kitap=Kitaps[1],Kategori = Kategoris[3]},
            new KitapKategori(){ Kitap=Kitaps[1],Kategori = Kategoris[2]},
            new KitapKategori(){ Kitap=Kitaps[0],Kategori = Kategoris[2]},
            new KitapKategori(){ Kitap=Kitaps[2],Kategori = Kategoris[0]},

        };
    }
}
