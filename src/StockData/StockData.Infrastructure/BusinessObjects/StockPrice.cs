namespace StockData.Infrastructure.BusinessObjects
{
    public class StockPrice
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public double LastTradingPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double ClosePrice { get; set; }
        public double YesterdayClosePrice { get; set; }
        public double Change { get; set; }
        public double Trade { get; set; }
        public double Value { get; set; }
        public double Volume { get; set; }
        public DateTime StockDateTime { get; set; }

        public async Task<double> ConvertStockValueToDouble(string number)
        {
            double convertedNumber;

            try
            {
                convertedNumber = Convert.ToDouble(number);
            }
            catch (Exception e)
            {
                convertedNumber = 0;
            }

            return convertedNumber;
        }
    }
}
