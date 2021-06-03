using System;
using System.Collections.Generic;
using System.Text;

namespace TicaretApp.Entities
{
    public class Kart
    {
        public int KartId { get; set; }
        public string UserId { get; set; }

        public List<KartItem> KartItems { get; set; }
    }
}
