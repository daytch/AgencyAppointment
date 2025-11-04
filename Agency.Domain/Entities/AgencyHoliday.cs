namespace Agency.Domain.Entities
{
    public class AgencyHoliday
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public Agency Agency { get; set; } = null!;
    }
}
