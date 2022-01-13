using MediatR;
using TddSample.Api.Application.Repository;
using TddSample.Domain;

namespace TddSample.Api.Application.Query
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotsQuery, IEnumerable<Slot>>
    {
        private readonly ISlotRepository _repository;

        public GetSlotQueryHandler(ISlotRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Slot>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = await _repository.GetAllAsync();

            return await Task.FromResult(slots);
        }
    }
}
