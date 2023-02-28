using StockData.Infrastructure.Services;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICompanyService _companyService;
        private readonly IStockPriceService _stockPriceService;

        public Worker(ILogger<Worker> logger, ICompanyService companyService, IStockPriceService stockPriceService)
        {
            _logger = logger;
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _companyService.CreateAllCompanies();
                await _stockPriceService.CreateAllStockPrice();

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}