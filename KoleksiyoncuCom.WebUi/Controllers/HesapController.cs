using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.Entites;
using KoleksiyoncuCom.Entities;
using KoleksiyoncuCom.WebUi.Identity;
using KoleksiyoncuCom.WebUi.Models;
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
        public HesapController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISellerService sellerService, IUsersAndSellersService userAndSellersService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _sellerService = sellerService;
            _userAndSellersService = userAndSellersService;
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
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {

                var seller = User.Identity;
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
    }
}