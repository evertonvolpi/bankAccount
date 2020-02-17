using System;
using System.Collections.Generic;
using System.Text;

namespace bankAccount
{
    class AccountClass
    {
        public double balance { get; set; }
        public string customerName { get; }
        public double monthlyDepositAmount { get; }
        public int accountNumber { get; }

        static double monthlyFee = 4.0;
        static double monthlyInterestRate = 0.0025;
        static double minimumInitialBalance = 1000.0;

        public AccountClass (string newName, double newBalance)
        {
            customerName = newName;

            while (newBalance < minimumInitialBalance)
            {
                Console.Write("The initial deposit cannot be lower than $ {0}. Please enter new value: ", minimumInitialBalance);
                while (!double.TryParse(Console.ReadLine(), out newBalance))
                {
                    Console.Write("Please enter a valid value: $ ");
                }
            }

            balance = newBalance;

            Random random = new Random();
            accountNumber = random.Next(90000, 99999);
        }

        public double Withdraw (double valueToWithdraw)
        {
            while (valueToWithdraw > balance)
            {
                Console.Write("\nInsuficient funds. Please enter new value: ");
                while (!double.TryParse(Console.ReadLine(), out valueToWithdraw))
                {
                    Console.Write("\nPlease enter a valid value: ");
                }
            }

            balance -= valueToWithdraw;
            return balance;
        }

        public double MonthlyFeeDeduction ()
        {
            balance -= monthlyFee;
            return balance;
        }

        public double Deposit (double valueToDeposit)
        {
            balance += valueToDeposit;
            return balance;
        }

        public double MonthlyInterest()
        {
            balance += (balance * monthlyInterestRate);
            return balance;
        }
    }
}
