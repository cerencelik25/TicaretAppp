using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Entities;

namespace TicaretDB.Models
{
    public class KitapModel
    {
        public int KitapId { get; set; }
        //[Required]
        public string KitapAdi { get; set; }
        [Required]
        public string Yayinyili { get; set; }
        [Required]
        [Range(1,1000)]
        public int? StokAdedi { get; set; }
        [Required]
        [Range(1,1000)]
        public decimal? Fiyati { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(100000, MinimumLength = 15, ErrorMessage = "!!Ürün açıklaması en az 15, en fazla 100 karakter olmalı!!")]
        public string Aciklama { get; set; }
        
        public List<Kategori> SelectedKategoris { get; set; }
       
    }
}
