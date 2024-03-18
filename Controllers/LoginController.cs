using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TestMVC.Models;
using TestMVC.Services;

namespace TestMVC.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration? Configuration;
        private IHttpContextAccessor? HttpContextAccessor;
        public APIHander? APIHander;
        public LoginController(IConfiguration? configuration, IHttpContextAccessor? httpContextAccessor, IOptions<SessionOptions> options)
        {
            this.Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
            APIHander = new(httpContextAccessor);
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            string apiUrl = "Login/Login";
            var result = APIHander.ApiCall(apiUrl, loginModel);
            TokenResponse token =new TokenResponse();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                token = JsonConvert.DeserializeObject<TokenResponse>(result.Content.ReadAsStringAsync().Result);
            }
            return Json(token.Token);
            //ViewBag.Token = token.Token;
            //return Redirect("/Product/Index");
        }
    }
}
