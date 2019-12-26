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
    [Authorize(Roles = "Buyer")]
    public class ProfilController : Controller
    {
        private IUsersAndBuyersService _usersAndBuyersService;
        private IBuyerService _buyerService;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private UserManager<ApplicationUser> _userManager;
        public ProfilController(UserManager<ApplicationUser> userManager, ICategoryService categoryService, IProductService productService, IUsersAndBuyersService usersAndBuyersService, IBuyerService buyerService)
        {
            _usersAndBuyersService = usersAndBuyersService;
            _buyerService = buyerService;
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
        }
        public IActionResult Ayarlar()
        {
            var usersAndProfile = _usersAndBuyersService.GetByUserId(_userManager.GetUserId(User));
            var buyerId = usersAndProfile.BuyerId;
            var buyer = _buyerService.GetById(buyerId);
            var profileSettingsViewModel = new ProfileSettingsViewModel
            {
                BuyerId = buyer.BuyerId,
                BuyerName = buyer.BuyerName,
                Adress = buyer.Adress,
                Email = buyer.Email,
                PhoneNumber = buyer.PhoneNumber,
                ProfileImageUrl = buyer.ProfileImageUrl
            };
            return View(profileSettingsViewModel);
        }

        [HttpPost]
        public IActionResult Ayarlar(ProfileSettingsViewModel model)
        {
            var entity = new Buyer()
            {
                BuyerId = model.BuyerId,
                BuyerName = model.BuyerName,
                Adress = model.Adress,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ProfileImageUrl = model.ProfileImageUrl
            };
            _buyerService.Update(entity);
            TempData.Add("message", String.Format("Profil bilgileriniz başarıyla güncellendi!"));
            return View(model);
        }
    }
}