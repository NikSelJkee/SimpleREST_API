using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleREST_API.Dtos;
using SimpleREST_API.Entities;
using SimpleREST_API.Helpers;
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
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts(int companyId,
            [FromQuery]ProductResourceParameters productResourceParameters)
        {
            var products = _repository.GetProducts(companyId, productResourceParameters);

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{productId}", Name = "GetProduct")]
        public ActionResult<ProductDto> GetProduct(int companyId, int productId)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();
            if (!_repository.ProductExists(productId))
                return NotFound();

            var product = _repository.GetProduct(companyId, productId);

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public ActionResult<ProductDto> CreateProductForCompany(int companyId, 
            [FromBody]ProductForCreatingDto product)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();

            var productEntity = _mapper.Map<Product>(product);

            _repository.AddProduct(companyId, productEntity);
            _repository.Save();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute("GetProduct", 
                new { companyId = companyId, productId = productToReturn.Id }, productToReturn);
        }

        [HttpPut("{productId}")]
        public ActionResult<ProductDto> UpdateProductForCompany(int companyId,
            int productId, [FromBody]ProductForUpdatingDto product)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();
            if (!_repository.ProductExists(productId))
                return NotFound();

            var productEntity = _repository.GetProduct(companyId, productId);

            if (productId != productEntity.Id)
                return BadRequest();

            _mapper.Map(product, productEntity);

            _repository.UpdateProduct(productEntity);
            _repository.Save();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute("GetProduct", 
                new { companyId = companyId, productId = productToReturn.Id }, productToReturn);
        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProductForCompany(int companyId, int productId)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();
            if (!_repository.ProductExists(productId))
                return NotFound();

            var product = _repository.GetProduct(companyId, productId);

            _repository.DeleteProduct(product);
            _repository.Save();

            return Ok();
        }
    }
}
