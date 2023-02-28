using HtmlAgilityPack;
using StockData.Infrastructure.UnitOfWorks;

namespace StockData.Infrastructure.Services
{
    public class HtmlFetchService : IHtmlFetchService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public HtmlFetchService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        private async Task<HtmlDocument> LoadStockInformation()
        {
            var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            return htmlDoc;
        }

        private async Task<string> AddValidCharacters(HtmlNode item)
        {
            string newHtml = "";

            foreach (var charValue in item.InnerHtml)
            {
                if (charValue == 13 || charValue == 10 || charValue == 9)
                {
                    continue;
                }

                newHtml += charValue;
            }

            return newHtml;
        }

        private async Task<bool> StockMarkOpenStatus()
        {
            var htmlDoc = await LoadStockInformation();
            var node = htmlDoc.DocumentNode.SelectSingleNode("//span[@class='green']");

            if(node.InnerText == "Open")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<string[]>> GetAllStockInformation()
        {
            if(!await StockMarkOpenStatus())
            {
                var emptyList = new List<string[]>();
                return emptyList;
            }
                
            var htmlDoc = await LoadStockInformation();
            var node = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered background-white shares-table fixedHeader']");
            var newHtmlDoc = new HtmlDocument();

            List<string[]> stringHtmls = new List<string[]>();

            foreach (var item in node.ChildNodes)
            {
                string newHtml = await AddValidCharacters(item);

                if (newHtml.Contains("td"))
                {
                    
                    newHtmlDoc.LoadHtml(newHtml);
                    var htmlBody = newHtmlDoc.DocumentNode.SelectNodes("//td");
                    string newString = "";

                    foreach (var singleTd in htmlBody)
                    {
                        newString += singleTd.InnerText + " ";
                    }

                    string[] allColumns = newString.TrimEnd().Split(" ").Skip(1).ToArray();

                    stringHtmls.Add(allColumns);
                }
            }

            return stringHtmls;
        }
    }
}
