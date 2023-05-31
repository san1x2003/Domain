using System.ComponentModel.DataAnnotations;

namespace SanshopDomain1.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public Guid PostId { get; set; }
        public Post? Post { get; set; }

    }
}
