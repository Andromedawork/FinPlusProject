namespace FinPlus.Domain.Offers
{
    public static class OfferTypeFactory
    {
        public static Offer CreateOffer(OfferType type)
        {
            return type switch
            {
                OfferType.Debet => new DebetOffer(),
                OfferType.Credit => new CreditOffer(),
                OfferType.SCS => new SCSOffer(),
                _ => throw new ArgumentException("Invalid offer type", nameof(type)),
            };
        }
    }
}
