using Microsoft.EntityFrameworkCore;
using StockData.Infrastructure.DbContexts;
using StockData.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICompanyRepository Companies { get; private set; }
        public IStockPriceRepository StockPrices { get; private set; }

        public ApplicationUnitOfWork(ITrainingDbContext dbContext,
            ICompanyRepository companyRepository, IStockPriceRepository stockRepository) : base((DbContext)dbContext)
        {
            Companies = companyRepository;
            StockPrices = stockRepository;
        }
    }
}
