

namespace AlpaStock.Core.DTOs.Response.Stock
{
    public class StockResp
    {

        public string symbol { get; set; }

      
        public string name { get; set; }

 
        public double price { get; set; }

      
        public double changePercentage { get; set; }

   
        public double change { get; set; }


        public long volume { get; set; }

        public double dayLow { get; set; }

  
        public double dayHigh { get; set; }

        public double yearHigh { get; set; }
     
        public double yearLow { get; set; }
        public long marketCap { get; set; }

        public double priceAvg50 { get; set; }

        public double priceAvg200 { get; set; }

    
        public string exchange { get; set; }


        public double open { get; set; }

        public double previousClose { get; set; }

    
        public long timestamp { get; set; }
    }
}
