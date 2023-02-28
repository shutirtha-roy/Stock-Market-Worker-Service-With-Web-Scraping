using AutoMapper;
using StockData.Infrastructure.Entities;
using StockData.Infrastructure.UnitOfWorks;
using CompanyBO = StockData.Infrastructure.BusinessObjects.Company;
using CompanyEO = StockData.Infrastructure.Entities.Company;

namespace StockData.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IHtmlFetchService _htmlFetchService;
        private readonly IMapper _mapper;

        public CompanyService(IApplicationUnitOfWork applicationUnitOfWork, IHtmlFetchService fetchService, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _htmlFetchService = fetchService;
            _mapper = mapper;
        }

        private async Task<string[]> ConvertAllStockDataToCompanies(List<string[]> allCompanyStockPrice)
        {
            string[] companies = new string[allCompanyStockPrice.Count];

            for(int i = 0; i < allCompanyStockPrice.Count; i++)
            {
                companies[i] = allCompanyStockPrice[i][0];
            }

            return companies;
        }

        public async Task CreateAllCompanies()
        {
            var allCompanyStockPrice = await _htmlFetchService.GetAllStockInformation();
            var companies = await ConvertAllStockDataToCompanies(allCompanyStockPrice);

            foreach(var companyTradeCode in companies)
            {
                CompanyBO company = new CompanyBO();
                company.TradeCode = companyTradeCode;
                await CreateCompany(company);
            }
        }

        public async Task CreateCompany(CompanyBO company)
        {
            var count = _applicationUnitOfWork.Companies.GetCount(x => x.TradeCode == company.TradeCode);

            if(count == 0)
            {
                CompanyEO companyEntity = _mapper.Map<CompanyEO>(company);

                _applicationUnitOfWork.Companies.Add(companyEntity);
                _applicationUnitOfWork.Save();
            }
        }
    }
}
