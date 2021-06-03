using System;
using System.Collections.Generic;
using System.Text;

namespace TicaretApp.Entities
{
   public  class Siparis
    {
        public Siparis()
        {
            SiparisItems = new List<SiparisItem>();
        }
        public int SiparisId { get; set; }

        public string SiparisNumber { get; set; }
        public DateTime dateTime { get; set; }
        public string UserId { get; set; }
        public EnumSiparisDurum SiparisDurum { get; set; }
        public EnumOdemeTipi OdemeTipi { get; set; }
        public string  KullaniciAd { get; set; }
        public string KullaniciSoyad { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string SiparisNot { get; set; }
        
        public string OdemeId { get; set; }
        public string OdemeToken { get; set; }
        public string ConversationId { get; set; }

        public List<SiparisItem> SiparisItems { get; set; }
    }

    public enum EnumSiparisDurum
    {
        onaybekliyor=0,
        odenmemis=1,
        tamamlandi=2,
        hazirlaniyor=3,
        yolda=4,
        teslimedildi=5
    }

    public enum EnumOdemeTipi
    {
        KrediKart=0,
        Eft=1,
        KapidaOdeme=2
    }
}
