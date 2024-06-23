using LinkPoint.MVC.Areas.Admin.ViewModels;
using LinkPoint.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LinkPoint.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ContactController : Controller
{
    Uri baseAdress = new Uri("https://localhost:7255/api");
    private readonly IHttpClientFactory _httpClientFactory;

    public ContactController(IHttpClientFactory httpClientFactory)
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

        var response2 = await client.GetAsync(baseAdress + "/ContactMessages/GetAllContactMessage");
        if (response2.IsSuccessStatusCode)
        {
            var json2 = await response2.Content.ReadAsStringAsync();
            var contacts = JsonConvert.DeserializeObject<List<ContactMessageGetViewModel>>(json2);
            var SortedContacts = contacts.OrderByDescending(post => post.Id).ToList();
            return View(SortedContacts);
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
    public async Task<IActionResult> Detail(int Id)
    {
        var token = HttpContext.Session.GetString("JWToken");
        var userId = HttpContext.Request.Cookies["UserId"];
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Auth");
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response2 = await client.GetAsync(baseAdress + "/ContactMessages/GetContactMessageById/" + Id);
        response2.EnsureSuccessStatusCode();
        var json2 = await response2.Content.ReadAsStringAsync();
        var contact = JsonConvert.DeserializeObject<ContactMessageGetViewModel>(json2);
        return View(contact);
    }
    [HttpGet]
    public async Task<IActionResult> AcceptContact(int ContactMessageId)
    {
        var token = HttpContext.Session.GetString("JWToken");
        var userId = HttpContext.Request.Cookies["UserId"];
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Auth");
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response2 = await client.PutAsync(baseAdress + "/ContactMessages/AcceptContactMessage/"+ ContactMessageId,null);
        if (response2.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Contact");
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
    public async Task<IActionResult> RejectContact(int ContactMessageId)
    {
        var token = HttpContext.Session.GetString("JWToken");
        var userId = HttpContext.Request.Cookies["UserId"];
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Auth");
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response2 = await client.PutAsync(baseAdress + "/ContactMessages/RejectContactMessage/" + ContactMessageId,null);
        if (response2.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Contact");
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
