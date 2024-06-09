using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;

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
                var callBackUrl = Url.Action("EmailConfirm", "Account", null, Request.Scheme);
                registerViewModel.callbackUrl=callBackUrl;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/Account/LogOut");
            response.EnsureSuccessStatusCode();
            HttpContext.Session.Remove("JWToken");
            HttpContext.Response.Cookies.Delete("UserId");
            return RedirectToAction("Login","Account");
        }
        [HttpGet]
        public async Task <IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var callBackUrl = Url.Action("ResetPassword", "Account", null, Request.Scheme);
                forgotPasswordViewModel.callbackUrl = callBackUrl;
                var response = await client.PostAsJsonAsync(baseAdress + "/Account/ForgotPassword", forgotPasswordViewModel);
                if (response.IsSuccessStatusCode)
                {
                    string Message = "Successfully sent email";
                    ViewData["Confirm"] = Message;
                    return View();
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    //var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, errorResponse);
                }
            }
            return View(forgotPasswordViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (token is null || email is null) 
            {
                ModelState.AddModelError("", "Invalid password reset token.");
            }
            var decodedToken = WebUtility.UrlDecode(token);
            return View(new ResetPasswordViewModel { Token = decodedToken, Email = email });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(baseAdress + "/Account/ResetPassword", resetPasswordViewModel);
                if (response.IsSuccessStatusCode)
                {
                    string message = "Successfully changed password";
                    ViewData["confirm"] = message;
                    return View();
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    //var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, errorResponse);
                }
            }
            return View(resetPasswordViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EmailConfirm( string UserId, string code)
        {
            var decodedCode = WebUtility.UrlDecode(code);
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{baseAdress}/Account/Emailconfirm/{UserId}/{decodedCode}");
            if (response.IsSuccessStatusCode)
            {
                return View();
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                //var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                ModelState.AddModelError(string.Empty, errorResponse);
            }   
            return View();
        }
    }
}
