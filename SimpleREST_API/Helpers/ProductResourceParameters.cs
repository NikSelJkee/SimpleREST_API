using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleREST_API.Helpers
{
    public class ProductResourceParameters
    {
        public string SearchQuery { get; set; }
        public decimal MoreThen { get; set; }
        public decimal LessThen { get; set; }
    }
}
