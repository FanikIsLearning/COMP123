using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week07a_AccountLibrary; //must add a reference to the library project

namespace Week07b_AccountApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //create an account object
            Account account = new Account(500);
            Console.WriteLine(account);
            //call deposit and print
            double amount = 120;
            Console.WriteLine($"Depositing {amount:C} to the object");
            account.Deposit(amount);
            Console.WriteLine(account);
            //call withdraw and print
            amount = -700;
            try
            {
                Console.WriteLine($"Withdrawing {amount:C} from the object");
                account.Withdraw(amount);
                Console.WriteLine(account);
            }
            catch (AccountException e)
            {
                Console.WriteLine($"Could not withdraw {amount:c} from object");
                Console.WriteLine(e.Message);
                Console.WriteLine(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(account);
            }
        }
    }
}