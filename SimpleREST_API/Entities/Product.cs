using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleREST_API.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title needs to be filled in")]
        [MaxLength(100, ErrorMessage = "Title needs to be up to 100 characters")]
        public string Title { get; set; }
        [MaxLength(300, ErrorMessage = "Description needs to be up to 300 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price needs to be filled in")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Price { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}
