using TddSample.Domain;

namespace TddSample.Api.Application.Repository
{
    public interface ISlotRepository
    {
        Task<ICollection<Slot>> GetAllAsync();
    }
}
