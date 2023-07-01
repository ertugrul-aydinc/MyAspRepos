using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _10___ServiceContracts;
using _10___StockAppQuestion.Models;
using _10___StockAppQuestion.OptionsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _10___StockAppQuestion.Controllers
{
    public class TradesController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;

        public TradesController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _configuration = configuration;
        }

        // GET: /<controller>/
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if(string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";

            Dictionary<string, object>? companies = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
            var quotes = await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

            if (companies is not null && quotes is not null)
            {
                stockTrade = new StockTrade()
                {
                    StockSymbol = Convert.ToString(companies["ticker"]),
                    StockName = Convert.ToString(companies["name"]),
                    Price = Convert.ToDouble(quotes["c"].ToString())
                };
            }

            ViewBag.finnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }
    }
}

