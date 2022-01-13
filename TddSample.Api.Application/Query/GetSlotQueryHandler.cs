using MediatR;
using TddSample.Domain;

namespace TddSample.Api.Application.Query
{
    public class GetSlotQueryHandler : IRequestHandler<GetSlotsQuery, IEnumerable<Slot>>
    {
        
        public async Task<IEnumerable<Slot>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
