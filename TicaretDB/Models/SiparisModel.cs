using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicaretDB.Models
{
    public class SiparisModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Adres { get; set; }
        [Display(Name ="Şehir")]
        public string Sehir { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KartAy { get; set; }
        public string KartYil { get; set; }
        public string KartCvv { get; set; }
        public string KartNumarasi { get; set; }
        public string KartUzeriAd { get; set; }

        public KartModel KartModel { get; set; }
    }
}
