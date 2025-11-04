namespace Agency.Domain.Entities
{
    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int MaxAppointmentsPerDay { get; set; } = 10;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<OffDay> OffDays { get; set; } = new List<OffDay>();
    }
}
