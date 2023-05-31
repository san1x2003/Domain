using System.ComponentModel.DataAnnotations;

namespace SanshopDomain1.Models
{
    public class Zakaz
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ClientID { get; set; }
        public Client? Client { get; set; }

        public Guid? SkladId { get; set; }
        public Sklad? Sklad { get; set; }

        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public Guid? TovarId { get; set; }
        public Tovar? Tovar { get; set; }

        public string NumberContact { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Adress { get; set; } = string.Empty;


        public DateTime? OrderDate { get; set; }
    }
}
