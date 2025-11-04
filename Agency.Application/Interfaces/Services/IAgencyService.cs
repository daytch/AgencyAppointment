using Agency.Domain.Entities;

namespace Agency.Application.Interfaces.Services
{
    public interface IAgencyService
    {
        Task<IEnumerable<Agency.Domain.Entities.Agency>> GetAllAsync();
        Task<Agency.Domain.Entities.Agency> GetByIdAsync(int id);
        Task<Agency.Domain.Entities.Agency> CreateAsync(Agency.Domain.Entities.Agency agency);
        Task UpdateAsync(Agency.Domain.Entities.Agency agency);
        Task DeleteAsync(int id);
    }
}
