using LinkPoint.MVC.Areas.Admin.ViewModels;
using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LinkPoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7255/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
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
        public async Task<IActionResult> Login(AdminViewModel adminViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(baseAdress + "/Admin/Login", adminViewModel);
                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenViewModel>();
                    HttpContext.Session.SetString("JWToken", tokenResponse.AccesToken);
                    HttpContext.Response.Cookies.Append("UserId", tokenResponse.UserId);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, apiError.message);
                }
            }
            return View(adminViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/Admin/LogOut");
            response.EnsureSuccessStatusCode();
            HttpContext.Session.Remove("JWToken");
            HttpContext.Response.Cookies.Delete("UserId");
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var callBackUrl = Url.Action("ResetPassword", "Auth", null, Request.Scheme);
                forgotPasswordViewModel.callbackUrl = callBackUrl;
                var response = await client.PostAsJsonAsync(baseAdress + "/Admin/ForgotPassword", forgotPasswordViewModel);
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
                var response = await client.PostAsJsonAsync(baseAdress + "/Admin/ResetPassword", resetPasswordViewModel);
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
    }
}
