using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Street { get; set; }
        public virtual Cinema Cinema { get; set; }
    }
}
