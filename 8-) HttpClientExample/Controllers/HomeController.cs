using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8___HttpClientExample.Models;
using _8___HttpClientExample.OptionsModels;
using _8___HttpClientExample.ServiceContracts;
using _8___HttpClientExample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _8___HttpClientExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFinnhubService _finnhubSerive;
        //private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly TradingOptions _tradingOptions;

        public HomeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubSerive = finnhubService;
            //_tradingOptions = tradingOptions;
            _tradingOptions = tradingOptions.Value;
        }

        // GET: /<controller>/
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object>? responseDictionary = await _finnhubSerive.GetStockQuoteValues();

            if (responseDictionary is null)
                throw new InvalidOperationException("Request returned null");
            if (_tradingOptions.DefaultStockSymbol is null)
                _tradingOptions.DefaultStockSymbol = "AAPL";

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
                Change = Convert.ToDouble(responseDictionary["d"].ToString()),
                PercentChange = Convert.ToDouble(responseDictionary["dp"].ToString()),
                HighPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString()),
                ClosePrice = Convert.ToDouble(responseDictionary["pc"].ToString())
            };

            return View(stock);
        }
    }
}

