using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary
{
    public class StockQuantity
    {
        /// <summary>
        /// The stock that is tied to this object
        /// </summary>
        private Stock stock;

        /// <summary>
        /// The amount of this stock
        /// </summary>
        private int quantity;

        /// <summary>
        /// Price at time of purchase
        /// </summary>
        private double periodStartPrice;

        /// <summary>
        /// Price of this stock when purchased
        /// </summary>
        private double priceAtPurchase;

        /// <summary>
        /// Gets the price at purchase
        /// </summary>
        public double PriceAtPurchase
        {
            get
            {
                return priceAtPurchase;
            }
        }

        /// <summary>
        /// Gets the stock
        /// </summary>
        public Stock Stock
        {
            get
            {
                return stock;
            }
        }

        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        /// <summary>
        /// Returns or sets the price at the start of the period
        /// </summary>
        public double PeriodStartPrice
        {
            get
            {
                return periodStartPrice;
            }
            set
            {
                periodStartPrice = value;
            }
        }

        /// <summary>
        /// Gets the dollar value of the gain/loss over all
        /// </summary>
        public double GainLossValue
        {
            get
            {
                return stock.StockPrice - periodStartPrice;
            }
        }

        /// <summary>
        /// Gets the gain/loss as a percent
        /// </summary>
        public double GainLossPercent
        {
            get
            {
                return GainLossValue / stock.StockPrice * 100;
            }
        }

        /// <summary>
        /// Constructor for the StockQuantity class
        /// </summary>
        /// <param name="s">The stock</param>
        /// <param name="q">The quantity</param>
        public StockQuantity(Stock s, int q)
        {
            stock = s;
            quantity = q;
            priceAtPurchase = s.StockPrice;
            periodStartPrice = s.StockPrice;
        }

        public override string ToString()
        {
            string updown = "";
            if (GainLossValue > 0)
            {
                updown = "+";
            }
            return (stock.Ticker + " " + stock.Name + " | Current Price: " + stock.StockPrice + " | Gain/Loss: " + GainLossValue + "  (" + updown + Math.Abs(GainLossPercent)+ ")\nQuantity Owned: " + quantity);
        }
    }
}
