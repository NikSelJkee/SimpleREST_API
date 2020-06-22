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
        public ActionResult<IEnumerable<CompanyDto>> GetCompanies([FromQuery]
            CompanyResourceParameters companyResourceParameters)
        {
            var companies = _repository.GetCompanies(companyResourceParameters);

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

        [HttpPut("{companyId}")]
        public ActionResult<CompanyDto> UpdateCompany(int companyId, [FromBody]
            CompanyForUpdatingDto company)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();

            var companyEntity = _repository.GetCompany(companyId);

            if (companyId != companyEntity.Id)
                return BadRequest();

            _mapper.Map(company, companyEntity);

            _repository.UpdateCompany(companyEntity);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return CreatedAtRoute("GetCompany", 
                new { companyId = companyToReturn.Id }, companyToReturn);
        }

        [HttpDelete("{companyId}")]
        public ActionResult DeleteCompany(int companyId)
        {
            if (!_repository.CompanyExists(companyId))
                return NotFound();

            var company = _repository.GetCompany(companyId);

            _repository.DeleteCompany(company);
            _repository.Save();

            return Ok();
        }

        [HttpOptions]
        public ActionResult CompanyOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE,OPTIONS");
            return Ok();
        }
    }
}
