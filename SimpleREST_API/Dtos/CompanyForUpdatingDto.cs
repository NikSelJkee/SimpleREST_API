using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleREST_API.Dtos
{
    public class CompanyForUpdatingDto
    {
        [Required(ErrorMessage = "Name needs to be filled in")]
        [MaxLength(50, ErrorMessage = "Name needs to be up to 50 characters")]
        public string Name { get; set; }
    }
}
