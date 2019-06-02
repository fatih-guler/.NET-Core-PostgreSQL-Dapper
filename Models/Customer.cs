using System.ComponentModel.DataAnnotations;

namespace NetCore_Dapper.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
         public string Phone { get; set; }
         [Required]
         public string Address { get; set; }
    }
}