using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.WebUi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoleksiyoncuCom.WebUi.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ISellerService _sellerService;
        public ProductController(IProductService productService, ISellerService sellerService)
        {
            _productService = productService;
            _sellerService = sellerService;
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productDetailsViewModel = new ProductDetailsViewModel
            {
                Product = _productService.GetById((int)id),
                Seller = _sellerService.GetById(_productService.GetById((int)id).SellerId)
            };
            if(productDetailsViewModel.Product == null)
            {
                return NotFound();
            }
            return View(productDetailsViewModel);
        }
    }
}