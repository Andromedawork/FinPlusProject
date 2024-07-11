namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Presentation.Models;
    using FinPlusService.User.Auth;
    using Microsoft.AspNetCore.Mvc;

    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            AuthModel model = new AuthModel();
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
                return RedirectToAction("Index", "Home");
            }

            model.Error = "Неверный логин или пароль";

            return View(model);
        }
    }
}
