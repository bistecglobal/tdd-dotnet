using MediatR;
using TddSample.Domain;

namespace TddSample.Api.Application.Query
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotsQuery, IEnumerable<Slot>>
    {
        public async Task<IEnumerable<Slot>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = new List<Slot>
            {
                new Slot
                {
                    From = new TimeSpan(12, 0, 0)
                }
            };

            return await Task.FromResult(slots);
        }
    }
}
