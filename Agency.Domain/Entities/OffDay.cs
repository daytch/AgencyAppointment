namespace Agency.Domain.Entities
{
    public class OffDay
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; } = default!;
        public Agency Agency { get; set; } = default!;
    }
}
