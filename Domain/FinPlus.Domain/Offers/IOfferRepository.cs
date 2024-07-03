namespace FinPlus.Domain.Offers
{
    public interface IOfferRepository
    {
        public Task<List<Offer>> GetAllOffers();

        public Task AddOffer(Offer offer);
    }
}
