using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.Entities;

namespace StockData.Infrastructure.DbContexts
{
    public interface ITrainingDbContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<StockPrice> StockPrices { get; set; }
    }
}
