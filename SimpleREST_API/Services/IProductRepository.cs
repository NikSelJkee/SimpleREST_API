using SimpleREST_API.Entities;
using SimpleREST_API.Helpers;
using System.Collections.Generic;

namespace SimpleREST_API.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int companyId);
        IEnumerable<Product> GetProducts(int companyId, ProductResourceParameters 
            productResourceParameters);
        Product GetProduct(int companyId, int productId);
        void AddProduct(int companyId, Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        IEnumerable<Company> GetCompanies();
        IEnumerable<Company> GetCompanies(CompanyResourceParameters 
            companyResourceParameters);
        Company GetCompany(int companyId);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);

        bool ProductExists(int productId);
        bool CompanyExists(int companyId);

        bool Save();
    }
}
