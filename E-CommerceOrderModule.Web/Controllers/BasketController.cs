using E_CommerceOrderModule.Core.Asbtract.Services;
using E_CommerceOrderModule.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CommerceOrderModule.Web.Headers;
using E_CommerceOrderModule.Web.RabbitMQ;

namespace E_CommerceOrderModule.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly ISaleService _saleService;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        public BasketController(IProductService productService, IBasketService basketService, IUserService userService, ISaleService saleService, RabbitMQPublisher rabbitMQPublisher)
        {
            _productService = productService;
            _basketService = basketService;
            _userService = userService;
            _saleService = saleService;
            _rabbitMQPublisher = rabbitMQPublisher;
        }
        public async Task<IActionResult> List()
        {
            var userId = this.HttpContext.Session.GetString("UserId");
            var result = await _basketService.GetAllBasketAsync();
            var basketDTO = result.ResultObject.Where(x => x.UserCode == userId).ToList();
            return View(basketDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteShopping()
        {
            try
            {
                var userId = this.HttpContext.Session.GetString("UserId");
                var user = await _userService.GetUserAsync();
                _rabbitMQPublisher.Publish(user.ResultObject);
                HttpContext.Session.Set<List<BasketDTO>>("BasketCard", new List<BasketDTO>());

                return Json(true);

            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
