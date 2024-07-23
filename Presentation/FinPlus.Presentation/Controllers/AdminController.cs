namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Admin;
    using FinPlus.Domain.Users.Trafer;
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
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;
            var admin = await _adminService.GetAllAdmins();
            return View(admin);
        }

        public async Task<IActionResult> AddAdmin(Admin admin, string name)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            if (name == null)
            {
                return View(admin);
            }

            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                return BadRequest("Введите полное ФИО");
            }

            var fio = new FIO
            {
                Surname = parts[0],
                Name = parts[1],
                Patronymic = parts.Length > 2 ? parts[2] : string.Empty,
            };

            admin.Name = fio;
            var orgId = HttpContext.Session.GetString("OrganisationId");
            admin.OrganisationId = orgId;
            await _adminService.AddAdmin(admin);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAdmin(string id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            var admin = await _adminService.GetAdminById(id);
            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdmin(Admin admin, string name)
        {
            if (ModelState.IsValid)
            {
                var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 2)
                {
                    return BadRequest("Введите полное ФИО");
                }

                var fio = new FIO
                {
                    Surname = parts[0],
                    Name = parts[1],
                    Patronymic = parts.Length > 2 ? parts[2] : string.Empty,
                };
                admin.Name = fio;
                await _adminService.UpdateAdmin(admin);
                return RedirectToAction("Index");
            }

            return View("EditTraffer", admin);
        }

        public async Task<IActionResult> SearchAdmin(string partName)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            var admins = await _adminService.GetAllAdminsByPartName(partName);
            return View("Index", admins);
        }
    }
}
