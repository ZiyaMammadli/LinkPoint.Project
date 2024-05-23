using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LinkPoint.MVC.Controllers
{
    public class NewsFeedController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7255/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public NewsFeedController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress+ "/AccountSettings/GetAuthUserInfo");
            if (response.IsSuccessStatusCode)
            {
                
                var data = await response.Content.ReadFromJsonAsync<UserInfoViewModel>();
                return View(data);
            }

            ModelState.AddModelError(string.Empty, "An error occurred while fetching data.");
            return View();
        }
    }
}
