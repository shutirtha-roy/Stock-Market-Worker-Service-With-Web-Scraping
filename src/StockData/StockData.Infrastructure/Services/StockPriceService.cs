using AutoMapper;
using StockData.Infrastructure.UnitOfWorks;
using StockPriceBO = StockData.Infrastructure.BusinessObjects.StockPrice;
using StockPriceEO = StockData.Infrastructure.Entities.StockPrice;

namespace StockData.Infrastructure.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IHtmlFetchService _htmlFetchService;
        private readonly IMapper _mapper;

        public StockPriceService(IApplicationUnitOfWork applicationUnitOfWork, IHtmlFetchService htmlFetchService, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _htmlFetchService = htmlFetchService;
            _mapper = mapper;
        }
        public async Task CreateAllStockPrice()
        {
            var allCompanyStockPrice = await _htmlFetchService.GetAllStockInformation();
            var presentTime = DateTime.Now;

            foreach (var companyStockPrice in allCompanyStockPrice)
            {
                StockPriceBO stockPrice = new StockPriceBO();
                stockPrice.CompanyId = await _applicationUnitOfWork.Companies.GetIdFromName(companyStockPrice[0]);
                stockPrice.LastTradingPrice = await stockPrice.ConvertStockValueToDouble(companyStockPrice[1]);
                stockPrice.High = await stockPrice.ConvertStockValueToDouble(companyStockPrice[2]);
                stockPrice.Low = await stockPrice.ConvertStockValueToDouble(companyStockPrice[3]);
                stockPrice.ClosePrice = await stockPrice.ConvertStockValueToDouble(companyStockPrice[4]);
                stockPrice.YesterdayClosePrice = await stockPrice.ConvertStockValueToDouble(companyStockPrice[5]);
                stockPrice.Change = await stockPrice.ConvertStockValueToDouble(companyStockPrice[6]);
                stockPrice.Trade = await stockPrice.ConvertStockValueToDouble(companyStockPrice[7]);
                stockPrice.Value = await stockPrice.ConvertStockValueToDouble(companyStockPrice[8]);
                stockPrice.Volume = await stockPrice.ConvertStockValueToDouble(companyStockPrice[9]);
                stockPrice.StockDateTime = presentTime;

                await CreateStockPrice(stockPrice);
            }
        }

        public async Task CreateStockPrice(StockPriceBO stockPrice)
        {
            StockPriceEO stockEntity = _mapper.Map<StockPriceEO>(stockPrice);

            _applicationUnitOfWork.StockPrices.Add(stockEntity);
            _applicationUnitOfWork.Save();
        }
    }
}
