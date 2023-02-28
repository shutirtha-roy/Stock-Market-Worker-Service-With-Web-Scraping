using AutoMapper;
using StockPriceEO = StockData.Infrastructure.Entities.StockPrice;
using StockPriceBO = StockData.Infrastructure.BusinessObjects.StockPrice;
using CompanyEO = StockData.Infrastructure.Entities.Company;
using CompanyBO = StockData.Infrastructure.BusinessObjects.Company;

namespace StockData.Infrastructure.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<StockPriceEO, StockPriceBO>()
                .ReverseMap();

            CreateMap<CompanyEO, CompanyBO>()
                .ReverseMap();
        }
    }
}
