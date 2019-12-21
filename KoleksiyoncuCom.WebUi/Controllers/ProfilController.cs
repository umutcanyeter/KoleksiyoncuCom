using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.Entities;
using KoleksiyoncuCom.WebUi.Identity;
using KoleksiyoncuCom.WebUi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KoleksiyoncuCom.WebUi.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private IUsersAndSellersService _usersAndSellersService;
        private ISellerService _sellerService;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private UserManager<ApplicationUser> _userManager;
        public ProfilController(UserManager<ApplicationUser> userManager, ICategoryService categoryService, IProductService productService, IUsersAndSellersService usersAndSellersService, ISellerService sellerService)
        {
            _usersAndSellersService = usersAndSellersService;
            _sellerService = sellerService;
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
        }
        public IActionResult Ayarlar()
        {
            var usersAndProfile = _usersAndSellersService.GetByUserId(_userManager.GetUserId(User));
            var sellerId = usersAndProfile.SellerId;
            var seller = _sellerService.GetById(sellerId);
            var profileSettingsViewModel = new ProfileSettingsViewModel
            {
                Seller = seller
            };
            return View(profileSettingsViewModel);
        }

        [HttpPost]
        public IActionResult Ayarlar(ProfileSettingsViewModel model)
        {
            _sellerService.Update(model.Seller);
            TempData.Add("message", String.Format("Profil bilgileriniz başarıyla güncellendi!"));
            return View(model);
        }

        public IActionResult Urunlerim()
        {
            var userId = _userManager.GetUserId(User);
            var sellerId = _usersAndSellersService.GetByUserId(userId).SellerId;
            List<Product> products = _productService.GetBySellerId(sellerId);
            var profileProductsViewModel = new ProfileProductsViewModel
            {
                Products = products
            };
            return View(profileProductsViewModel);
        }

        public IActionResult Guncelle(int? id)
        {
            var productsSellerId = _productService.GetById((int)id).SellerId;
            var authorizedSellerId = _usersAndSellersService.GetByUserId(_userManager.GetUserId(User)).SellerId;
            if(productsSellerId != authorizedSellerId)
            {
                return NotFound();
            }
            if(id == null)
            {
                return NotFound();
            }
            var productUpdateViewModel = new ProductUpdateViewModel
            {
                Product = _productService.GetById((int)id),
                Category = _categoryService.GetAll()
            };
            return View(productUpdateViewModel);
        }

        [HttpPost]
        public IActionResult Guncelle(ProductUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData.Add("message", "Boşlukları doldurunuz.");
            }
            _productService.Update(model.Product);
            TempData.Add("message", "Ürününüz başarıyla güncellendi.");
            return RedirectToAction("Urunlerim", "Profil");
        }

        public IActionResult Sil(int? id)
        {
            var productsSellerId = _productService.GetById((int)id).SellerId;
            var authorizedSellerId = _usersAndSellersService.GetByUserId(_userManager.GetUserId(User)).SellerId;
            if (productsSellerId != authorizedSellerId)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }
            _productService.Delete((int)id);
            TempData.Add("message", "Ürününüz başarıyla silindi.");
            return RedirectToAction("Urunlerim", "Profil");
        }
    }
}