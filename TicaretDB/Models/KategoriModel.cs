using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Entities;

namespace TicaretDB.Models
{
    public class KategoriModel
    {
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }

        public List<Kitap> Kitaps { get; set; }
    }
}
