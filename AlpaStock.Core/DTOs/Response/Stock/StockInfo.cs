using System;
using Newtonsoft.Json;

public class StockInfo
{
    public string Symbol { get; set; }
    public double Price { get; set; }
    public long MarketCap { get; set; }
    public double Beta { get; set; }
    public double LastDividend { get; set; }
    public string Range { get; set; }
    public double Change { get; set; }
    public double ChangePercentage { get; set; }
    public long Volume { get; set; }
    public long AverageVolume { get; set; }
    public string CompanyName { get; set; }
    public string Currency { get; set; }
    public string Cik { get; set; }
    public string Isin { get; set; }
    public string Cusip { get; set; }
    public string ExchangeFullName { get; set; }
    public string Exchange { get; set; }
    public string Industry { get; set; }
    public string Website { get; set; }
    public string Description { get; set; }
    public string Ceo { get; set; }
    public string Sector { get; set; }
    public string Country { get; set; }
    public string FullTimeEmployees { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Image { get; set; }
    public DateTime IpoDate { get; set; }
    public bool DefaultImage { get; set; }
    public bool IsEtf { get; set; }
    public bool IsActivelyTrading { get; set; }
    public bool IsAdr { get; set; }
    public bool IsFund { get; set; }
}
