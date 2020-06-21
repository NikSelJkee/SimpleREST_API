using SimpleREST_API.Data;
using SimpleREST_API.Entities;
using SimpleREST_API.Helpers;
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

        public IEnumerable<Company> GetCompanies(CompanyResourceParameters 
            companyResourceParameters)
        {
            if (companyResourceParameters == null)
                throw new ArgumentNullException(nameof(companyResourceParameters));

            if (string.IsNullOrWhiteSpace(companyResourceParameters.SearchQuery))
                return GetCompanies();

            var searchQuery = companyResourceParameters.SearchQuery.Trim();

            return _db.Companies.Where(c => c.Name.Contains(searchQuery)).ToList();
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

        public IEnumerable<Product> GetProducts(int companyId, ProductResourceParameters 
            productResourceParameters)
        {
            if (productResourceParameters == null)
                throw new ArgumentNullException(nameof(productResourceParameters));

            if (string.IsNullOrWhiteSpace(productResourceParameters.SearchQuery) &&
                productResourceParameters.LessThen <= 0 &&
                productResourceParameters.MoreThen <= 0)
                return GetProducts(companyId);

            var collection = _db.Products as IQueryable<Product>;

            if (!string.IsNullOrWhiteSpace(productResourceParameters.SearchQuery))
            {
                var searchQuery = productResourceParameters.SearchQuery.Trim();
                collection = collection.Where(p => p.Title.Contains(searchQuery));
            }

            if (productResourceParameters.LessThen > 0)
            {
                var lessThen = productResourceParameters.LessThen;
                collection = collection.Where(p => p.Price < lessThen);
            }

            if (productResourceParameters.MoreThen > 0)
            {
                var moreThen = productResourceParameters.MoreThen;
                collection = collection.Where(p => p.Price > moreThen);
            }

            collection = collection.Where(p => p.CompanyId == companyId);

            return collection.ToList();
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
