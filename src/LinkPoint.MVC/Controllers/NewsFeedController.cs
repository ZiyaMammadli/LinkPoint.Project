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

            var response3 = await client.GetAsync(baseAdress + "/Likes/GetAllLikes");
            response3.EnsureSuccessStatusCode();
            var json3 = await response3.Content.ReadAsStringAsync();
            var likes = JsonConvert.DeserializeObject<List<LikeGetAllViewModel>>(json3);

            var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllDontFollowingUsers/"+userId+"/"+4);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var dontFollowingUsers = JsonConvert.DeserializeObject<List<DontFollowingUsersViewModel>>(json4);

            var response5 = await client.GetAsync(baseAdress + "/FriendShips/GetAllAcceptedFollowingUsers/" + userId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var acceptedFollowingUsers = JsonConvert.DeserializeObject<List<AcceptedFollowingUsersGetViewModel>>(json5);
            NewsFeedViewModel newsFeedViewModel = new NewsFeedViewModel()
            {
                Token = token,
                UserInfo= userInfo,
                Posts= SortedPosts,
                LikeList=likes,
                DontFollowingUsers= dontFollowingUsers,
                AcceptedFollowingUsers= acceptedFollowingUsers
            };
            return View(newsFeedViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> SearchResults(string query)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response1 = await client.GetAsync(baseAdress + "/AccountSettings/GetAuthUserInfo/" + userId);
            response1.EnsureSuccessStatusCode();
            var json1 = await response1.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(json1);

            var response = await client.GetAsync(baseAdress + "/AccountSettings/GetAllUsers/"+query);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserGetViewModel>>(json);
            SearchVeiwModel searchVeiw = new SearchVeiwModel()
            {
                UserInfo=userInfo,
                Users=users
            };
            return PartialView("_UserDropdownPartial", searchVeiw);
        }
    }
}
