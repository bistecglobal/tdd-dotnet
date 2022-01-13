using MediatR;
using TddSample.Domain;

namespace TddSample.Api.Application.Query
{
    public class GetSlotsQuery: IRequest<IEnumerable<Slot>>
    {
    }
}
