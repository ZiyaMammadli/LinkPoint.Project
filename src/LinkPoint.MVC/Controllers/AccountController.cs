using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LinkPoint.MVC.Controllers
{
    public class AccountController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7255/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var client=_httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(baseAdress + "/Account/Login", loginViewModel);
                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenViewModel>();
                    HttpContext.Session.SetString("JWToken", tokenResponse.AccesToken);
                    HttpContext.Response.Cookies.Append("UserId",tokenResponse.UserId);
                    return RedirectToAction("Index", "NewsFeed");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, apiError.message);
                }
            }
            return View(loginViewModel);      
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(baseAdress + "/Account/Register", registerViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("login", "account");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, apiError.message);
                }
            }
            return View(registerViewModel);
        }       
    }
}
