using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.Entites;
using KoleksiyoncuCom.Entities;
using KoleksiyoncuCom.WebUi.Bussiness.EmailServices;
using KoleksiyoncuCom.WebUi.Bussiness.Identity;
using KoleksiyoncuCom.WebUi.Bussiness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KoleksiyoncuCom.Bussiness.WebUi.Controllers
{
    public class HesapController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private ISellerService _sellerService;
        private IUsersAndSellersService _userAndSellersService;
        private IEmailSender _emailSender;
        private RoleManager<ApplicationRole> _roleManager;
        public HesapController(RoleManager<ApplicationRole> roleManager, IEmailSender emailSender, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISellerService sellerService, IUsersAndSellersService userAndSellersService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _sellerService = sellerService;
            _userAndSellersService = userAndSellersService;
            _emailSender = emailSender;
            _roleManager = roleManager;
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
                UserName = model.NameAndSurname,
                Email = model.EmailAdress
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
                await _emailSender.SendEmailAsync(model.EmailAdress, "Hesabınızı Onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:44301{callBackUrl}'>tıklayınız.</a>");
                var seller = new Seller
                {
                    NameAndSurname = model.NameAndSurname,
                    EmailAdress = model.EmailAdress,
                    City = model.City,
                    Adress = model.Adress,
                    PhoneNumber = model.PhoneNumber,
                    Rate = 0,
                    VerifiedSeller = false
                };
                _sellerService.Add(seller);
                var sellerId = seller.SellerId;
                _userAndSellersService.Add(new UsersAndSellers { SellerId = sellerId, UserId = user.Id });
                var addToRole = await _userManager.AddToRoleAsync(user, "Seller");
                if (addToRole.Succeeded)
                {
                    return RedirectToAction("GirisYap", "Hesap");
                }
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Böyle bir koleksiyoncu bulunamadı.");
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Bu hesap e-posta ile onaylanmamış.");
                return View(model);
            }
            if (!await _userManager.IsInRoleAsync(user,"Seller"))
            {
                ModelState.AddModelError("", "Bu hesap satıcı hesabı değil!");
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
            if (userId == null || token == null)
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