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

        public async Task<List<Offer>> GetAllOffersById(List<string> id)
        {
            List<Offer> offers = new List<Offer>();
            if (id != null)
            {
                foreach (var offerId in id)
                {
                    Offer offer = await _offerRepository.GetOfferById(offerId);
                    offers.Add(offer);
                }
            }

            return offers;
        }

        public async Task<Offer> GetOfferById(string id)
        {
            return await _offerRepository.GetOfferById(id);
        }
    }
}