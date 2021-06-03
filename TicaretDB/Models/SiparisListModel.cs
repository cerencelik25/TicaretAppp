using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Entities;

namespace TicaretDB.Models
{
    public class SiparisListModel
    {
        public int SiparisId { get; set; }

        public string SiparisNumber { get; set; }
        public DateTime SiparisDate { get; set; }
        public EnumSiparisDurum SiparisDurum { get; set; }
        public EnumOdemeTipi OdemeTipi { get; set; }
        public string KullaniciAd { get; set; }
        public string KullaniciSoyad { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string SiparisNot { get; set; }

        public List<SiparisItemModel> SiparisItems { get; set; }

        public decimal TotalPrice()
        {
            return SiparisItems.Sum(i => i.Fiyat * i.Adet);
        }
        public int ToplamAdet()
        {
            return SiparisItems.Sum(a => a.Adet);
        }

    }
    public class SiparisItemModel
    {
        public int SiparisItemId { get; set; }
        public decimal Fiyat { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Adet { get; set; }
    }
}
