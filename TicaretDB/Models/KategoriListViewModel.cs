using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Entities;

namespace TicaretDB.Models
{
    public class KategoriListViewModel
    {
        public string  SelectedKategori { get; set; }
        public List<Kategori> Kategoris { get; set; }
    }
}
