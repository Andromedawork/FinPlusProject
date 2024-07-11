namespace FinPlus.Presentation.Controllers
{
    using FinPlusService;
    using Microsoft.AspNetCore.Mvc;

    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var admin = await _adminService.GetAllAdmins();
            return View(admin);
        }
    }
}
