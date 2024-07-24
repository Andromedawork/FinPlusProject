namespace FinPlusService
{
    using FinPlus.Domain.Offers;

    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task AddOffer(Offer offer)
        {
            await _offerRepository.AddOffer(offer);
        }

        public async Task DeleteOffer(string id)
        {
            await _offerRepository.DeleteOffer(id);
        }

        public async Task<List<Offer>> GetAllOffers()
        {
            return await _offerRepository.GetAllOffers();
        }

        public async Task<Offer> GetOfferById(string id)
        {
            return await _offerRepository.GetOfferById(id);
        }
    }
}