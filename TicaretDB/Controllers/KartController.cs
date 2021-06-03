using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Business.Abstract;
using TicaretApp.Entities;
using TicaretDB.Identity;
using TicaretDB.Models;

namespace TicaretDB.Controllers
{
    [Authorize]
    public class KartController : Controller
    {
        private IKartService _kartSerivce;
        private ISiparisService _siparisService;
        private UserManager<ApplicationUser> _userManager;
        public KartController(ISiparisService siparisService,IKartService kartService, UserManager<ApplicationUser> userManager)
        {
            _siparisService = siparisService;
            _kartSerivce = kartService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var Kart = _kartSerivce.GetKartUserIdyeGore(_userManager.GetUserId(User));
           return View(new KartModel()
            {
                KartId = Kart.KartId,
                KartItems = Kart.KartItems.Select(i => new KartItemModel()
                {
                    KartItemId = i.KartItemId,
                    KitapId = i.Kitap.KitapId,
                    Ad = i.Kitap.KitapAdi,
                    Fiyat = (decimal)i.Kitap.Fiyati,
                    ImageUrl = i.Kitap.ImageUrl,
                    Adet = i.Adet
                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult SepeteEkle(int kitapid, int adet)
        {
            _kartSerivce.SepeteEkle(_userManager.GetUserId(User), kitapid, adet);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SepettenSil(int kitapId)
        {
            _kartSerivce.SepettenSil(_userManager.GetUserId(User), kitapId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var Kart = _kartSerivce.GetKartUserIdyeGore(_userManager.GetUserId(User));

            var siparisModel = new SiparisModel();

            siparisModel.KartModel = new KartModel()
            {
                KartId = Kart.KartId,
                KartItems = Kart.KartItems.Select(i => new KartItemModel()
                {
                    KartItemId = i.KartItemId,
                    KitapId = i.Kitap.KitapId,
                    Ad = i.Kitap.KitapAdi,
                    Fiyat = (decimal)i.Kitap.Fiyati,
                    ImageUrl = i.Kitap.ImageUrl,
                    Adet = i.Adet
                }).ToList()
            };
            return View(siparisModel);
        }
        [HttpPost]
        public IActionResult Checkout(SiparisModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var kart = _kartSerivce.GetKartUserIdyeGore(userId);
                model.KartModel = new KartModel()
                {
                    KartId=kart.KartId,
                    KartItems=kart.KartItems.Select(i=> new KartItemModel() {
                        KartItemId = i.KartItemId,
                        KitapId = i.Kitap.KitapId,
                        Ad = i.Kitap.KitapAdi,
                        Fiyat = (decimal)i.Kitap.Fiyati,
                        ImageUrl = i.Kitap.ImageUrl,
                        Adet = i.Adet

                    }).ToList()
                };
                //odeme
                var payment = PaymentProcess(model);
                if (payment.Status == "success")
                {
                    SaveOrder(model, payment, userId);
                    ClearKart(kart.KartId.ToString());
                    return View("success");
                }
            }
            return View(model);
          }

        private void SaveOrder(SiparisModel model, Payment payment, string userId)
        {
            var siparis = new Siparis();
            siparis.SiparisNumber = new Random().Next(111111, 999999).ToString();
            siparis.SiparisDurum = EnumSiparisDurum.hazirlaniyor;
            siparis.OdemeTipi = EnumOdemeTipi.KrediKart;
            siparis.OdemeId = payment.PaymentId;
            siparis.ConversationId = payment.ConversationId;
            siparis.dateTime = new DateTime();
            siparis.KullaniciAd = model.Ad;
            siparis.KullaniciSoyad = model.Soyad;
            //siparis.Email = model.Email;
            siparis.Telefon = model.Telefon;
            siparis.Adres = model.Adres;
            siparis.UserId = userId;

            foreach (var item in model.KartModel.KartItems)
            {
                var siparisitem = new SiparisItem()
                {
                    Fiyat = item.Fiyat,
                    Adet = item.Adet,
                    KitapId=item.KitapId
                };
                siparis.SiparisItems.Add(siparisitem);
            }
            _siparisService.Create(siparis);
        }
        private void ClearKart(string kartId)
        {
            _kartSerivce.ClearKart(kartId);
        }

        private Payment PaymentProcess(SiparisModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-htr9LnkN3vjGJWoTUtTlgSmLIT1o26Uz";
            options.SecretKey = "sandbox-8xZgFMWd15VV49wGHS49e2RhDN4o1GxS";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.KartModel.TotalPrice().ToString().Split(",")[0];
            request.PaidPrice = model.KartModel.TotalPrice().ToString().Split(",")[0];
            request.Currency = Currency.TRY.ToString().Split(",")[0];
            request.Installment = 1;
            request.BasketId = model.KartModel.KartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.KartUzeriAd;
            paymentCard.CardNumber = model.KartNumarasi;
            paymentCard.ExpireMonth = model.KartAy;
            paymentCard.ExpireYear = model.KartYil;
            paymentCard.Cvc = model.KartCvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //paymentCard.CardHolderName = "John Doe5528790000000008"
            //paymentCard.CardNumber = "";
            //paymentCard.ExpireMonth = "12";
            //paymentCard.ExpireYear = "2030";
            //paymentCard.Cvc = "123";

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;

            foreach (var item in model.KartModel.KartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.KitapId.ToString();
                basketItem.Name = item.Ad;
                basketItem.Category1 = "Roman";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                if (item.Adet >1)
                {
                    basketItem.Price= ((item.Adet)*(item.Fiyat)).ToString().Split(",")[0];
                }
                else
                {
                    basketItem.Price = item.Fiyat.ToString().Split(",")[0];
                }
              
                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }

        public IActionResult GetSiparis()
        {
           var siparis= _siparisService.GetSiparis(_userManager.GetUserId(User));
            var siparisListModel = new List<SiparisListModel>();
            SiparisListModel siparisModel;

            foreach (var item in siparis)
            {
                siparisModel = new SiparisListModel();
                siparisModel.SiparisId = item.SiparisId;
                siparisModel.SiparisNumber = item.SiparisNumber;
                siparisModel.SiparisDate = item.dateTime;
                siparisModel.SiparisNot = item.SiparisNot;
                siparisModel.Telefon = item.Telefon;
                siparisModel.KullaniciAd = item.KullaniciAd;
                siparisModel.KullaniciAd = item.KullaniciAd;
                siparisModel.Email = item.Email;
                siparisModel.Adres = item.Adres;
                siparisModel.Sehir = item.Sehir;


                siparisModel.SiparisItems = item.SiparisItems.Select(i => new SiparisItemModel()
                {
                    SiparisItemId = i.SiparisId,//değişsebilir
                    Name=i.Kitap.KitapAdi,
                    Fiyat=i.Fiyat,
                    Adet=i.Adet,
                    ImageUrl=i.Kitap.ImageUrl
                }).ToList();
                siparisListModel.Add(siparisModel);
            }
            return View(siparisListModel);
        }
    }
}
