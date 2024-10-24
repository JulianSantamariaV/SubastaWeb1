using Microsoft.AspNetCore.Mvc;

namespace SubastaWeb.Controllers
{
    public class SubastaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
