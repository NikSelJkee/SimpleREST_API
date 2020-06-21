using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleREST_API.Dtos
{
    public class ProductForUpdatingDto
    {
        [Required(ErrorMessage = "Title needs to be filled in")]
        [MaxLength(100, ErrorMessage = "Title needs to be up to 100 characters")]
        public string Title { get; set; }
        [MaxLength(300, ErrorMessage = "Description needs to be up to 300 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price needs to be filled in")]
        public decimal Price { get; set; }
        public int CompanyId { get; set; }
    }
}
