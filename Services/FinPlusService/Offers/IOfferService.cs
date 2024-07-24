namespace FinPlusService
{
    using FinPlus.Domain.Offers;

    public interface IOfferService
    {
        public Task<List<Offer>> GetAllOffers();

        public Task<Offer> GetOfferById(string id);

        public Task AddOffer(Offer offer);

        public Task DeleteOffer(string id);
    }
}