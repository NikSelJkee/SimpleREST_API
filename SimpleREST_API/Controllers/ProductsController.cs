using Microsoft.AspNetCore.Mvc;
using SimpleREST_API.Entities;
using SimpleREST_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleREST_API.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(int companyId)
        {
            var products = _repository.GetProducts(companyId);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        public ActionResult<Product> GetProduct(int productId)
        {
            if (!_repository.ProductExists(productId))
                return NotFound();

            var product = _repository.GetProduct(productId);

            return Ok(product);
        }
    }
}
