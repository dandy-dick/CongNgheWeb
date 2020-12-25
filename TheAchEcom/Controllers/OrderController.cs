using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheAchEcom.Models;
using Repository.DomainModels;
using Repository.BusinessModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using TheAchEcom.Models.PayPal;
using TheAchEcom.Models.NganLuong;

namespace TheAchEcom.Controllers
{
    public class OrderController : ApplicationController
    {
        private readonly EcomRepository Repository = new EcomRepository();
        private readonly IPageMaster PageMaster;
        private UserManager<Customer> UserManager { get; set; }
        private SignInManager<Customer> SignInManager { get; set; }

        public OrderController(
            IPageMaster pageMaster,
            UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            PageMaster = pageMaster;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IActionResult CheckOut()
        {
            var cart = PageMaster.GetShoppingCart();
            ViewBag.CountCartItems = cart.CartProducts.Count();
            return RedirectToAction("OrderDetail");
        }

        [HttpGet]
        [Route("CheckOut/OrderDetail")]
        public IActionResult OrderDetail()
        {
            var cart = PageMaster.GetShoppingCart();
            cart.CartProducts = Repository.GetCartItems(cart);
            ViewBag.CountCartItems = cart.CartProducts.Count();

            var model = new CheckOutModel
            {
                State = CheckOutState.OrderDetail,
                Cart = cart,
                TotalPrice = Repository.GetCartTotalPrice(cart),
                TotalQuantity = cart.CartProducts.Sum(p => p.Quantity)
            };

            return View(model);
        }

        [HttpPost]
        [Route("CheckOut/OrderDetail")]
        public IActionResult OrderDetail([FromForm]OrderDetailModel orderDetail)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("OrderPayment", "Order");
            }

            var cart = PageMaster.GetShoppingCart();
            cart.CartProducts = Repository.GetCartItems(cart);
            ViewBag.CountCartItems = cart.CartProducts.Count();

            var model = new CheckOutModel
            {
                State = CheckOutState.OrderDetail,
                Cart = cart,
                TotalPrice = Repository.GetCartTotalPrice(cart),
                TotalQuantity = cart.CartProducts.Sum(p => p.Quantity),
                OrderDetail = orderDetail
            };

            this.ViewBag.ModelStateErrorDictionary = this.GetModelStateDictionary<OrderDetailModel>();
            return View(model);
        }

        [Route("CheckOut/Payment")]
        public IActionResult OrderPayment()
        {
            var cart = PageMaster.GetShoppingCart();
            ViewBag.CountCartItems = cart.CartProducts.Count();

            var model = new CheckOutModel
            {
                State = CheckOutState.Payment
            };
            return View(model);
        }

        [Route("CheckOut/Complete")]
        public IActionResult OrderComplete()
        {
            ShoppingCart cart = PageMaster.GetShoppingCart();
            /* Clear cart items when purchase */
            if (SignInManager.IsSignedIn(User))
            {
                Repository.ShoppingCart_ClearItems(cart.Id);
            }
            else
            {
                cart.CartProducts.Clear();
                var responseCookie = JsonConvert.SerializeObject(cart);
                HttpContext.Response.Cookies.Append(_cartCookieName, responseCookie);
            }

            var model = new CheckOutModel
            {
                State = CheckOutState.Complete
            };
            ViewBag.CountCartItems = cart.CartProducts.Count();
            return View(model);
        }

        [HttpPost]
        [Route("/checkout/paypal/create")]
        public JsonResult PayPalCheckOut()
        {
            ShoppingCart cart = PageMaster.GetShoppingCart();

            var orderDetail = new {
                intent = "capture",
                purchase_units = new List<PurchaseUnit> {
                    new PurchaseUnit
                    {
                        amount = new Amount {
                            currency_code = "USD",
                            value = Repository.GetCartTotalPrice(cart),
                        },
                    }
                }
            };
            return new JsonResult(new { orderDetail });
        }

        [HttpPost]
        [Route("/checkout/nganluong/create")]
        public JsonResult NganLuongCheckOut()
        {
            ShoppingCart cart = PageMaster.GetShoppingCart();

            string host = $"{Request.Scheme}://{Request.Host}";
            var orderDetail = new NlOrderDetail
            {
                order_code = Guid.NewGuid().ToString(),
                price = Repository.GetCartTotalPrice(cart),
                quantity = cart.CartProducts.Sum(p => p.Quantity),
                return_url = host + "/CheckOut/Complete",
                cancel_url = host + "/CheckOut/Payment"
            };
            orderDetail.SetSecureCode();

            return new JsonResult(new { orderDetail });
        }
    }
}
