namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.Organisations;
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Admin;
    using FinPlus.Presentation.Models;
    using FinPlusService;
    using FinPlusService.Organisation;
    using Microsoft.AspNetCore.Mvc;

    public class OrganisationController : Controller
    {
        private readonly IOrganisationService _organisationService;
        private readonly IAdminService _adminService;

        public OrganisationController(IOrganisationService organisationService, IAdminService adminService)
        {
            _organisationService = organisationService;
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            var organisations = await _organisationService.GetAllOrganisations();
            List<OrganisationModel> model = new List<OrganisationModel>();
            foreach (var organisation in organisations)
            {
                OrganisationModel organisationModel = new OrganisationModel()
                {
                    Name = organisation.Name,
                    Id = organisation.Id,
                    MainAdmin = await _adminService.GetAdminById(organisation.MainAdminId),
                };
                model.Add(organisationModel);
            }

            return View(model);
        }

        public async Task<IActionResult> AddOrganisation(OrganisationModel model, string fio)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            if (string.IsNullOrEmpty(model.Name))
            {
                return View(model);
            }

            Organisation organisation = new Organisation()
            {
                Name = model.Name,
                Id = model.Id,
            };

            await _organisationService.AddOrganisation(organisation);
            organisation = await _organisationService.GetOrganisationByName(model.Name);

            var parts = fio.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                return BadRequest("Введите полное ФИО");
            }

            var adminName = new FIO
            {
                Surname = parts[0],
                Name = parts[1],
                Patronymic = parts.Length > 2 ? parts[2] : string.Empty,
            };

            model.MainAdmin.OrganisationId = organisation.Id;
            model.MainAdmin.Name = adminName;

            await _adminService.AddAdmin(model.MainAdmin);
            var admin = await _adminService.GetAdminByOrganisationId(organisation.Id);

            organisation.MainAdminId = admin.Id;
            await _organisationService.UpdateOrganisation(organisation);
            return RedirectToAction("Index");
        }
    }
}
