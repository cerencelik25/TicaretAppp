using System;
using System.Collections.Generic;
using System.Text;

namespace TicaretApp.Entities
{
    public class Kitap
    {
        public int KitapId { get; set; }
        public int YazarId { get; set; }
        public int KitapturId { get; set; }
        public int YayineviId { get; set; }
        public int YorumId { get; set; }
        public string KitapAdi { get; set; }
        public string Yayinyili { get; set; }
        public int? StokAdedi { get; set; }
        public decimal? Fiyati { get; set; }
        public string ImageUrl { get; set; }
        public string Aciklama { get; set; }
       
        public List<KitapKategori> KitapKategoris { get; set; }
    }
}
