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
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await client.GetAsync(baseAdress+ "/AccountSettings/GetAuthUserInfo/"+ userId);
            response.EnsureSuccessStatusCode();               
            var json1 = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(json1);

            var response2 = await client.GetAsync(baseAdress + "/Posts/GetAllPosts");
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostGetViewModel>>(json2);
            var SortedPosts= posts.OrderByDescending(post=>post.PostId).ToList();
            NewsFeedViewModel newsFeedViewModel = new NewsFeedViewModel()
            {
                Token = token,
                UserInfo= userInfo,
                Posts= SortedPosts
            };
            return View(newsFeedViewModel);
        }
    }
}
