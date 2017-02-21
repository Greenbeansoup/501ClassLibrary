using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary
{
    public static class MarketSimulator
    {
        public static void Tick(MarketVolatility mv, List<Stock> stocks)//Runs one iteration of the market simulator
        {
            Random rand = new Random();
            Random operation = new Random();
            int min = 1;
            int max = 4;
            if (mv == MarketVolatility.High)
            {
                min = 3;
                max = 15;
            }
            else if (mv == MarketVolatility.Medium)
            {
                min = 2;
                max = 8;
            }
            foreach (Stock s in stocks)
            {
                int num = rand.Next(min, max);
                int op = operation.Next(0, 2);//Determines if the stock is going up or down.
                if (op == 0)
                {
                    num = -num;
                }
                s.PreviousPrice = s.StockPrice;
                s.StockPrice = (float)Math.Round(s.StockPrice * (num / 100.0f + 1.0f), 2);
            }
        }
    }
}
