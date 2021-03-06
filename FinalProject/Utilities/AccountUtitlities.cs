﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using FinalProject.DAL;

namespace FinalProject.Utilities
{

    public static class AccountUtitlities
    {
        //static class for validation and other misc items
        //glyphicon-wrench
        //glyphicon-comment
        //glyphicon-user
        //glyphicon-th-list


        //list for account types
        public static List<string> AccountTypes()
        {
            List<string> AccountTypeList = new List<string>();

            AccountTypeList.Add("Savings");
            AccountTypeList.Add("Checking");
            AccountTypeList.Add("IRA");
           // AccountTypeList.Add("Stock");         

            return AccountTypeList;
        }

        public static string NullName(BankAccount account)
        {
            string Name;

            Name = "Longhorn " + account.AccountType;

            return Name;
                    
        }

        public static string NullName(StockPortfolio portfolio)
        {
            string name;

            name = portfolio.User.FName + "'s stock portfolio";

            return name;

        }

        //Noah recommendations: Auto increment bank account here. Get largest current bank account and add one. 
        //Call method on the controller. 
        //public static Int32 AccountNumber = 1000000000;
        public static Int32 AddAccountNumber(Int32 LargestAccountNumber)
        {
            Int32 NewAccountNumber;
            NewAccountNumber = LargestAccountNumber + 1;

            return NewAccountNumber;
        }
        public static Int32 SetTransactionNumber(AppDbContext db)
        {
            Int32 transaction_number;


            var query = from a in db.Deposits
                        select a.TransactionNumber;

            var query2 = from p in db.Withdrawals
                         select p.TransactionNumber;

            var query3 = from t in db.Transfers
                         select t.TransactionNumber;



            List<int> AccountNumbers = query.ToList();

            AccountNumbers.AddRange(query2.ToList());

            AccountNumbers.AddRange(query3.ToList());

            transaction_number = AccountNumbers.Max() + 1;

            return transaction_number;

        }



        public static Int32 SetAccountNumber(AppDbContext db)
        {
            Int32 account_number;


            var query = from a in db.Accounts
                        select a.AccountNumber;

            var query2 = from p in db.Portfolios
                         select p.AccountNumber;

            List<int> AccountNumbers = query.ToList();

            AccountNumbers.AddRange(query2.ToList());

            account_number = AccountNumbers.Max() + 1;

            return account_number;

        }

        public static Double CalcStockBalance(StockPortfolio portfolio)
        {
            Double balance = 0;

            foreach (var item in portfolio.Transactions)
            {


                //get all transactions with same symbol
                var query = from t in portfolio.Transactions
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

        public static void GetDescription(Deposit deposit)
        {
            deposit.Description = "Deposit to " + deposit.Account.Name + " on " + deposit.Date;
        }

        public static void GetDescription(Withdraw withdraw)
        {
            withdraw.Description = "Withdrawal from " + withdraw.Account.Name + " on " + withdraw.Date;
        }

        

        //validates IRA deposits
        //returns -1 when there is an error
        public static int IRA_Deposit(Deposit deposit)
        {
            DateTime user = deposit.Account.AppUser.Birthday;

            DateTime today = DateTime.Today;

            int age = today.Year - user.Year;

            if(age > 70)
            {
                return -1;
            }
            else
            {
                decimal contributions = deposit.Amount;

                List<Deposit> Deposits = deposit.Account.Deposits;
                List<Transfer> Transfers = deposit.Account.Transfers;

                foreach(var item in Deposits)
                {
                    contributions += item.Amount;
                }
                foreach(var item in Transfers)
                {
                    decimal amount = item.Amount;
                    contributions += amount;
                }
                if(contributions > 5000)
                {

                    deposit.Amount = 5000 - contributions;
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        //ira withdrawals
        //return -1 if the transaction is unqualified
        public static int IRA_Withdraw(Withdraw withdrawal)
        {
            DateTime bday = withdrawal.Account.AppUser.Birthday;
            DateTime now = DateTime.Today;

            //calculate age
            int age = now.Year - bday.Year;

            //qualified
            if (age > 65)
            {
                return 0;
            }
            //unqualified
            else
            {
                //TODO: add logic to apply fees for unqualified transaction

                //max for unqualified 
                if(withdrawal.Amount > 3000)
                {
                    
                    withdrawal.Amount = 3000;
                    return -1;
                }
                else
                {
                    return -1;
                }

            }
        }
 





    }
}
 