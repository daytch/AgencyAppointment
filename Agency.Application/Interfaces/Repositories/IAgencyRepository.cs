using Agency.Domain.Entities;

namespace Agency.Application.Interfaces.Repositories
{
    public interface IAgencyRepository
    {
        Task<Domain.Entities.Agency> GetByIdAsync(int id);
        Task<IEnumerable<Domain.Entities.Agency>> GetAllAsync();
        Task<Domain.Entities.Agency> AddAsync(Domain.Entities.Agency agency);
        Task UpdateAsync(Domain.Entities.Agency agency);
        Task DeleteAsync(int id);
        //Task<IEnumerable<Appointment>> GetByAgencyIdAsync(int agencyId);
    }
}
