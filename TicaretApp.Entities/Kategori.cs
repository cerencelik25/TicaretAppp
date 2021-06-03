using System;
using System.Collections.Generic;
using System.Text;

namespace TicaretApp.Entities
{
    public class Kategori
    {
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }

        public List<KitapKategori> KitapKategoris { get; set; }
    }
}
