using MediatR;
using TddSample.Api.Application.Repository;
using TddSample.Api.Application.Services;
using TddSample.Domain;

namespace TddSample.Api.Application.Query
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotsQuery, IEnumerable<Slot>>
    {
        private readonly ISlotRepository _repository;
        private readonly IDateTimeService _dateTimeService;

        public GetSlotQueryHandler(ISlotRepository repository, IDateTimeService dateTimeService)
        {
            _repository = repository;
            _dateTimeService = dateTimeService;
        }
        public async Task<IEnumerable<Slot>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = await _repository.GetAllAsync();

            var result = new List<Slot>();

            foreach (var slot in slots)
            {
                if(slot.From.Hours > _dateTimeService.Current().Hour)
                {
                    result.Add(slot);
                }                
            }

            return result;
        }
    }
}
