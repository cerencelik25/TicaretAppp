using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Business.Abstract;
using TicaretApp.Entities;
using TicaretDB.Models;

namespace TicaretDB.Controllers
{
    public class KitapController : Controller
    {
        private IKitapService _kitapService;

        public KitapController(IKitapService kitapService)
        {
            _kitapService = kitapService;

        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Kitap kitap = _kitapService.GetKitapDetails((int)id);
            if(kitap == null)
            {
                return NotFound();
            }
            return View(new KitapDetailsModel()
            {
                Kitap = kitap,
                Kategoris = kitap.KitapKategoris.Select(i => i.Kategori).ToList()
            });
        }
        public IActionResult List(string kategori,int page=1)
        {
            const int pageSize = 2;
            return View(new KitapListModel()
            {
                PageInfo=new PageInfo()
                {
                    TotalItems= _kitapService.GetSayiKategoriyeGore(kategori),
                    CurrentPage=page,
                    ItemPerPage= pageSize,
                    CurrentKategori= kategori
                },
                Kitaps = _kitapService.GetKitapKategoriyeGore(kategori,page, pageSize),

            });
        }
        public IActionResult Arama(string q)
        {
            
            return View(new KitapListModel()
            {
                Kitaps = _kitapService.GetAramaSonuc(q),

            });
        }
    }
}
