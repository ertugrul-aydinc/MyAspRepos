using System;
namespace _8___HttpClientExample.ServiceContracts
{
	public interface IFinnhubService
	{
		Task<Dictionary<string, object>?> GetStockQuoteValues();
	}
}

