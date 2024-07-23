namespace FinPlus.Presentation.Models
{
    using FinPlus.Domain.Users.Admin;

    public class OrganisationModel
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public Admin? MainAdmin { get; set; }
    }
}
