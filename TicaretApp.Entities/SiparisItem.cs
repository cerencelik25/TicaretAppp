using System;
using System.Collections.Generic;
using System.Text;

namespace TicaretApp.Entities
{
   public class SiparisItem
   {
        public int SiparisItemId { get; set; }

        public int SiparisId { get; set; }
        public Siparis Siparis { get; set; }

        public int KitapId { get; set; }
        public Kitap Kitap { get; set; }

        public decimal Fiyat { get; set; }

        public int Adet { get; set; }
    }
}
