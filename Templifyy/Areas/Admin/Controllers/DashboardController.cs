using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Templifyy.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Users()
    {
        return View();
    }

    public IActionResult Courses()
    {
        return View();
    }

    public IActionResult Categories()
    {
        return View();
    }

    public IActionResult Reports()
    {
        return View();
    }
}
