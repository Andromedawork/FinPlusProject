namespace FinPlus.Presentation.Models
{
    using FinPlus.Domain.Offers;

    public class OfferModel
    {
       public string? Id { get; set; }

       public OfferType? Type { get; set; }

       public string? Name { get; set; }

       public decimal Profit { get; set; }

       public decimal TargetAction { get; set; }

       public string? OrganisationId { get; set; }

       public OfferStatus Status { get; set; }
    }
}
