using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;
using KoleksiyoncuCom.WebUi.Models;
using Microsoft.AspNetCore.Identity;
using KoleksiyoncuCom.WebUi.Identity;

namespace KoleksiyoncuCom.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var homePageProductListViewModel = new HomePageProductListViewModel
            {
                Product = _productService.GetAll(),
                Category = _categoryService.GetAll()
            };
            return View(homePageProductListViewModel);
        }
    }
}