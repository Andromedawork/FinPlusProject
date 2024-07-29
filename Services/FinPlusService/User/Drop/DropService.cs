namespace FinPlusService
{
    using FinPlus.Domain.Offers;
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Drop;
    using FinPlusService.Sources;

    public class DropService : IDropService
    {
        private readonly IDropRepository _dropRepository;

        private readonly ITraferService _traferService;

        private readonly ISourceService _sourceService;

        public DropService(IDropRepository dropRepository, ITraferService traferService, ISourceService sourceService)
        {
            _dropRepository = dropRepository;
            _traferService = traferService;
            _sourceService = sourceService;
        }

        public async Task<List<Drop>> GetAllDrops()
        {
            return await _dropRepository.GetAllDrops();
        }

        public async Task<Drop> GetDropById(string id)
        {
            return await _dropRepository.GetDropById(id);
        }

        public async Task AddDrop(Drop drop)
        {
            await _dropRepository.AddDrop(drop);
        }

        public async Task<List<Drop>> GetAllDropsByPartName(string partName)
        {
            return await _dropRepository.GetAllDropsByPartName(partName);
        }

        public async Task DeleteDrop(string id)
        {
            await _dropRepository.DeleteDrop(id);
        }

        public async Task<string> GetPartner(string id)
        {
            var partner = await _traferService.GetTrafferById(id);

            if (partner == null)
            {
                return "-";
            }

            return partner.Name.ToString();
        }

        public async Task<string> GetSubPartner(string id)
        {
            var partner = await _traferService.GetTrafferById(id);

            if (partner == null)
            {
                return "-";
            }

            return partner.Name.ToString();
        }

        public async Task<string> GetSource(string id)
        {
            var source = await _sourceService.GetSourceById(id);

            if (source == null)
            {
                return "-";
            }

            return source.Name;
        }

        public async Task<bool> UpdateDropData(string id, FIO name, string referalId, DropPassport pass, string mobileNumber, string cardNumber, string personalReferalId, DateTime dateOfBirth, string telegram)
        {
            return await _dropRepository.UpdateDropData(id, name, referalId, pass, mobileNumber, cardNumber, personalReferalId, dateOfBirth, telegram);
        }

        public async Task<bool> UpdateDropStep(string id, int step, List<Offer> offers, string comments)
        {
            Dictionary<string, DropStep> steps = new Dictionary<string, DropStep>()
            {
                {
                    DateTime.Now.ToString(), (DropStep)step
                },
            };

            DropStep dStep = (DropStep)step;

            Dictionary<string, List<Offer>> offer = new Dictionary<string, List<Offer>>()
            {
                {
                    dStep.ToString(), offers
                },
            };
            return await _dropRepository.UpdateDropStep(id, steps, offer, comments);
        }

        public Task<bool> UpdateDropOffer(string dropId, string offerId, DateTime date, OfferStatus status)
        {
            // OfferStatus newStatus = (OfferStatus)status;
            return _dropRepository.UpdateOfferStatus(dropId, offerId, date.ToString(), status);
        }
    }
}
