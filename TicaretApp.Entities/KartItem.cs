using System;
using System.Collections.Generic;
using System.Text;

namespace TicaretApp.Entities
{
    public class KartItem
    {
        public int KartItemId { get; set; }

        public Kitap Kitap { get; set; }
        public int KitapId { get; set; }

        public Kart Kart { get; set; }
        public int KartId { get; set; }

        public int Adet { get; set; }
    }
}
