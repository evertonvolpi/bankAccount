using System;
using System.Collections.Generic;
using System.Linq;

namespace bankAccount
{
    class Program
    {
        static List<AccountClass> allAccounts = new List<AccountClass>();

        static bool atLeastOneAccount = false;

        static int number;
        static void CreateAccount ()
        {
            Console.Write("Enter customer name: ");
            string customer = Console.ReadLine();
            while (string.IsNullOrEmpty(customer))
            {
                Console.Write("Enter a valid name: ");
                customer = Console.ReadLine();
            }

            Console.Write("Enter initial deposit value (min $ 1000): $ ");
            double initialDeposit;
            while (!double.TryParse(Console.ReadLine(), out initialDeposit))
            {
                Console.Write("Enter a valid value: $ ");
            }

            AccountClass newAccount = new AccountClass(customer, initialDeposit);
            allAccounts.Add(newAccount);
            Console.WriteLine("{0}'s Account, Number  *** {1} ***, was successfuly created.", customer, newAccount.accountNumber);
            atLeastOneAccount = true;

            Console.Write("Do you wish to create another account? (y) > ");
            string choice = Console.ReadLine();
            if (choice == "y" || choice == "Y")
            {
                CreateAccount();
            }
            else
            {
                Main();
            }
        }

        static void AccessAccount ()
        {
            if (atLeastOneAccount == true)
            {
                Console.Write("Enter the number of the account you wish to access: ");
                while (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.Write("Enter a valid account: ");
                }

                bool accountExists = false;

                foreach (AccountClass account in allAccounts)
                {
                    if (number == account.accountNumber)
                    {
                        Console.WriteLine("*******\nAccount number: {0}\nCustomer: {1}\nBalance: $ {2}\n*******", account.accountNumber, account.customerName, account.balance);
                        accountExists = true;
                    }
                }

                if (accountExists == false)
                {
                    Console.WriteLine("This account does not exist in our system.");
                    Main();
                }

            }
            else
            {
                Console.WriteLine("This bank has no accounts! Help it creating the first account.");
                CreateAccount();
            }
        }

        static void calculateInterest(int accountNumber)
        {
            Console.Write("How many months will you keep the monthly deposits: ");
            int months;
            while (!int.TryParse(Console.ReadLine(), out months))
            {
                Console.Write("Enter a valid number: ");
            }

            Console.Write("Enter the fixed value you wish to deposit every month (minimum $ 50): $ ");
            double monthDeposit;
            while (!double.TryParse(Console.ReadLine(), out monthDeposit) || monthDeposit < 50.0)
            {
                Console.Write("Enter a valid value (minimum $ 50): $ ");
            }

            foreach (AccountClass account in allAccounts)
            {
                if (accountNumber == account.accountNumber)
                {
                    double atualBalance = account.balance;
                    double simulatedBalance = account.balance;

                    for (int i = 0; i < months; i++)
                    {
                        account.balance = account.MonthlyFeeDeduction();
                        account.balance = account.MonthlyInterest();
                        account.balance = account.Deposit(monthDeposit);
                        simulatedBalance = account.balance;
                    }

                    account.balance = atualBalance;
                    Console.WriteLine("After {0} months, {1}'s account (#{2}), has a balance of $ {3:F2}. Actual balance: $ {4}.", months, account.customerName, accountNumber, simulatedBalance, account.balance);
                }
            }
        }

        static void Withdraw (int accountNumber)
        {
            Console.Write("Enter the value you wish to withdraw: $ ");
            double valueToWithdraw;
            while (!double.TryParse(Console.ReadLine(), out valueToWithdraw))
            {
                Console.Write("Enter a valid number: $ ");
            }

            foreach (AccountClass account in allAccounts)
            {
                if (accountNumber == account.accountNumber)
                {
                    account.balance = account.Withdraw(valueToWithdraw);

                    Console.WriteLine("Account #{0} new Balance: $ {1}", accountNumber, account.balance);
                }
            }
        }

        static void Deposit(int accountNumber)
        {
            Console.Write("Enter the value you wish to deposit: $ ");
            double valueToDeposit;
            while (!double.TryParse(Console.ReadLine(), out valueToDeposit))
            {
                Console.Write("Enter a valid number: $ ");
            }

            foreach (AccountClass account in allAccounts)
            {
                if (accountNumber == account.accountNumber)
                {
                    account.balance = account.Deposit(valueToDeposit);

                    Console.WriteLine("Account #{0} new Balance: $ {1}", accountNumber, account.balance);
                }
            }
        }

        static void Main()
        {
            Console.Write("Create Account (c)\nAccess Account (a)\nAny other value to exit.\n>>> ");
            string choice = Console.ReadLine();
            if (choice == "c" || choice == "C")
            {
                CreateAccount();
            }

            else if (choice == "a" || choice == "A")
            {
                AccessAccount();
                Console.Write("Simulate investment (s)\nDeposit (d)\nWithdraw (w)\nAny value to go back to main menu.\n>>> ");
                string internChoice = Console.ReadLine();
                if (internChoice == "s" || internChoice == "S")
                {
                    calculateInterest(number);
                    Main();
                }
                else if (internChoice == "d" || internChoice == "D")
                {
                    Deposit(number);
                    Main();
                }
                else if (internChoice == "w" || internChoice == "W")
                {
                    Withdraw(number);
                    Main();
                }
                else
                {
                    Main();
                }

            }

        }
    }
}
