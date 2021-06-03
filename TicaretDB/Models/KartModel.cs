using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicaretDB.Models
{
    public class KartModel
    {
        public int KartId { get; set; }
        public List<KartItemModel> KartItems { get; set; }
        public List<KategoriModel> Kategoris { get; set; }
        public decimal TotalPrice()
        {
            return KartItems.Sum(i => i.Fiyat * i.Adet);
        }
        public int ToplamAdet()
        {
            return KartItems.Sum(a => a.Adet);
        }

    }
    public class KartItemModel
    {
        public int KartItemId { get; set; }
        public int KitapId { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
        public string ImageUrl { get; set; }
        public int Adet { get; set; }

    }
}
