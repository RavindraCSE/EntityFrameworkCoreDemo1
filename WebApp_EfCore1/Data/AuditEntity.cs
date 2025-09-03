namespace WebApp_EfCore1.Data
{
    public class AuditEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ?LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
