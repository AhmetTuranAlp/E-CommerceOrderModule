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
            List<BasketDTO> basketList = new List<BasketDTO>();
            var result = await _basketService.GetAllInBasketAsync(this.HttpContext.Session.GetString("UserId"));
            if (result.ResultStatus && result.ResultObject.Count > 0)
                basketList = result.ResultObject;
            return View(basketList);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteShopping()
        {
            try
            {
                var userId = this.HttpContext.Session.GetString("UserId");
                var user = await _userService.GetUserAsync();
                if (user.ResultStatus)
                {
                    var basketAll = await _basketService.GetAllInBasketAsync(userId);
                    if (basketAll.ResultStatus && basketAll.ResultObject.Count > 0)
                    {
                        foreach (var basketItem in basketAll.ResultObject)
                        {
                            basketItem.Status = ModelEnumsDTO.Status.Sale;
                            await _basketService.UpdateBasket(basketItem);
                        }

                        var result = basketAll.ResultObject.FirstOrDefault();
                        BasketRequestDTO basketRequestDTO = new BasketRequestDTO()
                        {
                            BasketId = result.BasketId,
                            UserCode = result.UserCode
                        };
                        _rabbitMQPublisher.Publish(basketRequestDTO);
                        HttpContext.Session.Set<List<BasketDTO>>("BasketCard", new List<BasketDTO>());

                        return Json(true);
                    }
                    else
                        return Json(false);

                }
                else
                    return Json(false);

            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
