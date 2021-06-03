using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Business.Abstract;
using TicaretApp.Entities;
using TicaretDB.Models;

namespace TicaretDB.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private IKitapService _kitapService;
        private IKategoriService _kategoriService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(IKitapService kitapService, IKategoriService kategoriService, IWebHostEnvironment hostEnvironment)
        {
            _kitapService = kitapService;
            _kategoriService = kategoriService;
            this._hostEnvironment = hostEnvironment;

        }
        public IActionResult KitapList()
        {
            return View(new KitapListModel()
            {
                Kitaps = _kitapService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateKitap()
        {

            return View(new KitapModel());
        }
        [HttpPost]
        public IActionResult CreateKitap(KitapModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Kitap
                {
                    KitapAdi = model.KitapAdi,
                    Aciklama = model.Aciklama,
                    Yayinyili = model.Yayinyili,
                    StokAdedi = model.StokAdedi,
                    Fiyati = model.Fiyati,
                    ImageUrl = model.ImageUrl
                };

                if (_kitapService.Create(entity)==true)
                {
                    return RedirectToAction("KitapList");
                }
                else  {
                    ViewBag.ErrorMessage = _kitapService.ErrorMessage;
                    return View(model);
                
                }
                

            }
            return View(model);
            
        }
        [HttpGet]
        public IActionResult EditKitap(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _kitapService.GetByIdKategoriyeGoreYeni((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new KitapModel()
            {
                KitapId = entity.KitapId,
                KitapAdi = entity.KitapAdi,
                Aciklama = entity.Aciklama,
                Yayinyili = entity.Yayinyili,
                StokAdedi = entity.StokAdedi,
                Fiyati = entity.Fiyati,
                ImageUrl = entity.ImageUrl,
                SelectedKategoris = entity.KitapKategoris.Select(i => i.Kategori).ToList()
            };
            ViewBag.Kategoris = _kategoriService.GetAll();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditKitap(KitapModel model,int[] kategoriIds,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _kitapService.GetById(model.KitapId);
            if (entity == null)
            {
                return NotFound();

            }
            entity.KitapAdi = model.KitapAdi;

                if (file !=null)
                {
                    entity.ImageUrl = file.FileName;
                    string ImageUrl = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(20).ToArray()).Replace(' ', '-');
                    ImageUrl = ImageUrl + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
                    var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot\\img", ImageUrl);
                    // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                }

            entity.Fiyati = model.Fiyati;
            entity.Aciklama = model.Aciklama;
            entity.Yayinyili = model.Yayinyili;
            entity.StokAdedi = model.StokAdedi;
            _kitapService.Update(entity,kategoriIds);
            return RedirectToAction("KitapList");
        }
            ViewBag.Kategoris = _kategoriService.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteKitap(int kitapId)
        {
            var entity = _kitapService.GetById(kitapId);
            if (entity != null)
            {
                _kitapService.Delete(entity);
            }
            return RedirectToAction("KitapList");
        }

        public IActionResult KategoriList()
        {
            return View(new KategoriListModel()
            {
                Kategoris = _kategoriService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult EditKategori(int id)
        {
            var entity = _kategoriService.GetByIdKitabaGore(id);
            return View(new KategoriModel()
            {
                KategoriAdi = entity.KategoriAdi,
                KategoriId = entity.KategoriId,
                Kitaps=entity.KitapKategoris.Select(p=>p.Kitap).ToList()
            });
        }
        [HttpPost]
        public IActionResult EditKategori(KategoriModel model)
        {
            var entity = _kategoriService.GetById(model.KategoriId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.KategoriAdi = model.KategoriAdi;
            _kategoriService.Update(entity);
            return RedirectToAction("KategoriList");
        }
        [HttpGet]
        public IActionResult CreateKategori()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateKategori(KategoriModel model)
        {
            var entity = new Kategori()
            {
                KategoriAdi = model.KategoriAdi
            };
            _kategoriService.Create(entity);
            return RedirectToAction("KategoriList");
        }
        [HttpPost]
        public IActionResult DeleteKategori(int kategoriid)
        {
            var entity = _kategoriService.GetById(kategoriid);
            if (entity != null)
            {
                _kategoriService.Delete(entity);
            }
            return RedirectToAction("KategoriList");
        }
        [HttpPost]
        public IActionResult DeleteFromKategori(int kategoriid,int kitapid)
        {
            _kategoriService.DeleteFromKategori(kategoriid,kitapid);
            return Redirect("/admin/editkategori/"+kategoriid);
        }
    }
}
