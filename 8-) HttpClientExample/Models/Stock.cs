﻿using System;
namespace _8___HttpClientExample.Models
{
	public class Stock
	{
		public string? StockSymbol { get; set; }
		public double? CurrentPrice { get; set; }
		public double? Change { get; set; }
		public double? PercentChange { get; set; }
		public double? HighPrice { get; set; }
		public double? LowPrice { get; set; }
		public double? OpenPrice { get; set; }
		public double? ClosePrice { get; set; }
	}
}

