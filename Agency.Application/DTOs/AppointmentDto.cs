namespace Agency.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
