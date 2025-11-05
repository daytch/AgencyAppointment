using Agency.Application.DTOs;
using Agency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.Interfaces.Services
{
    public interface IOffDayService
    {
        Task<IEnumerable<OffDay>> GetByAgencyIdAsync(int agencyId);
        Task<OffDay> CreateOffDayAsync(CreateOffDayRequest request);
    }
}
