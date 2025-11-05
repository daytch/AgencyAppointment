namespace Agency.Domain.Entities
{
    public class CustomerAppointment
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime AppointmentDate { get; set; } // simpan dalam UTC
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
