using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace LinkPoint.MVC.Controllers
{
    public class UserProfileController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7255/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public UserProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet("/UserProfile/Index/{UserId}")]
        public async Task< IActionResult> Index(string UserId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAuthUserInfo/" + userId);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var authUserInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(json4);

            var response = await client.GetAsync(baseAdress + "/AccountSettings/GetAuthUserInfo/" + UserId);
            response.EnsureSuccessStatusCode();
            var json1 = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(json1);

            var response3 = await client.GetAsync(baseAdress + "/Likes/GetAllLikes");
            response3.EnsureSuccessStatusCode();
            var json3 = await response3.Content.ReadAsStringAsync();
            var likes = JsonConvert.DeserializeObject<List<LikeGetAllViewModel>>(json3);

            var response2 = await client.GetAsync(baseAdress + "/Posts/GetAllOneUserPosts/" + UserId);
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostGetViewModel>>(json2);
            var SortedPosts = posts.OrderByDescending(post => post.PostId).ToList();

            var response5 = await client.GetAsync(baseAdress + "/FriendShips/GetAllAcceptedFollowerUsers/" + UserId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var acceptedFollowerUsers = JsonConvert.DeserializeObject <List<AcceptedFollowerUsersGetViewModel>>(json5);
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel()
            {
                Token = token,
                AuthUserInfo= authUserInfo,
                UserInfo =userInfo,
                LikeList=likes,
                Posts=SortedPosts,
                AcceptedFollowerUsers=acceptedFollowerUsers
            };
            return View(userProfileViewModel);
        }

        [HttpGet("/UserProfile/About/{UserId}")]
        public async Task<IActionResult> About(string UserId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/AccountSettings/GetAuthUserInfo/" + UserId);
            response.EnsureSuccessStatusCode();
            var json1 = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(json1);

            var response2 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserAbout/" + UserId);
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var userAbout = JsonConvert.DeserializeObject<UserAboutGetViewModel>(json2);

            var response3 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserWork/" + UserId);
            response3.EnsureSuccessStatusCode();
            var json3 = await response3.Content.ReadAsStringAsync();
            var userWork = JsonConvert.DeserializeObject<UserWorkGetViewModel>(json3);

            var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllUserInterests/" + UserId);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var userInterests = JsonConvert.DeserializeObject<List<UserInterestGetViewModel>>(json4);

            var response5 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserEducation/" + UserId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var userEducation = JsonConvert.DeserializeObject<UserEducationGetViewModel>(json5);
            AboutViewModel authUserProfileViewModel = new AboutViewModel()
            {
                Token = token,
                UserInfo = userInfo,
                UserAbout = userAbout,
                UserWork = userWork,
                UserInterests = userInterests,
                UserEducation = userEducation,
            };
            return View(authUserProfileViewModel);
        }

        [HttpGet("/UserProfile/Album/{UserId}")]
        public async Task<IActionResult> Album(string UserId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(baseAdress + "/AccountSettings/GetAuthUserInfo/" + UserId);
            response.EnsureSuccessStatusCode();
            var json1 = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(json1);

            var response2 = await client.GetAsync(baseAdress + "/Posts/GetAllOneUserPosts/" + UserId);
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostGetViewModel>>(json2);
            var SortedPosts = posts.OrderByDescending(post => post.PostId).ToList();
            AlbumViewModel albumViewModel = new AlbumViewModel()
            {
                Token = token,
                UserInfo = userInfo,
                Posts = posts,
            };
            return View(albumViewModel);
        }
    }
}
