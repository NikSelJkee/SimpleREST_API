using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleREST_API.Dtos;
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
        private readonly IMapper _mapper;
        public CompaniesController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDto>> GetCompanies()
        {
            var companies = _repository.GetCompanies();

            return Ok(_mapper.Map<IEnumerable<CompanyDto>>(companies));
        }

        [HttpGet("{companyId}", Name = "GetCompany")]
        public ActionResult<CompanyDto> GetCompany(int companyId)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();

            var company = _repository.GetCompany(companyId);

            return Ok(_mapper.Map<CompanyDto>(company));
        }

        [HttpPost]
        public ActionResult<CompanyDto> CreateCompany([FromBody]
            CompanyForCreatingDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repository.AddCompany(companyEntity);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return CreatedAtRoute("GetCompany", 
                new { companyId = companyToReturn.Id }, companyToReturn);
        }
    }
}
