namespace HelpDesk.Domain
{
    public class BaseDto
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public int CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
