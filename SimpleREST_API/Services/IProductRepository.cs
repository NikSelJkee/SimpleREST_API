using SimpleREST_API.Entities;
using System.Collections.Generic;

namespace SimpleREST_API.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int companyId);
        Product GetProduct(int companyId, int productId);
        void AddProduct(int companyId, Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

        IEnumerable<Company> GetCompanies();
        Company GetCompany(int companyId);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);

        bool ProductExists(int productId);
        bool CompanyExists(int companyId);

        bool Save();
    }
}
