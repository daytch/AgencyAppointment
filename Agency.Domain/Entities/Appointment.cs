namespace Agency.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TokenNumber { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public string CustomerEmail { get; set; } = default!;

        public Agency Agency { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
