// See https://aka.ms/new-console-template for more information
using banklibrary;
using System;
using bankDB;
using System.Data.SqlClient;

namespace bankrepository
{
    public class BankRepository : IBankRepository
    {
        //BankDataBase bankdbobj = BankDataBase();
        BankDataBase bankdbobj = new BankDataBase();
        
        public class AccnumberException : ApplicationException
        {
            public AccnumberException(string message) : base(message) { }
        }



        List<SBAccount> accounts = new List<SBAccount>();
        List<SBTransaction> transactions = new List<SBTransaction>();
        public int _id = 1;

        // **************************** Creating new account ****************************
        public void NewAccount(SBAccount acc)
        {

            SBAccount user = bankdbobj.insertAccountDetails(acc);
            if (user != null)
            {
                Console.WriteLine("\n***************************** Welcome {0}! Account created successfully *****************************\n", user.CustomerName);
                DisplayAcc(user);
                
            }
        }

        // **************************** Getting account details ****************************
        public SBAccount GetAccountDetails(long accno)
        {
            return bankdbobj.retriveAccountDetails(accno);

        }
        // **************************** Getting All account details ****************************
        public List<SBAccount> GetAllAccounts()
        {

            return bankdbobj.retriveAllAccounts();
        }


        // **************************** Deposit Amount **************************
        public void DepositAmount(long accno, decimal amt)
        {
            if (bankdbobj.checkUserExists(accno))
            {
                bankdbobj.depositAmount(accno, amt);
                Console.WriteLine("Deposited successfully\n");
            }
            else
                Console.WriteLine("Account Not found");
        }

        // **************************** Withdraw Amount **************************
        public void WithdrawAmount(long accno, decimal amt)
        {
            if(bankdbobj.checkUserExists(accno))
            {
                if (bankdbobj.withdrawAmount(accno, amt))
                    Console.WriteLine("Withdraw successfull\n");
            }
            else
                Console.WriteLine("Account not found");
        }

        // **************************** Getting Transaction of single account **************************
        public List<SBTransaction> GetTransactions(long accno)
        {
            return bankdbobj.displayTransaction(accno); 
            
        }


        public void DisplayAcc(SBAccount acc)
        {
            Console.WriteLine("Account Number: " + acc.AccountNumber +
                " Customer Name: " + acc.CustomerName +
                " Customer Address: " + acc.CustomerAddress +
                " Current Balance: " + acc.CurrentBalance);
        }

        public void DisplayTransac(SBTransaction alltransactions)
        {
            Console.WriteLine("Transaction Id Number: " + alltransactions.TransactionId +
                " Transaction Date: " + alltransactions.TransactionDate +
                " Account number: " + alltransactions.AccountNumber +
                " Amount: " + alltransactions.Amount +
                " Transaction type: " + alltransactions.TransactionType);
           
        }

        public bool ValidateAccNum(long accno)
        {

            if ((accno > 999999999) && (accno < 10000000000))
                return true;
            else
                throw new AccnumberException("Account number should contain 10 digits");
        }
    }
}


