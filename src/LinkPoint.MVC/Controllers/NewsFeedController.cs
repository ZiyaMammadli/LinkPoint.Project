﻿    using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
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

        [HttpGet]
        public async Task<IActionResult> Friends()
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

            var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllDontFollowingUsers/" + userId + "/" + 4);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var dontFollowingUsers = JsonConvert.DeserializeObject<List<DontFollowingUsersViewModel>>(json4);

            var response5 = await client.GetAsync(baseAdress + "/FriendShips/GetAllMyFriends/" + userId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var myFriends = JsonConvert.DeserializeObject<List<MyFriendsGetViewModel>>(json5);
            FriendsViewModel friendsViewModel = new FriendsViewModel()
            {
                Token = token,
                UserInfo=userInfo,
                MyFriends = myFriends,
                DontFollowingUsers=dontFollowingUsers
            };
            return View(friendsViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Images()
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

            var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllDontFollowingUsers/" + userId + "/" + 4);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var dontFollowingUsers = JsonConvert.DeserializeObject<List<DontFollowingUsersViewModel>>(json4);

            var response2 = await client.GetAsync(baseAdress + "/Posts/GetAllPostsForImage");
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostGetViewModel>>(json2);
            var onlyImagePosts = posts.OrderByDescending(post => post.PostId).ToList();

            var response3 = await client.GetAsync(baseAdress + "/Likes/GetAllLikes");
            response3.EnsureSuccessStatusCode();
            var json3 = await response3.Content.ReadAsStringAsync();
            var likes = JsonConvert.DeserializeObject<List<LikeGetAllViewModel>>(json3);

            var response5 = await client.GetAsync(baseAdress + "/FriendShips/GetAllAcceptedFollowingUsers/" + userId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var acceptedFollowingUsers = JsonConvert.DeserializeObject<List<AcceptedFollowingUsersGetViewModel>>(json5);
            ImagesViewModel imagesViewModel = new ImagesViewModel()
            {
                Token=token,
                LikeList=likes,
                UserInfo=userInfo,
                OnlyImagePosts=onlyImagePosts,
                DontFollowingUsers=dontFollowingUsers,
                AcceptedFollowingUsers=acceptedFollowingUsers
            };
            return View(imagesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Videos()
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

            var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllDontFollowingUsers/" + userId + "/" + 4);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var dontFollowingUsers = JsonConvert.DeserializeObject<List<DontFollowingUsersViewModel>>(json4);

            var response2 = await client.GetAsync(baseAdress + "/Posts/GetAllPostsForVideo");
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<PostGetViewModel>>(json2);
            var onlyVideoPosts = posts.OrderByDescending(post => post.PostId).ToList();

            var response3 = await client.GetAsync(baseAdress + "/Likes/GetAllLikes");
            response3.EnsureSuccessStatusCode();
            var json3 = await response3.Content.ReadAsStringAsync();
            var likes = JsonConvert.DeserializeObject<List<LikeGetAllViewModel>>(json3);

            var response5 = await client.GetAsync(baseAdress + "/FriendShips/GetAllAcceptedFollowingUsers/" + userId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var acceptedFollowingUsers = JsonConvert.DeserializeObject<List<AcceptedFollowingUsersGetViewModel>>(json5);
            VideosViewModel videosViewModel = new VideosViewModel()
            {
                Token = token,
                LikeList = likes,
                UserInfo = userInfo,
                OnlyVideoPosts = onlyVideoPosts,
                DontFollowingUsers = dontFollowingUsers,
                AcceptedFollowingUsers = acceptedFollowingUsers
            };
            return View(videosViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Messages()
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

            var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllDontFollowingUsers/" + userId + "/" + 4);
            response4.EnsureSuccessStatusCode();
            var json4 = await response4.Content.ReadAsStringAsync();
            var dontFollowingUsers = JsonConvert.DeserializeObject<List<DontFollowingUsersViewModel>>(json4);

            var response5 = await client.GetAsync(baseAdress + "/FriendShips/GetAllAcceptedFollowingUsers/" + userId);
            response5.EnsureSuccessStatusCode();
            var json5 = await response5.Content.ReadAsStringAsync();
            var acceptedFollowingUsers = JsonConvert.DeserializeObject<List<AcceptedFollowingUsersGetViewModel>>(json5);
            
            var response6 = await client.GetAsync(baseAdress + "/Conversations/GetAllConversations/" + userId);
            response6.EnsureSuccessStatusCode();
            var json6 = await response6.Content.ReadAsStringAsync();
            var allConversations = JsonConvert.DeserializeObject<List<ConversationGetViewModel>>(json6);
            MessagesViewModel messages = new MessagesViewModel()
            {
                Token = token,
                UserInfo=userInfo,
                DontFollowingUsers=dontFollowingUsers,
                AcceptedFollowingUsers=acceptedFollowingUsers,
                AllConversations=allConversations
            };
            return View(messages);
        }
        [Route("NewsFeed/StatusCode")]
        public IActionResult StatusCode(int code)
        {
            if (code == 404)
            {
                return View("404");
            }

            return StatusCode(code);
        }   
    }
}

