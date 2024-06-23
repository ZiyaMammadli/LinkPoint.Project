using LinkPoint.MVC.Areas.Admin.ViewModels;
using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LinkPoint.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7255/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public PostController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response2 = await client.GetAsync(baseAdress + "/Admin/GetAllPosts");
            if (response2.IsSuccessStatusCode)
            {
                var json2 = await response2.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<GetPostViewModel>>(json2);
                var SortedPosts = posts.OrderByDescending(post => post.PostId).ToList();
                return View(SortedPosts);
            }
            else if (response2.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ModelState.AddModelError(string.Empty, "You are not allowed to enter here.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int postId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response2 = await client.GetAsync(baseAdress + "/Admin/GetByIdPost/" + postId);
            response2.EnsureSuccessStatusCode();
            var json2 = await response2.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<GetPostViewModel>(json2);
            return View(post);
        }
        [HttpGet("[action]/{postId}")]
        public async Task<IActionResult> SoftDelete(int postId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response2 = await client.GetAsync(baseAdress + "/Admin/SoftDeletePost/" + postId);
            if (response2.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Post");
            }
            else if (response2.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ModelState.AddModelError(string.Empty, "You are not allowed to enter here.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View("Error");

        }
        [HttpGet("[action]/{postId}")]
        public async Task<IActionResult> Activate(int postId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response2 = await client.GetAsync(baseAdress + "/Admin/ActivatePost/" + postId);
            if (response2.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Post");
            }
            else if (response2.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ModelState.AddModelError(string.Empty, "You are not allowed to enter here.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View("Error");
        }
        [HttpGet("[action]/{postId}")]
        public async Task<IActionResult> Delete(int postId)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response2 = await client.GetAsync(baseAdress + "/Admin/Delete/" + postId);
            if (response2.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Post");
            }
            else if (response2.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ModelState.AddModelError(string.Empty, "You are not allowed to enter here.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View("Error");

        }
    }
}
