using StockData.Infrastructure.BusinessObjects;

namespace StockData.Infrastructure.Services
{
    public interface IStockPriceService
    {
        Task CreateStockPrice(StockPrice stockPrice);
        Task CreateAllStockPrice();
    }
}
