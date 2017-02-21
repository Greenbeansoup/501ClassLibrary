using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary
{
     public class Account
    {
        /// <summary>
        /// The flat rate fee charged for performing a trade
        /// </summary>
        private const double TRADEFEE = 9.99;

        /// <summary>
        /// Keeps track of the period
        /// </summary>
        private int period;

        /// <summary>
        /// Getter setter for period
        /// </summary>
        public int Period
        {
            set
            {
                period = value;
            }
            get
            {
                return period;
            }
        }

        /// <summary>
        /// Returns the flat fee charged for performing a trade
        /// </summary>
        public double TradeFee
        {
            get
            {
                return TRADEFEE;
            }
        }

        /// <summary>
        /// The flat rate fee charged for transferring money
        /// </summary>
        private const double TRANSFEE = 4.99;

        /// <summary>
        /// Returns the fee for performing a transfer of money (deposit/withdrawl)
        /// </summary>
        public double TransFee
        {
            get
            {
                return TRANSFEE;
            }
        }

        /// <summary>
        /// The cash balance that is a result of deposits. Will be used to calculate gain/loss of the account
        /// </summary>
        private double depositedCash;

        /// <summary>
        /// gets/sets the depositedCash
        /// </summary>
        public double DepositedCash
        {
            get
            {
                return depositedCash;
            }
            set
            {
                depositedCash = value;
            }
        }

        /// <summary>
        /// The portfolios held by this account
        /// </summary>
        private List<Portfolio> portfolios = new List<Portfolio>();

        /// <summary>
        /// Gets the list of portfolios
        /// </summary>
        public List<Portfolio> Portfolios
        {
            get
            {
                return portfolios;
            }
        }

        /// <summary>
        /// Gets the number of portfolios stored in this account. Currently limited to three by the program
        /// </summary>
        public int PortfolioCount
        {
            get
            {
                return portfolios.Count;
            }
        }

        /// <summary>
        /// Name of the account
        /// </summary>
        private string accountName;

        /// <summary>
        /// Gets the account name
        /// </summary>
        public string AccountName
        {
            get
            {
                return accountName;
            }
        }

        /// <summary>
        /// Cash in this account
        /// </summary>
        private double cashBalance;

        /// <summary>
        /// Gets and sets the cash balance
        /// </summary>
        public double CashBalance
        {
            get
            {
                return (double)Math.Round(cashBalance, 2);
            }
            set
            {
                cashBalance = value;
            }
        }

 /*       /// <summary>
        /// Position in dollar amounts of this account
        /// </summary>
        private double positionsBalance;

        /// <summary>
        /// Gets the positionsBalance
        /// </summary>
        public double PositionsBalance
        {
            get
            {
                return positionsBalance;
            }
        }*/

        /// <summary>
        /// Reference to all of the stocks
        /// </summary>
        private List<Stock> stocks;

        /// <summary>
        /// List of transactions, in order of occurence
        /// </summary>
        private List<Transaction> transactionHistory;

        /// <summary>
        /// Returns the list of transactions
        /// </summary>
        public List<Transaction> TransactionHistory
        {
            get
            {
                return transactionHistory;
            }
        }

        /// <summary>
        /// Constructor for the account class.
        /// </summary>
        /// <param name="name">The name of the account</param>
        /// <param name="stocks">A reference to the list of stocks</param>
        public Account(string name, List<Stock> stocks)
        {
            accountName = name;
            this.stocks = stocks;
            cashBalance = 0;
            depositedCash = 0;
            period = 1;
            transactionHistory = new List<Transaction>();
        }

        /// <summary>
        /// Creates a new portfolio
        /// </summary>
        /// <param name="name">The name of the portfolio</param>
        public void CreatePortfolio(string name)
        {
            //Find a way to implement functionality identical to ms explorer where
            //it automatically applies (1), (2), (3)... to identical filenames
            
            portfolios.Add(new Portfolio(name));
        }

        /// <summary>
        /// Returns the percentage of the total value in the account that this portfolio represents
        /// </summary>
        /// <param name="portfolioName">the name of the portfolio in question</param>
        /// <param name="value">The dollar amount of this portfolio</param>
        /// <returns>The percentage this portfoilio represents</returns>
        public double PortfolioPercentage(string portfolioName, out double value)
        {
            Portfolio p = new Portfolio("0");
            double totalValue = 0;
            foreach(Portfolio po in portfolios)
            {
                totalValue += po.PositionsBalance;
                if (po.Name == portfolioName)
                {
                    p = po;
                }
            }
            value = p.PositionsBalance;
            return value / totalValue * 100;
        }

        /// <summary>
        /// Gets the gain/loss of the entire account as a precent and in dollars
        /// </summary>
        /// <param name="value">The amount gain/loss in dollars</param>
        /// <returns>The precentage gain/loss</returns>
        public double GainLoss(out double value)
        {
            double amount = 0;
            foreach (Portfolio p in portfolios)
            {
                amount += p.PositionsBalance;
            }
            value = (double)Math.Round(amount + cashBalance - depositedCash,2);
            return (double)Math.Round((value/depositedCash) * 100,2);
        }

        public void DisplayHistory()
        {
            if (transactionHistory != null)
            {
                Console.WriteLine();
                foreach (Transaction t in transactionHistory)
                {
                    Console.WriteLine(t.ToString());
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Sells a specified stock from a specified portfolio
        /// </summary>
        /// <param name="portName">Name of the portfolio</param>
        /// <param name="stockName">Name of the stock</param>
        /// <param name="amount">Amount to sell</param>
        /// <returns>Whether or not the sale succeeded or failed</returns>
        public bool Sell(string portName, string stockName, int amount)
        {
            Portfolio selectedPortfolio = null;
            Stock selectedStock = null;
            foreach (Portfolio p in portfolios)
            {
                if (p.Name == portName)
                {
                    selectedPortfolio = p;
                }
            }
            if (selectedPortfolio.Name == null)
            {
                Console.WriteLine("Error, portfolio not found");
                return false;
            }
            foreach(KeyValuePair<Stock, int> s in selectedPortfolio.Stocks)
            {
                if (s.Key.Name == stockName)
                {
                    selectedStock = s.Key;
                }
            }
            if (selectedStock == null)
            {
                Console.WriteLine("Error, stock not found");
                return false;
            }
            double gainOnSale = selectedPortfolio.sell(selectedStock, amount);
            cashBalance += gainOnSale;
            transactionHistory.Add(new Transaction(Transaction.Event.SOLD_STOCK, selectedPortfolio, selectedStock, selectedStock.StockPrice, amount, gainOnSale));
            return true;
        }

        /// <summary>
        /// Buys a specified stock
        /// </summary>
        /// <param name="portName">Name of the portfolio</param>
        /// <param name="stockName">Name of the stock to buy</param>
        /// <param name="amount">Amount to be bought</param>
        /// <returns>An integer code referring to the outcome of the operation. 0 means there was no problem, 1 means the portfolio doesnt exist, 2 means the stock could not be found, 3 means there isn't enough funds to make the purchase</returns>
        public int Buy(string portName, string stockName, int amount)
        {
            
            Portfolio selectedPortfolio = null;
            Stock selectedStock = null;
            foreach (Portfolio p in portfolios)
            {
                if (p.Name == portName)
                {
                    selectedPortfolio = p;
                }
            }
            if (selectedPortfolio.Name == null)
            {
                Console.WriteLine("Error, portfolio not found");
                return 1;
            }
            foreach (Stock s in stocks)
            {
                if (s.Name == stockName)
                {
                    selectedStock = s;
                }
            }
            if (selectedStock == null)
            {
                Console.WriteLine("Error, stock not found");
                return 2;
            }
            if (selectedStock.StockPrice * amount + TRADEFEE > cashBalance)
            {
                Console.WriteLine("Error, not enough funds to make purchase");
                return 3;
            }
            cashBalance -= selectedStock.StockPrice * amount + TRADEFEE;
            selectedPortfolio.Add(selectedStock, amount);
            depositedCash -= TRADEFEE;
            transactionHistory.Add(new Transaction(Transaction.Event.PURCHASED_STOCK, selectedPortfolio, selectedStock, selectedStock.StockPrice, amount));
            return 0;
        }

        /// <summary>
        /// Withrdaws money from the account
        /// </summary>
        /// <param name="amount">Amount to be withdrawn</param>
        /// <returns>Boolean that indicates the success of the withdrawl</returns>
        public bool Withdraw(double amount)
        {
            if (amount + TRANSFEE <= cashBalance)
            {
                cashBalance -= amount + TRANSFEE;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deposits money into the account
        /// </summary>
        /// <param name="amount">Amount to be deposited</param>
        /// <returns>Boolean indicating the success of the operation</returns>
        public bool Deposit(double amount)
        {
            if (amount > TRANSFEE)
            {
                cashBalance += amount - TRANSFEE;
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Finds a portfolio with the given string name
        /// </summary>
        /// <param name="name">The name of the portfolio</param>
        /// <returns>A portfolio with a matching name or null if none are found</returns>
        public Portfolio findPortfolio(string name)
        {
            foreach (Portfolio p in Portfolios)
            {
                if (p.Name == name)
                {
                    return p;
                }
            }
            return null;
        }
    }
}
