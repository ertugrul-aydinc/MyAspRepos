using System;
using System.Text.Json;
using _8___HttpClientExample.ServiceContracts;

namespace _8___HttpClientExample.Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetStockQuoteValues()
        {
            using(HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={_configuration["TradingOptions:DefaultStockSymbol"]}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream responseMessage = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(responseMessage);

                string response = streamReader.ReadToEnd();

                Dictionary<string,object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                return responseDictionary;
            }
        }
    }
}

