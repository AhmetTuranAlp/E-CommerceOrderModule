using AutoMapper;
using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.Core.Asbtract;
using E_CommerceOrderModule.Core.Asbtract.Services;
using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Core.Entity;
using E_CommerceOrderModule.Web.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static E_CommerceOrderModule.Core.DTOs.ModelEnumsDTO;

namespace E_CommerceOrderModule.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public ProductController(IProductService productService, IBasketService basketService, IUserService userService)
        {
            _productService = productService;
            _basketService = basketService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetUserAsync();
            if (result.ResultStatus)
            {
                #region Session
                HttpContext.Session.SetString("UserId", result.ResultObject.Id.ToString());
                var basketAll = await _basketService.GetAllInBasketAsync(result.ResultObject.Id.ToString());
                if (basketAll.ResultStatus && basketAll.ResultObject.Count > 0)
                {
                    List<BasketDTO> basketsSession = basketAll.ResultObject.ToList();
                    if (basketsSession.Count > 0)
                        HttpContext.Session.Set<List<BasketDTO>>("BasketCard", basketsSession);
                    else
                        HttpContext.Session.Set<List<BasketDTO>>("BasketCard", new List<BasketDTO>());
                }
                else
                    HttpContext.Session.Set<List<BasketDTO>>("BasketCard", new List<BasketDTO>());

                #endregion
            }
            return RedirectToAction("List");
        }



        public async Task<IActionResult> List()
        {
            var products = await _productService.GetAllProductAsync();
            return View(products.ResultObject);
        }

        public async Task<IActionResult> BasketAdd(string id)
        {
            try
            {
                var userId = this.HttpContext.Session.GetString("UserId");
                var product = await _productService.GetProductAsync(id); // Ürün Çekiliyor.
                if (product.ResultObject != null && product.ResultStatus)
                {
                    var basketProduct = await _basketService.GetBasketProduct(userId, product.ResultObject.ProductId); //İlgili Ürün Sepet de Olup Olmadıgına Bakılıyor.
                    if (basketProduct.ResultStatus)
                    {
                        //Ürün Sepet de Varsa
                        basketProduct.ResultObject.Quantity += 1;
                        if (product.ResultObject.Stock >= basketProduct.ResultObject.Quantity)
                        {
                            var result = await _basketService.UpdateBasket(basketProduct.ResultObject);
                            if (result.ResultStatus)
                            {
                                #region Sepet Session'ı Güncelleniyor.
                                var basketAll = await _basketService.GetAllInBasketAsync(userId);
                                if (basketAll.ResultStatus)
                                {
                                    HttpContext.Session.Set<List<BasketDTO>>("BasketCard", basketAll.ResultObject.ToList());
                                    return Json(true);
                                }
                                else
                                    return Json(false);
                                #endregion

                            }
                            else
                                return Json(false);
                        }
                        else
                            return Json(false);
                    }
                    else
                    {
                        string basketId = string.Empty;
                        var basketAll = await _basketService.GetAllInBasketAsync(userId);
                        if (basketAll.ResultStatus && basketAll.ResultObject.Count > 0)
                            basketId = basketAll.ResultObject.FirstOrDefault().BasketId;
                        else
                            basketId = Operations.UniqueRandom(1, 9, 10);

                        //Ürün Sepet de Yoksa
                        if (product.ResultObject.Stock > 0)
                        {
                            BasketDTO basketDTO = new BasketDTO()
                            {
                                BasketId = basketId,
                                Price = product.ResultObject.SalePrice,
                                Status = Status.InBasket,
                                ProductName = product.ResultObject.Name,
                                Quantity = 1,
                                UpdateDate = DateTime.Now,
                                UploadDate = DateTime.Now,
                                UserCode = userId,
                                ProductCode = product.ResultObject.ProductId
                            };
                            var result = await _basketService.CreateBasket(basketDTO);
                            if (result.ResultStatus)
                            {
                                #region Sepet Session'ı Güncelleniyor.
                                 basketAll = await _basketService.GetAllInBasketAsync(userId);
                                if (basketAll.ResultStatus)
                                {
                                    HttpContext.Session.Set<List<BasketDTO>>("BasketCard", basketAll.ResultObject.ToList());
                                    return Json(true);
                                }
                                else
                                    return Json(false);
                                #endregion
                            }
                            else
                                return Json(false);
                        }
                        else
                            return Json(false);

                    }
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
