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

                #region Sepet Session'ı Güncelleniyor.

                //var basketAll = await _basketService.GetAllBasketAsync();
                //List<BasketDTO> basketsSession = basketAll.ResultObject.Where(x => x.Status == ModelEnumsDTO.Status.Active && x.UserCode == userId).ToList();
                HttpContext.Session.Set<List<BasketDTO>>("BasketCard", new List<BasketDTO>());
                return Json(true);
                #endregion


                //var userId = this.HttpContext.Session.GetString("UserId");
                //var basketList = await _basketService.GetAllBasketAsync();
                //if (basketList.ResultStatus && basketList.ResultObject.Count > 0)
                //{
                //    var baskets = basketList.ResultObject.Where(x => x.UserCode == userId).ToList();

                //    #region Ödeme Modeline Bilgiler Set Ediliyor.
                //    SalesDTO sales = new SalesDTO()
                //    {
                //        Status = ModelEnumsDTO.Status.Active,
                //        UploadDate = DateTime.Now,
                //        UpdateDate = DateTime.Now,

                //    };
                //    #endregion

                //    var productList = await _productService.GetAllProductAsync();

                //    foreach (var x in baskets)
                //    {
                //        #region Ürün Stok Bilgisi Güncelleniyor.
                //        if (productList.ResultStatus && productList.ResultObject.Count > 0)
                //        {
                //            var product = productList.ResultObject.Where(c => c.ProductId == x.ProductCode).FirstOrDefault();
                //            if (product != null)
                //            {
                //                product.Stock -= x.Quantity;
                //                var res = await _productService.UpdateProduct(product);
                //            }
                //        }
                //        #endregion

                //        #region Sepetdeki Ürün Satış İşleminden Dolayı Statusu Silindiye Çekiliyor.
                //        x.Status = ModelEnumsDTO.Status.Deleted;
                //        await _basketService.UpdateBasket(x);
                //        #endregion

                //        #region Ödeme Modeline Bilgiler Set Ediliyor.                 
                //        sales.TotalPrice += x.Price * x.Quantity;
                //        sales.PaymentType = "Kredi Kartı (Tek Çekim)";
                //        sales.TotalQuantity += x.Quantity;
                //        sales.UserCode = userId;

                //        var userDto =await _userService.GetUserAsync();
                //        if (userDto.ResultStatus)
                //        {
                //            sales.UserName = userDto.ResultObject.UserName;
                //        }

                //        #endregion
                //    }

                //    var result =await _saleService.CreateSales(sales);
                //    if (result.ResultStatus)
                //    {
                //        #region Sepet Session'ı Güncelleniyor.

                //        var basketAll = await _basketService.GetAllBasketAsync();
                //        List<BasketDTO> basketsSession = basketAll.ResultObject.Where(x => x.Status ==ModelEnumsDTO.Status.Active && x.UserCode == this.HttpContext.Session.GetString("UserId")).ToList();
                //        HttpContext.Session.Set<List<BasketDTO>>("BasketCard", basketsSession);
                //        return Json(true);
                //        #endregion
                //    }
                //    else
                //    {
                //        return Json(false);
                //    }
                //}
                //else
                //    return Json(false);


            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
