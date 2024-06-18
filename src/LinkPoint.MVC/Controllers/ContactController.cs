using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LinkPoint.MVC.Controllers
{
    public class ContactController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7255/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            ContactViewModel contactViewModel = new ContactViewModel()
            {
                UserId = userId
            };
            return View(contactViewModel);
        }
        [HttpPost]
        public async Task< IActionResult> Index(ContactMessagePostViewModel contactMessagePostViewModel)
        {
            var userId = HttpContext.Request.Cookies["UserId"];
            ContactViewModel contactViewModel = new ContactViewModel()
            {
                UserId = userId,
                ContactMessagePostViewModel = contactMessagePostViewModel
            };
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(baseAdress + "/ContactMessages/CreateContactMessage", contactMessagePostViewModel);
                if (response.IsSuccessStatusCode)
                {
                    
                    string successMessage = "Succesfully send";
                    ViewData["success"] = successMessage;
                    return View(contactViewModel);
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, apiError.message);
                }
            }
            return View(contactViewModel);
        }
    }
}
