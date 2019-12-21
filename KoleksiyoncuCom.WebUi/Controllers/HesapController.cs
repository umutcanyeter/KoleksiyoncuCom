using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.Entites;
using KoleksiyoncuCom.Entities;
using KoleksiyoncuCom.WebUi.EmailServices;
using KoleksiyoncuCom.WebUi.Identity;
using KoleksiyoncuCom.WebUi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KoleksiyoncuCom.WebUi.Controllers
{
    public class HesapController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private ISellerService _sellerService;
        private IUsersAndSellersService _userAndSellersService;
        private IEmailSender _emailSender;
        private ICartService _cartService;
        public HesapController(ICartService cartService, IEmailSender emailSender, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISellerService sellerService, IUsersAndSellersService userAndSellersService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _sellerService = sellerService;
            _userAndSellersService = userAndSellersService;
            _emailSender = emailSender;
            _cartService = cartService;
        }
        public IActionResult KayitOl()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = Url.Action("ConfirmEmail", "Hesap", new
                {
                    userId = user.Id,
                    token = code
                });
                _cartService.InitializeCart(user.Id);
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı Onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:44301{callBackUrl}'>tıklayınız.</a>");
                var seller = new Seller
                {
                    NameAndSurname = user.UserName,
                    EmailAdress = user.Email
                };
                _sellerService.Add(seller);
                var sellerId = seller.SellerId;
                _userAndSellersService.Add(new UsersAndSellers { SellerId = sellerId, UserId = user.Id });
                return RedirectToAction("GirisYap", "Hesap");
            }
            ModelState.AddModelError("", "Bilinmeyen hata oluştu lütfen tekrar deneyiniz.");
            return View(model);
        }

        public IActionResult GirisYap()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(LoginModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? "~/";
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "Böyle bir koleksiyoncu bulunamadı.");
                return View(model);
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Bu hesap e-posta ile onaylanmamış.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            ModelState.AddModelError("", "Kullanıcı adı veya parola yanlış!");
            return View(model);
        }

        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                TempData["message"] = "Geçersiz onay kodu.";
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Hesabınız onaylandı.";
                    return View();
                }
            }
            TempData["message"] = "Hesabınız onaylanamadı.";
            return View();
        }
    }
}