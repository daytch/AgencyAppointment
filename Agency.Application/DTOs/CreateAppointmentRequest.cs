namespace Agency.Application.DTOs
{
    public class CreateAppointmentRequest
    {
        public string CustomerName { get; set; } = default!;
        public string CustomerEmail { get; set; } = default!;
        public DateTime Date { get; set; }
        public int AgencyId { get; set; }
    }
}
