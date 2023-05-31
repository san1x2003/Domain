using System.ComponentModel.DataAnnotations;

namespace SanshopDomain1.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        public string Identificator { get; set; } = string.Empty;
        public int Poost { get; set; }
        public string TittlePost { get; set; } = string.Empty;

    }
}
