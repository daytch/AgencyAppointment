namespace Agency.Domain.Entities
{
    public class CustomerAppointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; } = null!;
        public DateTime AppointmentDate { get; set; } // simpan dalam UTC
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
