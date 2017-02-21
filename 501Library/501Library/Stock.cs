using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary
{
    public class Stock
    {
        /// <summary>
        /// price of stock in previous period
        /// </summary>
        private double previousPrice;

        /// <summary>
        /// The ticker, immutable
        /// </summary>
        private string ticker;

        /// <summary>
        /// Name of the company, immutable
        /// </summary>
        private string name;

        /// <summary>
        /// The stockprice
        /// </summary>
        private double stockPrice;

        /// <summary>
        /// Displays the ticker
        /// </summary>
        public string Ticker
        {
            get
            {
                return ticker;
            }
        }

        /// <summary>
        /// Gets or sets the previous price (period)
        /// </summary>
        public double PreviousPrice
        {
            get
            {
                return previousPrice;
            }
            set
            {
                previousPrice = value;
            }
        }

        /// <summary>
        /// Gets and sets the stockprice for this stock object
        /// </summary>
        public double StockPrice
        {
            get
            {
                return stockPrice;
            }
            set
            {
                stockPrice = value;
            }
        }

        /// <summary>
        /// Gets the name of the stock
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

        }


        /// <summary>
        /// Constructor for instantiating this object
        /// </summary>
        /// <param name="tick">The ticker</param>
        /// <param name="price">The price of the stock</param>
        /// <param name="name">The name of the company associated with the stock</param>
        public Stock(string tick, double price, string name)
        {
            ticker = tick;
            stockPrice = price;
            this.name = name;
            previousPrice = price;
        }

        public override string ToString()
        {
            string gainlosspercent = "";
            string gainlossvalue = "";
            double gl = (double)Math.Round(gainLossPercent(), 2);
            double glv = (double)Math.Round(gainLossValue(), 2);
            if (gl > 0)
            {
                gainlosspercent = "+" + gl;
                gainlossvalue = ", up $" + glv;
            }
            else
            {
                gainlosspercent = gl.ToString();
                if (glv == 0) gainlossvalue = "";
                else gainlossvalue = ", down $" + Math.Abs(glv);
            }
            return (ticker + " - " + name + ", $" + stockPrice + gainlossvalue + " (" + gainlosspercent + "%)");
        }

        /// <summary>
        /// Returns the period gains as a percent
        /// </summary>
        /// <returns></returns>
        private double gainLossPercent()
        {
            return (stockPrice - previousPrice) / previousPrice * 100f;
        }

        private double gainLossValue()
        {
            return (stockPrice - previousPrice);
        }
    }
}
