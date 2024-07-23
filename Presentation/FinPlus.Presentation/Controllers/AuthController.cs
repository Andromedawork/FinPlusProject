namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Presentation.Models;
    using FinPlusService;
    using FinPlusService.User.Auth;
    using Microsoft.AspNetCore.Mvc;

    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;

        public AuthController(IAuthService authService, IAdminService adminService)
        {
            _authService = authService;
            _adminService = adminService;
        }

        public IActionResult Index(AuthModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Entrance(AuthModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
            {
                model.Error = "Логин и пароль обязательны для заполнения.";
                return View(model);
            }

            var role = await _authService.Authentication(model.Login, model.Password);
            if (role != null)
            {
                HttpContext.Session.SetString("Role", role);

                if (role == "Admin")
                {
                    var admin = await _adminService.GetAdminByLogin(model.Login);
                    HttpContext.Session.SetString("OrganisationId", admin.OrganisationId);
                }

                return RedirectToAction("Index", "Home");
            }

            model.Error = "Неверный логин или пароль";

            return RedirectToAction("Index", model);
        }
    }
}
