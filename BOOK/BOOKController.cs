using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BOOK3.Controllers
{
    public class Book3Controller : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
