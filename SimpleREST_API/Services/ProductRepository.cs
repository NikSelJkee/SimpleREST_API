using SimpleREST_API.Data;
using SimpleREST_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleREST_API.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            _db.Companies.Add(company);
        }

        public void AddProduct(int companyId, Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            product.CompanyId = companyId;
            _db.Products.Add(product);
        }

        public bool CompanyExists(int companyId)
        {
            return _db.Companies.Any(c => c.Id == companyId);
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            _db.Companies.Remove(company);
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _db.Products.Remove(product);
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _db.Companies.ToList();
        }

        public Company GetCompany(int companyId)
        {
            return _db.Companies.FirstOrDefault(c => c.Id == companyId);
        }

        public Product GetProduct(int companyId, int productId)
        {
            return _db.Products.FirstOrDefault(p => p.CompanyId == companyId && p.Id == productId);
        }

        public IEnumerable<Product> GetProducts(int companyId)
        {
            return _db.Products.Where(p => p.CompanyId == companyId).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _db.Products.Any(p => p.Id == productId);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public void UpdateCompany(Company company)
        {            
        }

        public void UpdateProduct(Product product)
        {
        }
    }
}
