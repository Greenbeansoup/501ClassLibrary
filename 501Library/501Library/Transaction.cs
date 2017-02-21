/* Author: Austin Lee Gray
 * File Name: Transaction.cs
 * 
 * This class implements a Transaction object used to keep track of all actions taken on an account or portfolio
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace StockLibrary
{
    public class Transaction
    {
        
        /// <summary>
        /// The DateTime info for when the action happened
        /// </summary>
        private DateTime _dateTime;

        /// <summary>
        /// The Event Enum. Gives details of what the action taken was
        /// </summary>
        private Event _event;

        /// <summary>
        /// The amount of money involved in the action
        /// </summary>
        private double _amount;

        /// <summary>
        /// The name of a given portfolio
        /// </summary>
        private string _name;

        /// <summary>
        /// the gain/loss of the transaction
        /// </summary>
        private double _gain;

        /// <summary>
        /// The literal portfolio used in the transaction
        /// </summary>
        private Portfolio _portfolio;

        /// <summary>
        /// The Stock involved in the transaction
        /// </summary>
        private Stock _stock;

        /// <summary>
        /// The price of the stock at the time of the transaction
        /// </summary>
        private double _priceAtTime;

        /// <summary>
        /// The number of shares purchased during the transaction
        /// </summary>
        private int _sharesPurchased;

        /// <summary>
        /// The number of shares sold during the transaction
        /// </summary>
        private int _shareSold;

        /// <summary>
        /// This Enum gives insight into what type of Event/Action took place during the transaction each of the Enumerations has a set of properties assisated with it
        /// </summary>
        public enum Event { ADD_FUNDS_TO_ACCOUNT, WITHDRAW_FUNDS_FROM_ACCOUNT, NEW_PORTFOLIO, DELETE_PORTFOLIO, SOLD_STOCK, PURCHASED_STOCK, ADD_FUNDS_TO_PORTFOLIO, WITHDRAW_FUNDS_FROM_PORTFOLIO }

        /// <summary>
        /// Returns the stock involved with the transaction
        /// </summary>
        public Stock Stock
        {
            get
            {
                return _stock;
            }
        }

        /// <summary>
        /// Returns the action involved with the transaction
        /// </summary>
        public Event Action
        {
            get
            {
                return _event;
            }
        }

        /// <summary>
        /// Returns the price at the time of the transaction
        /// </summary>
        public double PriceAtTime
        {
            get
            {
                return _priceAtTime;
            }
        }

        /// <summary>
        /// Reruens the number of shares purchased in the transaction
        /// </summary>
        public int SharesPurchased
        {
            get
            {
                return _sharesPurchased;
            }
        }

       /*There is multiple constructors for the Transaction class
        * Each constructor is used for a seperate Event with the exception of
        * the adding and transfering funds events.
        * This allows a single class to represent all the information needed.
        */



        public Transaction(Event action, double amount)
        {
            _event = action;
            _dateTime = DateTime.UtcNow;
            _amount = amount;
        }

        public Transaction(Event action, string name)
        {
            _event = action;
            _dateTime = DateTime.UtcNow;
            _name = name;
        }

        public Transaction(Event action, string name, double gain)
        {
            _event = action;
            _dateTime = DateTime.UtcNow;
            _name = name;
            _gain = gain;
        }

        public Transaction(Event action, Portfolio portfolio, Stock stock, double priceAtTime, int sharesPurchased)
        {
            _event = action;
            _dateTime = DateTime.UtcNow;
            _portfolio = portfolio;
            _stock = stock;
            _priceAtTime = priceAtTime;
            _sharesPurchased = sharesPurchased;
        }

        public Transaction(Event action, Portfolio portfolio, Stock stock, double priceAtTime, int shareSold, double gain)
        {
            _event = action;
            _dateTime = DateTime.UtcNow;
            _portfolio = portfolio;
            _stock = stock;
            _priceAtTime = priceAtTime;
            _shareSold = shareSold;
            _gain = gain;
        }

        public Transaction(Event action, Portfolio portfolio, double amount)
        {
            _event = action;
            _dateTime = DateTime.UtcNow;
            _amount = amount;
        }

        

        /// <summary>
        /// This ToString overrides the default.
        /// This method returns a string based on what type of Event is assosiated with the instance of the class
        /// And is mostly used only for log displaying 
        /// </summary>
        /// <returns>String based on the Event</returns>
        public override string ToString()
        {
            switch (_event)
            {
                case Event.ADD_FUNDS_TO_ACCOUNT:
                    return "Added " + _amount.ToString("C", CultureInfo.CurrentCulture) + " to account at " + _dateTime.ToLocalTime();

                case Event.ADD_FUNDS_TO_PORTFOLIO:
                    return "Added " + _amount.ToString("C", CultureInfo.CurrentCulture) + " to portfolio " + _portfolio + "at time " + _dateTime.ToLocalTime();
                    

                case Event.DELETE_PORTFOLIO:
                    return "Deleted portfolio " + _name + " at " + _dateTime.ToLocalTime() + "it had a net gain of " + _gain.ToString("C", CultureInfo.CurrentCulture);

                case Event.NEW_PORTFOLIO:
                    return "Created portfolio " + _name + " at " + _dateTime.ToLocalTime();

                case Event.PURCHASED_STOCK:
                    return "Bought " + _sharesPurchased + " shares of " + _stock.Name + "at a price of " + _priceAtTime.ToString("C", CultureInfo.CurrentCulture) + " for each share at " + _dateTime.ToLocalTime();

                case Event.SOLD_STOCK:
                    return "Sold " + _sharesPurchased + " shares of " + _stock.Name + "at a price of " + _priceAtTime.ToString("C", CultureInfo.CurrentCulture) + " for each share making a gain of " + _gain.ToString("C", CultureInfo.CurrentCulture) + " at " + _dateTime.ToLocalTime();
                    
                case Event.WITHDRAW_FUNDS_FROM_ACCOUNT:
                    return "Withdrew " + _amount.ToString("C", CultureInfo.CurrentCulture) + "from account at " + _dateTime.ToLocalTime();

                case Event.WITHDRAW_FUNDS_FROM_PORTFOLIO:
                    return "Withdrew " + _amount.ToString("C", CultureInfo.CurrentCulture) + "from portfolio " + _portfolio + "at time " + _dateTime.ToLocalTime();

                default:
                    throw new ArgumentException();
            }
        }

    }
}
