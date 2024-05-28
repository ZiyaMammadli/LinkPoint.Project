using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LinkPoint.MVC.Controllers;

public class AuthUserProfileController : Controller
{
    Uri baseAdress = new Uri("https://localhost:7255/api");
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthUserProfileController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWToken");
        var userId = HttpContext.Request.Cookies["UserId"];
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(baseAdress + "/AccountSettings/GetAuthUserInfo/" + userId);
        response.EnsureSuccessStatusCode();
        var json1 = await response.Content.ReadAsStringAsync();
        var userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(json1);
        AuthUserProfileViewModel authUserProfileViewModel = new AuthUserProfileViewModel()
        {
            Token = token,
            UserInfo=userInfo,
        };
        return View(authUserProfileViewModel);
    }
}
