using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KoleksiyoncuCom.WebUi.Bussiness.Identity;
using KoleksiyoncuCom.WebUi.Bussiness.Models;
using KoleksiyoncuCom.Bussiness.WebUi.Models;

namespace KoleksiyoncuCom.Bussiness.WebUi.Controllers
{
    [Authorize(Roles = "Seller")]
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
                SellerId = seller.SellerId,
                SellerName = seller.NameAndSurname,
                City = seller.City,
                Adress = seller.Adress,
                Email = seller.EmailAdress,
                PhoneNumber = seller.PhoneNumber
            };
            return View(profileSettingsViewModel);
        }

        [HttpPost]
        public IActionResult Ayarlar(ProfileSettingsViewModel model)
        {
            var entity = new Seller()
            {
                SellerId = model.SellerId,
                NameAndSurname = model.SellerName,
                City = model.City,
                Adress = model.Adress,
                EmailAdress = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            _sellerService.Update(entity);
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
            if (productsSellerId != authorizedSellerId)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var categories = _categoryService.GetAll();
            var productModel = new ProductModel
            {
                ProductName = entity.ProductName,
                AllTimeClick = entity.AllTimeClick,
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                ProductUnitPrice = entity.ProductUnitPrice,
                ProductUnitsInStock = entity.ProductUnitsInStock,
                SellerId = entity.SellerId,
                Category = categories,
                ProductId = entity.ProductId
            };
            return View(productModel);
        }

        [HttpPost]
        public IActionResult Guncelle(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _productService.GetById(model.ProductId);
                entity.ProductName = model.ProductName;
                entity.AllTimeClick = model.AllTimeClick;
                entity.CategoryId = model.CategoryId;
                entity.Description = model.Description;
                entity.ImageUrl = model.ImageUrl;
                entity.ProductUnitPrice = model.ProductUnitPrice;
                entity.ProductUnitsInStock = model.ProductUnitsInStock;
                entity.SellerId = model.SellerId;

                _productService.Update(entity);
                TempData.Add("message", "Ürününüz başarıyla güncellendi.");
                return RedirectToAction("Urunlerim");
            }
            return View(model);
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

        public IActionResult Ekle()
        {
            return View(new ProductModel()
            {
                Category = _categoryService.GetAll(),
                SellerId = _usersAndSellersService.GetByUserId(_userManager.GetUserId(User)).SellerId
            });
        }

        [HttpPost]
        public IActionResult Ekle(ProductModel model)
        {
            var entity = new Product()
            {
                ProductName = model.ProductName,
                AllTimeClick = 0,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                ProductUnitPrice = model.ProductUnitPrice,
                ProductUnitsInStock = model.ProductUnitsInStock,
                SellerId = model.SellerId
            };
            if (ModelState.IsValid)
            {
                _productService.Add(entity);
                TempData.Add("message", "Ürün başarıyla eklendi.");
                return RedirectToAction("Urunlerim");
            }
            return View(model);
        }
    }
}