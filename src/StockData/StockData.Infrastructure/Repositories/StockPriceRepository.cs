using FirstDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, Guid>, IStockPriceRepository
    {
        public StockPriceRepository(ITrainingDbContext context) : base((DbContext)context)
        {
        }
    }
}
