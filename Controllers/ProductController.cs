using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TestMVC.Models;
using TestMVC.Services;
using System.Collections.Generic;

namespace TestMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment? WebHostEnvironment;
        private readonly IHttpContextAccessor? httpContextAccessor;
        public readonly APIHander ApiHander;
        public ProductController(IWebHostEnvironment? _webHostEnvironment,IHttpContextAccessor httpContext)
        {
            httpContextAccessor=httpContext;
            ApiHander = new(httpContextAccessor);
            _webHostEnvironment = WebHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(Product product)
        {
            try
            {
                //List<Product> product = new List<Product>();
                var data = ApiHander.ApiCall("Product/Create", product);
                TokenResponse tokenResponse =new TokenResponse();
                if (data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tokenResponse.Token = "200";
                    //tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(data.Content.ReadAsStringAsync().Result);
                }
                return Json(tokenResponse.Token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult GetAll()
        {
            try
            {
                List<Product> product = new List<Product>();
                var data = ApiHander.ApiCall("Product/GetAll", product);
                if (data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    product = JsonConvert.DeserializeObject<List<Product>>(data.Content.ReadAsStringAsync().Result);
                }
                return Json(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Edit(string id)
        {
            ViewData["editcode"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult Edited(string id)
        {
            try
            {
                Product product = new Product();
                product.ProductID = Convert.ToInt32(id);
                var data = ApiHander.ApiCallForEdit("/Product/GetById", product.ProductID);
                TokenResponse tokenResponse = new TokenResponse();
                List<Product> products =new List<Product>();
                if (data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tokenResponse.Token = "200";
                    products = JsonConvert.DeserializeObject<List<Product>>(data.Content.ReadAsStringAsync().Result);
                }
                return Json(products);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Delete(string id)
        {
            try
            {
                Product product = new Product();
                var data = ApiHander.ApiCall("/Product/Delete", Convert.ToInt32(id));
                TokenResponse tokenResponse = new TokenResponse();
                if (data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tokenResponse.Token = "200";
                    //tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(data.Content.ReadAsStringAsync().Result);
                }
                return Json(tokenResponse.Token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
