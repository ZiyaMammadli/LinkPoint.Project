using Microsoft.AspNetCore.Mvc;

namespace LinkPoint.MVC.Controllers
{
    public class NewsFeedController : Controller
    {
        public async Task <IActionResult> Index()
        {
            return View();
        }
    }
}
