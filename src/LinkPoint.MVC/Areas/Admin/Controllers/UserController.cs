using LinkPoint.MVC.Areas.Admin.ViewModels;
using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace LinkPoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7255/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response2 = await client.GetAsync(baseAdress + "/Admin/GetAllUsersWithPages/"+1+"/"+10);
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<PaginatedUsersGetViewModel>(json2);
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> SearchResults(string query)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/Admin/GetAllUsersForAdmin/" + query);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserGetViewModel>>(json);
            SearchVeiwModel searchVeiw = new SearchVeiwModel()
            {
                Users = users
            };
            return PartialView("_userDropdownPartial", searchVeiw);
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> Detail(string UserId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/Admin/GetUserById/" + UserId);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<GetUserViewModel>(json);

            var response2 = await client.GetAsync(baseAdress + "/Admin/GetUserAbout/" + UserId);
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var userAbout = JsonConvert.DeserializeObject<UserAboutGetViewModel>(json2);

            var response3 = await client.GetAsync(baseAdress + "/Admin/GetUserWork/" + UserId);
            response3.EnsureSuccessStatusCode();
            var json3 = await response3.Content.ReadAsStringAsync();
            var userWork = JsonConvert.DeserializeObject<UserWorkGetViewModel>(json3);

            var response4 = await client.GetAsync(baseAdress + "/Admin/GetAllUserInterests/" + UserId);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var userInterests = JsonConvert.DeserializeObject<List<UserInterestGetViewModel>>(json4);

            var response5 = await client.GetAsync(baseAdress + "/Admin/GetUserEducation/" + UserId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var userEducation = JsonConvert.DeserializeObject<UserEducationGetViewModel>(json5);
            UserDetailViewModel userDetailViewModel = new UserDetailViewModel()
            {
                Token = token,
                UserInfo = userInfo,
                UserAbout = userAbout,
                UserWork = userWork,
                UserInterests = userInterests,
                UserEducation = userEducation,
            };
            return View(userDetailViewModel);
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> UserSoftDelete(string UserId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/Admin/UserSoftDelete/" + UserId);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index","User");
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> UserActivate(string UserId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/Admin/UserActivate/" + UserId);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index", "User");
        }
    }
}
