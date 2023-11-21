using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
