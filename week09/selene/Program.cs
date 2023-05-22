using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Net.Http.Json;

namespace Lab07_Banking_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //testing the visa account
            Console.WriteLine("\nAll acounts:");
            Bank.PrintAccounts();
            Console.WriteLine("\nAll Users:");
            Bank.PrintPersons();

            Person p0, p1, p2, p3, p4, p5, p6, p7, p8;
            p0 = Bank.GetPerson("Narendra");
            p1 = Bank.GetPerson("Ilia");
            p2 = Bank.GetPerson("Tom");
            p3 = Bank.GetPerson("Syed");
            p4 = Bank.GetPerson("Arben");
            p5 = Bank.GetPerson("Patrick");
            p6 = Bank.GetPerson("Yin");
            p7 = Bank.GetPerson("Hao");
            p8 = Bank.GetPerson("Jake");

            p0.Login("123"); p1.Login("234");
            p2.Login("345"); p3.Login("456");
            p4.Login("567"); p5.Login("678");
            p6.Login("789"); p7.Login("890");

            //a visa account
            VisaAccount a = (VisaAccount)Bank.GetAccount("VS-100000");
            a.DoPayment(0, p0);
            a.DoPurchase(200, p1);
            a.DoPurchase(25, p2);
            a.DoPurchase(15, p0);
            a.DoPurchase(39, p1);
            a.DoPayment(400, p0);
            Console.WriteLine(a);

            a = (VisaAccount)Bank.GetAccount("VS-100001");
            a.DoPayment(25, p3);
            //a.DoPurchase(25, p1);
            //a.DoPurchase(20, p4);
            //a.DoPurchase(15, p2);
            a.DoPayment(400, p0);
            Console.WriteLine(a);

            //a saving account
            SavingAccount b = (SavingAccount)Bank.GetAccount("SV-100002");
            b.Withdraw(300, p0);
            b.Withdraw(32.90, p6);
            b.Withdraw(50, p5);
            b.Withdraw(111.11, p5);
            Console.WriteLine(b);

            b = (SavingAccount)Bank.GetAccount("SV-100003");
            b.Deposit(300, p3);     //ok even though p3 is not a holder
            b.Deposit(32.90, p2);
            b.Deposit(50, p5);
            b.Withdraw(111.11, p5);
            Console.WriteLine(b);

            //a checking account
            CheckingAccount c = (CheckingAccount)Bank.GetAccount("CK-100005");
            c.Deposit(33.33, p7);
            c.Deposit(40.44, p7);
            c.Withdraw(450, p5);
            c.Withdraw(500, p5);
            c.Withdraw(645, p5);
            c.Withdraw(850, p6);
            Console.WriteLine(c);

            c = (CheckingAccount)Bank.GetAccount("CK-100004");
            c.Deposit(33.33, p7);
            c.Deposit(40.44, p7);
            c.Withdraw(150, p5);
            c.Withdraw(200, p7);
            c.Withdraw(645, p7);
            c.Withdraw(350, p5);
            Console.WriteLine(c);

            Console.WriteLine("\n\nExceptions:");
            //The following will cause exception
            try
            {
                p8.Login("911");//incorrect password
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                p3.Logout();
                a.DoPurchase(12.5, p3);     //exception user is not logged in
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                a.DoPurchase(12.5, p0); //user is not associated with this account
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                a.DoPurchase(5825, p2); //credit limit exceeded
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }
            try
            {
                c.Withdraw(1500, p5); //no overdraft
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                Bank.GetAccount("CK-100008"); //account does not exist
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                Bank.GetPerson("Trudeau"); //user does not exist
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }
            Console.WriteLine("\nBefore PrepareMonthlyReport()");
            Console.WriteLine(c);

            Console.WriteLine("\nAfter PrepareMonthlyReport()");
            c.PrepareMonthlyReport();   //all transactions are cleared, balance changes
            Console.WriteLine(c);
        }
        class Person
        {
            private string password;
            public readonly string SIN;
            public bool IsAuthenticated { get; private set; }
            public string Name { get; private set; }

            public Person(string name, string sin)
            {
                Name = name;
                SIN = sin;
                password = SIN.Substring(0, 3);
            }
            public void Login(string password)
            {
                if (password != this.password)
                {
                    throw new AccountException(AccountException.PASSWORD_INCORRECT);
                }
                else
                {
                    IsAuthenticated = true;
                }
            }
            public void Logout()
            {
                IsAuthenticated = false;
            }
            public override string ToString()
            {
                return $"{Name}: {(IsAuthenticated ? "Authenticated" : "Not Authenticated")}";
            }
        }
        struct Transaction
        {
            public string AccountNumber { get; }
            public double Amount { get; }
            public double EndBalance { get; }
            public Person Originator { get; }
            public DateTime Time { get; }

            public Transaction(string accountNumber, double amount, double endBalance, Person person, DateTime time)
            {
                AccountNumber = accountNumber;
                Amount = amount;
                EndBalance = endBalance;
                Originator = person;
                Time = time;
            }
            public override string ToString()
            {
                if (Amount >= 0)
                {
                    return $"Deposit Transaction for Account Number {AccountNumber} by {Originator.Name} for {Amount} at {Time.ToShortTimeString()} Endbalance {EndBalance}";
                }
                else
                {
                    return $"Withdraw Transaction for Account Number {AccountNumber} by {Originator.Name} for {Amount} at {Time.ToShortTimeString()} Endbalance: {EndBalance}";
                }
            }
        }
        class AccountException : Exception
        {
            public const string ACCOUNT_DOES_NOT_EXIST = "This account does not exist.";
            public const string CREDIT_LIMIT_HAS_BEEN_EXCEEDED = "Your credit limit has been exceeded.";
            public const string NAME_NOT_ASSOCIATED_WITH_ACCOUNT = "This name is not associated with this account.";
            public const string NO_OVERDRAFT = "There is no overdraft available for this account.";
            public const string PASSWORD_INCORRECT = "Password is incorrect.";
            public const string USER_DOES_NOT_EXIST = "This user does not exist.";
            public const string USER_NOT_LOGGED_IN = "User is not logged in.";

            public AccountException()
                : base()
            {

            }
            public AccountException(string reason)
                : base(reason)
            {

            }
        }
        abstract class Account
        {
            public readonly List<Person> holders = new List<Person>();
            public readonly List<Transaction> transactions = new List<Transaction>();
            public readonly string Number;
            private static int LAST_NUMBER = 100_000;

            public double Balance { get; protected set; }
            public double LowestBalance { get; protected set; }
            public string AccountNumber { get => Number; }

            public Account(string type, double balance)
            {
                LowestBalance = balance;
                Balance = balance;
                Number = type + LAST_NUMBER;
                LAST_NUMBER++;
            }
            public void AddUser(Person person)
            {
                holders.Add(person);
            }
            public void Deposit(double amount, Person person)
            {
                Balance += amount;
                if (Balance < LowestBalance)
                {
                    LowestBalance = Balance;
                }
                Transaction transaction = new Transaction(Number, amount, Balance, person, DateTime.Now);
                transactions.Add(transaction);
            }
            public bool IsHolder(string name)
            {
                foreach (Person person in holders)
                {
                    if (person.Name == name)
                    {
                        return true;
                    }
                }
                return false;
            }
            public abstract void PrepareMonthlyReport();

            public override string ToString()
            {
                string result = "Account Number: " + Number + "\nHolders: ";
                foreach (Person person in holders)
                {
                    result += person.Name + ", ";
                }
                result.TrimEnd(',', ' ');
                result += "\nBalance: " + "\nTransactions:\n";
                foreach (Transaction transaction in transactions)
                {
                    result += transaction.ToString() + "\n";
                }
                return result;
            }
        }
        class CheckingAccount : Account
        {
            private static double COST_PER_TRANSACTION = 0.05;
            private static double INTEREST_RATE = 0.005;

            private bool hasOverDraft;

            public CheckingAccount(double balance = 0, bool hasOverdraft = false)
                : base("CK-", balance)
            {
                this.hasOverDraft = hasOverdraft;
            }
            public new void Deposit(double amount, Person person)
            {
                base.Deposit(amount, person);
            }
            public void Withdraw(double amount, Person person)
            {
                if (!holders.Contains(person))
                {
                    throw new AccountException(AccountException.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
                }
                if (person.IsAuthenticated == false)
                {
                    throw new AccountException(AccountException.USER_NOT_LOGGED_IN);
                }
                if (amount > Balance && !hasOverDraft)
                {
                    throw new AccountException(AccountException.NO_OVERDRAFT);
                }
                base.Deposit(-amount, person);
            }
            public override void PrepareMonthlyReport()
            {
                double serviceCharge = transactions.Count * COST_PER_TRANSACTION;
                double interest = Balance * INTEREST_RATE / 12;
                Balance += interest - serviceCharge;
                transactions.Clear();
            }
        }
        class SavingAccount : Account
        {
            public static double COST_PER_TRANSACTION = 0.05;
            public static double INTEREST_RATE = 0.015;

            public SavingAccount(double balance = 0, bool hasOverDraft = false)
                : base("SV-", balance)
            {

            }
            public new void Deposit(double amount, Person person)
            {
                base.Deposit(amount, person);
            }
            public void Withdraw(double amount, Person person)
            {
                if (!holders.Contains(person))
                {
                    throw new AccountException(AccountException.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
                }
                if (person.IsAuthenticated == false)
                {
                    throw new AccountException(AccountException.USER_NOT_LOGGED_IN);
                }
                if (amount > Balance)
                {
                    throw new AccountException(AccountException.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
                }
                base.Deposit(-amount, person);
            }
            public override void PrepareMonthlyReport()
            {
                double serviceCharge = transactions.Count * COST_PER_TRANSACTION;
                double interest = LowestBalance * INTEREST_RATE / 12;
                Balance += interest - serviceCharge;
                transactions.Clear();
            }
        }
        class VisaAccount : Account
        {
            private double creditLimit;
            private const double INTEREST_RATE = 0.1995;

            public VisaAccount(double balance = 0, double creditLimit = 1200)
                : base("VS-", balance)
            {
                this.creditLimit = creditLimit;
            }
            public void DoPayment(double amount, Person person)
            {
                base.Deposit(amount, person);
            }
            public void DoPurchase(double amount, Person person)
            {
                if (!holders.Contains(person))
                {
                    throw new AccountException(AccountException.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
                }
                if (person.IsAuthenticated == false)
                {
                    throw new AccountException(AccountException.USER_NOT_LOGGED_IN);
                }
                if (amount > creditLimit - Balance)
                {
                    throw new AccountException(AccountException.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
                }
                base.Deposit(-amount, person);
            }
            public override void PrepareMonthlyReport()
            {
                double interest = (LowestBalance * INTEREST_RATE) / 12;
                Balance -= interest;
                transactions.Clear();
            }
        }
        static class Bank
        {
            private static List<Account> accounts;
            private static List<Person> persons;

            public static void PrintAccounts()
            {
                foreach (var account in accounts)
                {
                    Console.WriteLine(account);
                }
            }

            public static void PrintPersons()
            {
                foreach (var person in persons)
                {
                    Console.WriteLine(person);
                }
            }

            public static void SaveAccounts(string filename)
            {
                //string json = JsonConvert.SerializeObject(accounts);
                //File.WriteAllText(filename, json);

                string json = JsonConvert.SerializeObject(accounts);
                File.WriteAllText(filename, json);
            }

            public static Person GetPerson(string name)
            {
                foreach (var person in persons)
                {
                    if (person.Name == name)
                    {
                        return person;
                    }
                }
                throw new AccountException(AccountException.USER_DOES_NOT_EXIST);
            }

            public static Account GetAccount(string number)
            {
                foreach (var account in accounts)
                {
                    if (account.Number == number)
                    {
                        return account;
                    }
                }
                throw new AccountException(AccountException.ACCOUNT_DOES_NOT_EXIST);
            }

            static Bank()
            {
                Initialize();
            }

            static void Initialize()
            {
                CreateAccounts();
                CreatePersons();

                accounts[0].AddUser(persons[0]);
                accounts[0].AddUser(persons[1]);
                accounts[0].AddUser(persons[2]);

                accounts[1].AddUser(persons[3]);
                accounts[1].AddUser(persons[4]);
                accounts[1].AddUser(persons[2]);

                accounts[2].AddUser(persons[0]);
                accounts[2].AddUser(persons[5]);
                accounts[2].AddUser(persons[6]);

                accounts[3].AddUser(persons[5]);
                accounts[3].AddUser(persons[6]);

                accounts[4].AddUser(persons[5]);
                accounts[4].AddUser(persons[7]);
                accounts[4].AddUser(persons[8]);

                accounts[5].AddUser(persons[5]);
                accounts[5].AddUser(persons[6]);

                AddUserToAccount("VS-100000", "Narendra");
                AddUserToAccount("VS-100000", "Ilia");
                AddUserToAccount("VS-100000", "Tom");

                AddUserToAccount("VS-100001", "Syed");
                AddUserToAccount("VS-100001", "Arben");
                AddUserToAccount("VS-100001", "Tom");

                AddUserToAccount("SV-100002", "Narendra");
                AddUserToAccount("SV-100002", "Patrick");
                AddUserToAccount("SV-100002", "Yin");

                AddUserToAccount("SV-100003", "Patrick");
                AddUserToAccount("SV-100003", "Yin");

                AddUserToAccount("CK-100004", "Patrick");
                AddUserToAccount("CK-100004", "Hao");
                AddUserToAccount("CK-100004", "Jake");

                AddUserToAccount("CK-100005", "Patrick");
                AddUserToAccount("CK-100005", "Yin");
            }

            static void CreatePersons()
            {
                persons = new List<Person>{
                    new Person("Narendra", "1234-5678"),
                    new Person("Ilia", "2345-6789"),
                    new Person("Tom", "3456-7890"),
                    new Person("Syed", "4567-8901"),
                    new Person("Arben", "5678-9012"),
                    new Person("Patrick", "6789-0123"),
                    new Person("Yin", "7890-1234"),
                    new Person("Hao", "8901-2345"),
                    new Person("Jake", "9012-3456")
                    };
            }

            static void CreateAccounts()
            {
                accounts = new List<Account>{
                    new VisaAccount(),              //VS-100000
                    new VisaAccount(550, -500),     //VS-100001
                    new SavingAccount(5000),        //SV-100002
                    new SavingAccount(),            //SV-100003
                    new CheckingAccount(2000),      //CK-100004
                    new CheckingAccount(1500, true) //CK-100005
                };
            }

            static void AddUserToAccount(string number, string name)
            {
                Account account = GetAccount(number);
                Person person = GetPerson(name);
                account.AddUser(person);
            }
        }
    }
}
