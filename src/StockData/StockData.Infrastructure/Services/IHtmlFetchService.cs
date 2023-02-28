using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure.Services
{
    public interface IHtmlFetchService
    {
        Task<List<string[]>> GetAllStockInformation();
    }
}
