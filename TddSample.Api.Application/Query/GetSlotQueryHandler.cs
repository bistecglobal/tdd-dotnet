using MediatR;
using TddSample.Api.Application.Repository;
using TddSample.Api.Application.Services;
using TddSample.Domain;

namespace TddSample.Api.Application.Query
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotsQuery, IEnumerable<Slot>>
    {
        private readonly ISlotRepository slotRepository;
        private readonly IDateTimeService dateTimeService;

        public GetSlotQueryHandler(ISlotRepository slotRepository, IDateTimeService dateTimeService)
        {
            this.slotRepository = slotRepository;
            this.dateTimeService = dateTimeService;
        }

        public async Task<IEnumerable<Slot>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = await slotRepository.GetAllAsync();

            var result = new List<Slot>();

            foreach (var slot in slots)
            {
                if (slot.From.Hours > dateTimeService.Current().Hour)
                {
                    result.Add(slot);
                }
            }

            return await Task.FromResult(result);
        }
    }
}
