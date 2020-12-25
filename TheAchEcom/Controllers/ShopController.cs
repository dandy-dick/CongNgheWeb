using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheAchEcom.Models;
using Repository.DomainModels;
using Repository.BusinessModels.ShopList;
using Repository.BusinessModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace TheAchEcom.Controllers
{
    public class ShopController : ApplicationController
    {
        private readonly IPageMaster PageMaster;
        public ShopController(IPageMaster pageMaster)
        {
            PageMaster = pageMaster;
        }

        public IActionResult ShopList(ShopListOptions options)
        {
            var repo = new EcomRepository();
            var cart = PageMaster.GetShoppingCart();

            int total;
            var model = new ShopListModel
            {
                ShopList = repo.GetShopList(options, cart, out total),
                Brands = repo.GetAllBrands()
            };
            
            options.TotalItems = total;

            ViewBag.Options = options;
            ViewBag.CountCartItems = cart.CartProducts != null ? cart.CartProducts.Count() : 0;

            return View(model);
        }
    }
}
