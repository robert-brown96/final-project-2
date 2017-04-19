using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models;
using System.Web.Mvc;
using FinalProject.Utilities;
using FinalProject.DAL;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinalProject.Models
{
    public class StockPortfolio
    {

        //account constructor
        public void CreateStockPortfolio()
        {
            CashBalance = InitialDeposit;
            //Transaction FirstTransaction = new Transaction();
            ////set amount from transaction
            //Balance = FirstTransaction.Amount;
            ////auto increment account number
            //AccountNumber = AccountUtitlities.AccountNumber + BankAccountID;


        }

        //primary key 
        //[Key, ForeignKey("User")]
        public int StockPortfolioID { get; set; }

        public Int32 AccountNumber { get; set; }

        //cash value
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Cash Balance")]
        public Double CashBalance { get; set; }

        [Display(Name = "Portfolio Name")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Stock Balance")]
        private Double _doubStockBalance;
        public double StockBalance
        {
            get
            {
                _doubStockBalance = CalcStockBalance();
                return _doubStockBalance;
            }
        }
        
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Portfolio Balance")]
        public Double Balance
        {
            get { return _doubStockBalance + CashBalance; }           
        }

        [Display(Name = "Initital Deposit")]
       // [Required(ErrorMessage = "An initial deposit is required")]
        public double InitialDeposit { get; set; }

        //TODO: add 1:1 relationship
        [Required]
        public virtual AppUser User { get; set; }

       
        public virtual List<StockTransaction> Transactions { get; set; }
       


        public virtual List<Withdraw> Withdrawals { get; set; }
        public virtual List<Deposit> Deposits { get; set; }
        


        public Double CalcStockBalance()
        {
            Double balance = 0;

            foreach(var item in Transactions)
            {
                

                //get all transactions with same symbol
                var query = from t in Transactions
                            where t.Stock.Symbol == item.Stock.Symbol
                            select t;

                List<StockTransaction> StockList = query.ToList();
                int Shares = 0;

                int count = 0;
                foreach (var order in StockList)
                {
                    
                    

                    if (order.Order == OrderType.Buy)
                    {
                        Shares += order.Shares;
                    }

                    if (order.Order == OrderType.Sell)
                    {
                        Shares -= order.Shares;
                    }
                    count += 1;
                }

                Double Fees = count * item.Stock.Fee;

                balance += Shares * item.Stock.CurrentPrice - Fees;
                  

            }


            return balance;
        }
    }
}