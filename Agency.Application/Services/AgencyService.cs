using Agency.Application.Interfaces.Repositories;
using Agency.Application.Interfaces.Services;
using Agency.Domain.Entities;

namespace Agency.Application.Services
{
    public class AgencyService : IAgencyService
    {
        private readonly IAgencyRepository _agencyRepository;

        public AgencyService(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        public async Task<IEnumerable<Agency.Domain.Entities.Agency>> GetAllAsync()
            => await _agencyRepository.GetAllAsync();

        public async Task<Agency.Domain.Entities.Agency> GetByIdAsync(int id)
            => await _agencyRepository.GetByIdAsync(id);

        public async Task<Agency.Domain.Entities.Agency> CreateAsync(Agency.Domain.Entities.Agency agency)
        {
            await _agencyRepository.AddAsync(agency);
            return agency;
        }

        public async Task UpdateAsync(Agency.Domain.Entities.Agency agency)
            => await _agencyRepository.UpdateAsync(agency);

        public async Task DeleteAsync(int id)
            => await _agencyRepository.DeleteAsync(id);
    }
}
