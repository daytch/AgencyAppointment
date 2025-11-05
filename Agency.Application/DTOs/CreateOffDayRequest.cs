using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Application.DTOs
{
    public class CreateOffDayRequest
    {
        public int AgencyId { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; } = default!;
    }
}
