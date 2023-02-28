using FirstDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company, Guid>, ICompanyRepository
    {
        private readonly ITrainingDbContext _context;

        public CompanyRepository(ITrainingDbContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<Guid> GetIdFromName(string name)
        {
            Guid id = _context.Companies.Where(company => company.TradeCode == name).Select(company => company.Id).FirstOrDefault();
            return id;
        }
    }
}
