using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.WebUi.Identity;
using KoleksiyoncuCom.WebUi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KoleksiyoncuCom.WebUi.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        private ICartService _cartService;
        private UserManager<ApplicationUser> _userManager;
        public SepetController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.Product.ProductId,
                    Name = i.Product.ProductName,
                    Price = (decimal)i.Product.ProductUnitPrice,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult SepeteEkle(int productId, int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User), productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SepettenSil(int productId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), productId);
            return RedirectToAction("Index");
        }
    }
}