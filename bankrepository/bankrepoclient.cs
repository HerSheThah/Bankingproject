using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using banklibrary;
using System.Text.RegularExpressions;
using bankDB;

namespace bankrepository
    
{
   
    internal class bankrepoclient
    {

        static void Main()
        {
            BankDataBase bdb = new BankDataBase();
            FileStream fs = new FileStream("accountdetails.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            

            Console.WriteLine("*************************************************************************************************************");
            Console.WriteLine("******************************************* WELCOME TO STATE BANK *******************************************");
            Console.WriteLine("*************************************************************************************************************\n");

            BankRepository bankrep = new BankRepository();

            

            bool toContinue = true;
            while (toContinue)
            {
                Console.WriteLine("\nTo Create new account press '1'\n" +
                "To get account details press '2'\n" +
                "To deposit amount press 3\n" +
                "To withdraw amount press 4\n" +
                "To get transaction details press '5'\n" +
                "To get all accounts press '6'\n");

                int choice = 0;
                try
                {
                    choice = int.Parse(Console.ReadLine());

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("\n");
                }
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("*************************************************************************************************************\n");

                            int n = 0;
                            
                                try
                                {
                                    Console.WriteLine("Enter number of new accounts to be created: ");

                                    n = int.Parse(Console.ReadLine());

                                }
                                catch (FormatException fe)
                                {
                                    Console.WriteLine("Invalid data. Enter a number ");
                                }
                            
                            for (int i = 0; i < n; i++)
                            {
                                Console.WriteLine("\n***************************** Creating new account with initial deposit of 20000 *****************************\n");
                                                          
                                Console.WriteLine("Enter fullname: ");
                                string custname = Console.ReadLine();
                                Console.WriteLine("Enter permanent address");
                                string address = Console.ReadLine();
                                //Generating random account number
                                Random rand = new Random();
                                Int64 accnum = rand.NextInt64(999999999, 10000000000);
                                //bdb.insertAccountDetails(accnum, custname, address, 2000);
                                bankrep.NewAccount(new SBAccount(accnum, custname, address, 2000));

                            }
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("*************************************************************************************************************\n");

                            try
                            {
                                Console.WriteLine("Please enter your account number: ");
                                long accno = long.Parse(Console.ReadLine());

                                if (bankrep.ValidateAccNum(accno))
                                {
                                    SBAccount accountdetails = bankrep.GetAccountDetails(accno);
                                    if (accountdetails != null)
                                    {
                                        Console.WriteLine("\n************************ Retriving Account Details ************************\n\n");

                                        bankrep.DisplayAcc(accountdetails);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account not found!");

                                    }
                                }
                            }

                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("*************************************************************************************************************\n");

                            List<SBAccount> accounts = bankrep.GetAllAccounts();
                            if (accounts.Count > 0)
                            {
                                Console.WriteLine("\n************************ Retriving Account Details ************************\n\n");
               
                               foreach (SBAccount account in accounts)
                                {
                                    bankrep.DisplayAcc(account);

                                }
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("No accounts created yet!\n ");
                            }
                        }
                        break;
                    case 3:

                        {
                            Console.WriteLine("*************************************************************************************************************\n");

                            try
                            {
                                Console.WriteLine("Please enter your account number: ");
                                long accno = long.Parse(Console.ReadLine());

                                if (bankrep.ValidateAccNum(accno))
                                {
                                    
                                    try
                                    {
                                        Console.WriteLine("Enter the amount to deposit: ");
                                        decimal amt = decimal.Parse(Console.ReadLine());
                                        if (amt <= 100000)
                                        {
                                            bankrep.DepositAmount(accno, amt);
                                                

                                        }
                                        else
                                        {
                                            Console.WriteLine("Only upto 100000 can be deposited ");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }
                                }

                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("*************************************************************************************************************\n");

                            try
                            {
                                Console.WriteLine("Please enter your account number: ");
                                long accno = long.Parse(Console.ReadLine());

                                if (bankrep.ValidateAccNum(accno))
                                {
                                    try
                                    {
                                        Console.WriteLine("Enter the amount to Withdraw: ");
                                        decimal amt = decimal.Parse(Console.ReadLine());
                                        bankrep.WithdrawAmount(accno, amt);
                                        
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }
                            }

                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("*************************************************************************************************************\n");

                            try
                            {
                                Console.WriteLine("Please enter your account number: ");
                                long accno = long.Parse(Console.ReadLine());

                                if (bankrep.ValidateAccNum(accno))
                                {
                                    List<SBTransaction> alltransac =  bankrep.GetTransactions(accno);
                                    if(alltransac!= null)
                                    {
                                        if (alltransac.Count > 0)
                                        {
                                            Console.WriteLine("\n************************ Retriving Transaction Details ************************\n\n");
                                            
                                            foreach (var transaction in alltransac)
                                                bankrep.DisplayTransac(transaction);
                                        }
                                        else
                                        {
                                            Console.WriteLine("No transactions Found!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account number not found!");
                                    }

                                }

                            }

                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Invalid key.. Press a valid key");
                        }
                        break;

                }
                try
                {
                    Console.WriteLine("\nPress any number from '1-9' to continue with banking, press '0' to quit");
                    int cont = int.Parse(Console.ReadLine());
                    if(cont == 0)
                        toContinue = false;
                }
                catch(Exception e)
                {
                    continue;                  
                }
                
            }
            Console.WriteLine("\n=========Thank you for visiting State Bank!=========");
            }
        }
    }



