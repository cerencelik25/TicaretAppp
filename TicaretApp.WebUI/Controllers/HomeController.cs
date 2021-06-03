using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Business.Abstract;

namespace TicaretApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IKitapService _kitapService;
        public HomeController(IKitapService kitapService)
        {
            _kitapService = kitapService;
        }
        public IActionResult Index()
        {
            return View(_kitapService.GetAll());
        }
    }
}
