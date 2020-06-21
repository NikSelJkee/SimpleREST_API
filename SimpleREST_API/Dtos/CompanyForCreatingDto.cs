using SimpleREST_API.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleREST_API.Dtos
{
    public class CompanyForCreatingDto
    {
        [Required(ErrorMessage = "Name needs to be filled in")]
        [MaxLength(50, ErrorMessage = "Name needs to be up to 50 characters")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
