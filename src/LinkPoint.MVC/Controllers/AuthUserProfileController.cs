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
    [HttpGet]
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

        var response3 = await client.GetAsync(baseAdress + "/Likes/GetAllLikes");
        response3.EnsureSuccessStatusCode();
        var json3 = await response3.Content.ReadAsStringAsync();
        var likes = JsonConvert.DeserializeObject<List<LikeGetAllViewModel>>(json3);

        var response2 = await client.GetAsync(baseAdress + "/Posts/GetAllOneUserPosts/" + userId);
        response2.EnsureSuccessStatusCode();
        var json2 = await response2.Content.ReadAsStringAsync();
        var posts = JsonConvert.DeserializeObject<List<PostGetViewModel>>(json2);
        var SortedPosts = posts.OrderByDescending(post => post.PostId).ToList();
        AuthUserProfileViewModel authUserProfileViewModel = new AuthUserProfileViewModel()
        {
            Token = token,
            UserInfo = userInfo,
            Posts = SortedPosts,
            LikeList = likes,

        };
        return View(authUserProfileViewModel);
    }
    [HttpGet]
    public async Task<IActionResult> About()
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

        var response2 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserAbout/" + userId);
        response2.EnsureSuccessStatusCode();
        var json2 = await response2.Content.ReadAsStringAsync();
        var userAbout = JsonConvert.DeserializeObject<UserAboutGetViewModel>(json2);

        var response3 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserWork/" + userId);
        response3.EnsureSuccessStatusCode();
        var json3 = await response3.Content.ReadAsStringAsync();
        var userWork = JsonConvert.DeserializeObject<UserWorkGetViewModel>(json3);

        var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllUserInterests/" + userId);
        response4.EnsureSuccessStatusCode();
        var json4 = await response4.Content.ReadAsStringAsync();
        var userInterests = JsonConvert.DeserializeObject<List<UserInterestGetViewModel>>(json4);

        var response5 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserEducation/" + userId);
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
    [HttpGet]
    public async Task<IActionResult> Album()
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

        var response2 = await client.GetAsync(baseAdress + "/Posts/GetAllOneUserPosts/" + userId);
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

    [HttpGet]
    public async Task<IActionResult> BasicInfo()
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

        var response2 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserAbout/" + userId);
        response2.EnsureSuccessStatusCode();
        var json2 = await response2.Content.ReadAsStringAsync();
        var userAbout = JsonConvert.DeserializeObject<UserAboutGetViewModel>(json2);
        BasicInfoViewModel basicInfoViewModel = new BasicInfoViewModel()
        {
            Token = token,
            UserInfo = userInfo,
            UserAbout=userAbout,
        };
        return View(basicInfoViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> EduAndWork()
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

        var response3 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserWork/" + userId);
        response3.EnsureSuccessStatusCode();
        var json3 = await response3.Content.ReadAsStringAsync();
        var userWork = JsonConvert.DeserializeObject<UserWorkGetViewModel>(json3);

        var response5 = await client.GetAsync(baseAdress + "/AccountSettings/GetUserEducation/" + userId);
        response5.EnsureSuccessStatusCode();
        var json5 = await response5.Content.ReadAsStringAsync();
        var userEducation = JsonConvert.DeserializeObject<UserEducationGetViewModel>(json5);
        EduAndWorkViewModel eduAndWorkViewModel = new EduAndWorkViewModel()
        {
            Token = token,
            UserInfo=userInfo,
            UserEducation=userEducation,
            UserWork=userWork,
        };
        return View(eduAndWorkViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Interests()
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

        var response4 = await client.GetAsync(baseAdress + "/AccountSettings/GetAllUserInterests/" + userId);
        response4.EnsureSuccessStatusCode();
        var json4 = await response4.Content.ReadAsStringAsync();
        var userInterests = JsonConvert.DeserializeObject<List<UserInterestGetViewModel>>(json4);
        InterestsViewModel interestsViewModel = new InterestsViewModel()
        {
            Token = token,
            UserInfo=userInfo,
            UserInterests=userInterests,
        };
        return View(interestsViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
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
        ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel()
        {
            Token=token,
            UserInfo=userInfo
        };
        return View(changePasswordViewModel);
    }
}