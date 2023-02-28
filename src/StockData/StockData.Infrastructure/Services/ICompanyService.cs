using StockData.Infrastructure.BusinessObjects;

namespace StockData.Infrastructure.Services
{
    public interface ICompanyService
    {
        Task CreateCompany(Company company);
        Task CreateAllCompanies();
    }
}
