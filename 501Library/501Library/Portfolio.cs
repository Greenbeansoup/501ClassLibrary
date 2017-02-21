using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary
{
    public class Portfolio
    {

        /// <summary>
        /// A List of all transactions to occur on the account
        /// </summary>
        List<Transaction> _transactions = new List<Transaction>();

        /// <summary>
        /// Stocks contained in this portfolio
        /// </summary>
        private Dictionary<Stock, int> stocks;

        /// <summary>
        /// Gets the list of stocks contained in this portfolio
        /// </summary>
        public Dictionary<Stock, int> Stocks
        {
            get
            {
                return stocks;
            }
        }

        /// <summary>
        /// The name of this portfolio
        /// </summary>
        private string name;

        /// <summary>
        /// Gets the name of this portfolio
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Value at the beginning of the current period
        /// </summary>
        private double periodStartValue;

        /// <summary>
        /// Returns price at the beginning of the current period
        /// </summary>
        public double PeriodStartValue
        {
            get
            {
                return periodStartValue;
            }
            set
            {
                periodStartValue = value;
            }
        }

 /*       /// <summary>
        /// Gets the cash balance of this portfolio as a double and sets the cash balance
        /// </summary>
        public double CashBalance
        {
            get
            {
                return cashBalance;
            }
            set
            {
                cashBalance = value;
            }
        }*/

        /// <summary>
        /// Gets the positions balance as a double
        /// </summary>
        public double PositionsBalance
        {
            get
            {
               double returnvalue = 0;
               foreach (KeyValuePair<Stock, int> k in stocks)
                {
                    returnvalue += (k.Key.StockPrice * k.Value);
                }
                return (double)Math.Round(returnvalue, 2);
            }
        }

        /// <summary>
        /// The value of this portfolio if everything was back to the buying price
        /// </summary>
        private double originPrices;

        public double OriginPrices
        {
            get
            {
                return originPrices;
            }
            set
            {
                originPrices = value;
            }
        }


        /// <summary>
        /// Returns the value of the portfolio as if the stocks had never changed value
        /// </summary>
        /*public double OriginalValue
        {
            get
            {
                double value = 0;
                foreach(StockQuantity s in stocks)
                {
                    value += s.OriginalPrice;
                }
                return value;
            }
        }*/
        /// <summary>
        /// Returns the value gain/loss of this portfolio (including purchases)
        /// </summary>
        public double GainLossValue
        {
            get
            {
                /*double returnvalue = 0;
                foreach (StockQuantity sq in stocks)
                {
                    returnvalue += (sq.GainLossValue * sq.Quantity);
                }
                return returnvalue;*/
                return (double)Math.Round((PositionsBalance - periodStartValue), 2);
            }
        }

        /// <summary>
        /// Returns the value gain/loss of this portfolio (excluding the value of trades)
        /// </summary>
        public double NPGainLossValue
        {
            get
            {
                return PositionsBalance - originPrices;
            }
        }

        /// <summary>
        /// Gets number of stocks in portfolio
        /// </summary>
        public int Count
        {
            get
            {
                return stocks.Count;
            }
        }

        /// <summary>
        /// Finds the gain/loss as a percent for the entire portfolio
        /// </summary>
        public double GainLossPercent
        {
            get
            {
                /*double num = 0;
                int totalUnits = 0;
                foreach (StockQuantity sq in stocks)
                {
                    num += sq.PeriodStartPrice * sq.Quantity;
                    totalUnits += sq.Quantity;
                }
                return (GainLossValue) / totalUnits *10;*/
                if (PeriodStartValue != 0)
                {
                    return (PositionsBalance - PeriodStartValue) / PeriodStartValue * 100;
                }
                else
                {
                    return PositionsBalance;
                }
            }
        }

        /// <summary>
        /// Constructor that only sets the name of the portfolio
        /// </summary>
        /// <param name="name">The name of the portfolio</param>
        public Portfolio(string name)
        {
            this.name = name;
            periodStartValue = 0;
            originPrices = 0;
            stocks = new Dictionary<Stock, int>();
        }

       
        /// <summary>
        /// Adds a stock to the list of stocks contained
        /// </summary>
        /// <param name="stock">The stock to add</param>
        /// <param name="quantity">The number of stocks to buy</param>
        /// <returns>The value of the purchase</returns>
        public void Add(Stock desiredStock, int numberToBuy)
        {
            if (stocks.ContainsKey(desiredStock))
            {
                int newTotalStocks = stocks[desiredStock] + numberToBuy;
                stocks.Remove(desiredStock);
                stocks.Add(desiredStock, newTotalStocks);

            }
            else
            {
                stocks.Add(desiredStock, numberToBuy);
            }
            _transactions.Add(new Transaction(Transaction.Event.SOLD_STOCK, this, desiredStock, desiredStock.StockPrice, numberToBuy));
        }


        /// <summary>
        /// Removes a stock from the portfolio based on the ticker value
        /// </summary>
        /// <param name="ticker">String identifier</param>
        /// <returns>The value of the sale in dollars</returns>
        public double sell(Stock desiredStock, int numberToSell)
        {
            double gain = 0;
            if (stocks.ContainsKey(desiredStock))
            {
                if (stocks[desiredStock] >= numberToSell)
                {
                    int newTotalStocks = stocks[desiredStock] - numberToSell;
                    stocks.Remove(desiredStock);
                    if (newTotalStocks != 0)
                    {
                        stocks.Add(desiredStock, newTotalStocks);
                    }
                    int stockLeftToSell = numberToSell;
                    foreach (Transaction t in _transactions)
                    {
                        if (t.Action == Transaction.Event.PURCHASED_STOCK && t.Stock == desiredStock)
                        {
                            if (t.SharesPurchased >= stockLeftToSell)
                            {
                                gain += (stockLeftToSell * desiredStock.StockPrice) - (stockLeftToSell * t.PriceAtTime);
                                stockLeftToSell -= t.SharesPurchased;
                                break;
                            }
                            else if (t.SharesPurchased < stockLeftToSell)
                            {
                                gain += (t.SharesPurchased * desiredStock.StockPrice) - (t.SharesPurchased * t.PriceAtTime);
                                stockLeftToSell -= t.SharesPurchased;
                            }
                        }
                    }
                    if (stockLeftToSell > 0)
                    {
                        throw new ArgumentException();
                    }
                    _transactions.Add(new Transaction(Transaction.Event.SOLD_STOCK, this, desiredStock, desiredStock.StockPrice, numberToSell, gain));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            return gain;
        }

       
    }
}
