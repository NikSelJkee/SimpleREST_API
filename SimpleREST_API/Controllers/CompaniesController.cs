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
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public CompaniesController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Company>> GetCompanies()
        {
            var companies = _repository.GetCompanies();

            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        public ActionResult<Company> GetCompany(int companyId)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();

            var company = _repository.GetCompany(companyId);

            return Ok(company);
        }
    }
}
