using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week07a_AccountLibrary
{

    public class AccountException : Exception
    {
        static void Main(string[] args)
        {

        }

        public AccountException(string reason)
        : base(reason)
        { }
    }
    public class Account
    {
        public double Balance { get; private set; }
        public Account(double balance) => Balance = balance;
        public void Deposit(double amount) => Balance += amount;
        public void Withdraw(double amount)
        {
            if (amount <= 0) throw new Exception("Amount can not be negative");
            if (amount > Balance)
            {
                throw new AccountException("Insufficient funds");
            }
            else
            {
                Balance -= amount;
            }
        }
        public override string ToString() => $"Bal: {Balance:c}";
    }
}