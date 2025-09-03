using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp_EfCore1.Data
{
    public class Customer: AuditEntity
    {
        public int Id { get; set; }
        [Column(TypeName ="varchar(200)")]
        public string CustomerName { get; set; }
        [Column(TypeName="varchar(100)")]
        public string CustomerEmail { get; set; }
        [Column(TypeName ="varchar(500)")]
        public string Address { get; set; }
        [Column(TypeName ="varchar(300)")]
        public string City { get; set; }

    }
}
