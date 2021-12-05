using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Business.Abstract;
using TicaretDB.EmailServices;
using TicaretDB.Extensions;
using TicaretDB.Identity;
using TicaretDB.Models;

namespace TicaretDB.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private IKartService _kartService;     

        public AccountController(IKartService kartService,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _kartService = kartService;
            _userManager = userManager;
            _signInManager = signInManager;   
            _emailSender = emailSender;
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName=model.Username,
                Email=model.Email,
                FullName=model.FullName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                //send email
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız!",$"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:38383{callbackUrl}'>tıklayınız</a> ");

                TempData.Put("message", new ResultMessage()
                {
                    Baslık="Hesap Onayı",
                    Message="Eposta adresinize gelen link ile hesabınızı onaylayınız",
                    Css="warning"
                });

                return RedirectToAction("Login");
            }

           // ModelState.AddModelError("", "Şifre en az bir büyük ve küçük harf içermeli");
            return View(model);
        }

        

        public IActionResult Login(string ReturnUrl="")
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            }) ;
        }
        [HttpPost]    
        public async Task<IActionResult> Login(LoginModel model)
        {
           
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanici kayıtlı değil.Lütfen Tekrar deneyiniz");
                return View(model);            
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen hesabınızı mail ile onaylayınız");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }
            
                ModelState.AddModelError("", "Email veya parola yanlış!!");
                return Redirect(model.ReturnUrl ?? "/Account/Login");
            
           
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new ResultMessage()
            {
                Baslık = "Oturum Kapatıldı",
                Message = "Hesabınız güvenli bir şekilde sonlandırıldı",
                Css = "warning"
            });
            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId==null || token==null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Baslık = "Hesap Onayı",
                    Message = "Hesap onayı için bilgileriniz yanlış. Lütfen kontrol ediniz",
                    Css = "danger"
                });
                return Redirect("~/");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //create cart object
                    _kartService.InitializeCart(user.Id);

                    TempData.Put("message", new ResultMessage()
                    {
                        Baslık = "Hesap Onayı",
                        Message = "Hesabınız başarılı bir şekilde onaylanmıştır",
                        Css = "success"
                    });
                    return RedirectToAction("Login");
                }
              
            }

            TempData.Put("message", new ResultMessage()
            {
                Baslık = "Hesap Onayı",
                Message = "Hesabınız onaylanamadı",
                Css = "danger"
            });
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData.Put("message", new ResultMessage()
                {
                    Baslık = "Şifremi Unuttum",
                    Message = "Bilgileriniz hatalı",
                    Css = "danger"
                });
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Baslık = "Şifremi Unuttum",
                    Message = "Bu eposta adresi ile bir kullanıcı bulunamadı",
                    Css = "danger"
                });
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            //generate token
            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = code
            });
            //send email
            await _emailSender.SendEmailAsync(Email, "Şifrenizi Güncelleyiz", $"Şifre değişikliği için lütfen linke <a href='http://localhost:38383{callbackUrl}'>tıklayınız</a> ");
            TempData.Put("message", new ResultMessage()
            {
                Baslık = "Şifremi Unuttum",
                Message = "Parolanızı yenilemeniz için hesabınıza mail gönderildi",
                Css = "warning"
            });

            return View();
        }
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var model = new ResetPasswordModel { Token = token };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View(model);
        }
      
        public IActionResult Accessdenied()
        {
            return View();
        }
    }

   
}
