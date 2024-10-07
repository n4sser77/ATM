using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankomaten
{
    internal class Account
    {

        public  string username { get; set; }
        public double Balance { get; private set; }

        public Account(string username, double balance)
        {
            username = username;
            Balance = balance;
        }

        public void ShowMenu()
        {
            Console.WriteLine("[I]nsättning");
            Console.WriteLine("[U]ttag");
            Console.WriteLine("[S]aldo");
            Console.WriteLine("[A]vsluta");
        }

        public void Deposit(double amount)
        {
            if (amount >= 10000)
            {
                Console.WriteLine("You're trying to deposit a large amount");
                Console.WriteLine("The limit is 10 000kr");
            }
            else if (amount >= 25)
            {
                Balance += amount;
            }
        }

        public void Withdraw(double amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("insufficient funds");
            }
            else if (amount > 100 && amount <= Balance)
            {
                Balance -= amount;
            }
        }
       public  void GetBalance()
        {
            Console.WriteLine("You have: " + Balance + " SEK " + "in your account");
        }
    }
}
