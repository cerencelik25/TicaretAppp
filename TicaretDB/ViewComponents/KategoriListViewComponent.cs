using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Business.Abstract;
using TicaretDB.Models;

namespace TicaretDB.ViewComponents
{
    public class KategoriListViewComponent:ViewComponent
    {
        private IKategoriService _kategoriService;
        public KategoriListViewComponent(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }
        public IViewComponentResult Invoke()
        {
            return View(new KategoriListViewModel()
            {
                SelectedKategori = RouteData.Values["kategori"]?.ToString(),
                Kategoris = _kategoriService.GetAll()
            }) ;

        }
    }
}
