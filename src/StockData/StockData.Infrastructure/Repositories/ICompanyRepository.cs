using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.Repositories
{
    public interface ICompanyRepository : IRepository<Company, Guid>
    {
        Task<Guid> GetIdFromName(string name);
    }
}
