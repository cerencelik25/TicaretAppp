using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Entities;

namespace TicaretDB.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentKategori { get; set; }
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);
        }
    }
    public class KitapListModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Kitap> Kitaps { get; set; }
       
    }
}
