using System;
using System.Collections.Generic;
using System.Text;

namespace TicaretApp.Entities
{
    public class KitapKategori
    {
        

        public int KitapId { get; set; }
        public Kitap Kitap { get; set; }
        
        
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

    }
}
