using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounts
{
    public enum ExceptionType
    {
        ACCOUNT_DOES_NOT_EXIST,
        CREDIT_LIMIT_HAS_BEEN_EXCEEDED,
        NAME_NOT_ASSOCIATED_WITH_ACCOUNT,
        NO_OVERDRAFT,
        PASSWORD_INCORRECT,
        USER_DOES_NOT_EXIST,
        USER_NOT_LOGGED_IN
    }

    public enum AccountType
    {
        Checking,
        Saving,
        Visa
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nAll acounts:");
            Bank.PrintAccounts();
            Console.WriteLine("\nAll Users:");
            Bank.PrintUsers();


            Person p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10;
            p0 = Bank.GetPerson("Narendra");
            p1 = Bank.GetPerson("Ilia");
            p2 = Bank.GetPerson("Mehrdad");
            p3 = Bank.GetPerson("Vijay");
            p4 = Bank.GetPerson("Arben");
            p5 = Bank.GetPerson("Patrick");
            p6 = Bank.GetPerson("Yin");
            p7 = Bank.GetPerson("Hao");
            p8 = Bank.GetPerson("Jake");
            p9 = Bank.GetPerson("Mayy");
            p10 = Bank.GetPerson("Nicoletta");

            p0.Login("123"); p1.Login("234");
            p2.Login("345"); p3.Login("456");
            p4.Login("567"); p5.Login("678");
            p6.Login("789"); p7.Login("890");
            p10.Login("234"); p8.Login("901");

            //a visa account
            VisaAccount a = Bank.GetAccount("VS-100000") as VisaAccount;
            a.DoPayment(1500, p0);
            a.DoPurchase(200, p1);
            a.DoPurchase(25, p2);
            a.DoPurchase(15, p0);
            a.DoPurchase(39, p1);
            a.DoPayment(400, p0);
            Console.WriteLine(a);

            a = Bank.GetAccount("VS-100001") as VisaAccount;
            a.DoPayment(500, p0);
            a.DoPurchase(25, p3);
            a.DoPurchase(20, p4);
            a.DoPurchase(15, p5);
            Console.WriteLine(a);

            //a saving account
            SavingAccount b = Bank.GetAccount("SV-100002") as SavingAccount;
            b.Withdraw(300, p6);
            b.Withdraw(32.90, p6);
            b.Withdraw(50, p7);
            b.Withdraw(111.11, p8);
            Console.WriteLine(b);

            b = Bank.GetAccount("SV-100003") as SavingAccount;
            b.Deposit(300, p3);     //ok even though p3 is not a holder
            b.Deposit(32.90, p2);
            b.Deposit(50, p5);
            b.Withdraw(111.11, p10);
            Console.WriteLine(b);

            //a checking account
            CheckingAccount c = Bank.GetAccount("CK-100004") as CheckingAccount;
            c.Deposit(33.33, p7);
            c.Deposit(40.44, p7);
            c.Withdraw(150, p2);
            c.Withdraw(200, p4);
            c.Withdraw(645, p6);
            c.Withdraw(350, p6);
            Console.WriteLine(c);

            c = Bank.GetAccount("CK-100005") as CheckingAccount;
            c.Deposit(33.33, p8);
            c.Deposit(40.44, p7);
            c.Withdraw(450, p10);
            c.Withdraw(500, p8);
            c.Withdraw(645, p10);
            c.Withdraw(850, p10);
            Console.WriteLine(c);

            a = Bank.GetAccount("VS-100006") as VisaAccount;
            a.DoPayment(700, p0);
            a.DoPurchase(20, p3);
            a.DoPurchase(10, p1);
            a.DoPurchase(15, p1);
            Console.WriteLine(a);

            b = Bank.GetAccount("SV-100007") as SavingAccount;
            b.Deposit(300, p3);     //ok even though p3 is not a holder
            b.Deposit(32.90, p2);
            b.Deposit(50, p5);
            b.Withdraw(111.11, p7);
            Console.WriteLine(b);

            Console.WriteLine("\n\nExceptions:");
            //The following will cause exception
            try
            {
                p8.Login("911");            //incorrect password
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
                a.DoPurchase(12.5, p0);     //user is not associated with this account
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                a.DoPurchase(5825, p4);     //credit limit exceeded
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }
            try
            {
                c.Withdraw(1500, p6);       //no overdraft
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                Bank.GetAccount("CK-100018"); //account does not exist
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            try
            {
                Bank.GetPerson("Trudeau");  //user does not exist
            }
            catch (AccountException e) { Console.WriteLine(e.Message); }

            //show all transactions
            Console.WriteLine("\n\nAll transactions");
            foreach (var transaction in Bank.GetAllTransactions())
                Console.WriteLine(transaction);
            foreach (var keyValuePair in Bank.ACCOUNTS)
            {
                Account account = keyValuePair.Value;
                Console.WriteLine("\nBefore PrepareMonthlyReport()");
                Console.WriteLine(account);

                Console.WriteLine("\nAfter PrepareMonthlyReport()");
                account.PrepareMonthlyReport();   //all transactions are cleared, balance changes
                Console.WriteLine(account);
            }

            Logger.ShowLoginEvents();
            Logger.ShowTransactionEvents();


        }

        public static class Utils
        {
            static DayTime _time = new DayTime(1_048_000_000);
            static Random random = new Random();
            public static DayTime Time
            {
                get => _time += random.Next(1, 59);
            }
            public static DayTime Now
            {
                get => _time += 0;
            }

            public readonly static Dictionary<AccountType, string> ACCOUNT_TYPES =
                new Dictionary<AccountType, string>
            {
                { AccountType.Checking , "CK" },
                { AccountType.Saving , "SV" },
                { AccountType.Visa , "VS" }
            };

        }


        public interface ITransaction
        {
            void Withdraw(double amount, Person person);
            void Deposit(double amount, Person person);
        }

        public class AccountException : Exception
        {
            public AccountException(ExceptionType reason) : base(reason.ToString())
            {
            }
        }

        // LoginEventArgs.cs
        public class LoginEventArgs : EventArgs
        {
            public string PersonName { get; }
            public bool Success { get; }

            public LoginEventArgs(string personName, bool success) : base()
            {
                PersonName = personName;
                Success = success;
            }
        }

        // TransactionEventArgs.cs
        public class TransactionEventArgs : LoginEventArgs
        {
            public double Amount { get; }

            public TransactionEventArgs(string personName, double amount, bool success) : base(personName, success)
            {
                Amount = amount;
            }
        }


        public struct Transaction
        {
            public string AccountNumber { get; }
            public double Amount { get; }
            public Person Originator { get; }
            public DayTime Time { get; }

            public Transaction(string accountNumber, double amount, Person person)
            {
                AccountNumber = accountNumber;
                Amount = amount;
                Originator = person;
                Time = Utils.Time;
            }

            public override string ToString()
            {
                string operation = Amount >= 0 ? "deposited by" : "withdrawn by";
                return $"{AccountNumber} {Math.Abs(Amount):C} {operation} {Originator.Name} on {Time}";
            }
        }

        public struct DayTime
        {
            private long _minutes;

            public DayTime(long minutes)
            {
                _minutes = minutes;
            }

            public static DayTime operator +(DayTime lhs, int minutes)
            {
                return new DayTime(lhs._minutes + minutes);
            }

            public override string ToString()
            {
                long totalMinutes = _minutes;

                long years = totalMinutes / 518_400;
                totalMinutes %= 518_400;

                long months = totalMinutes / 43_200;
                totalMinutes %= 43_200;

                long days = totalMinutes / 1_440;
                totalMinutes %= 1_440;

                long hours = totalMinutes / 60;
                long remainingMinutes = totalMinutes % 60;

                return $"{years:0000}-{months + 1:00}-{days + 1:00} {hours:00}:{remainingMinutes:00}";
            }
        }

        static class Logger
        {
            private static List<string> loginEvents = new List<string>();
            private static List<string> transactionEvents = new List<string>();

            public static void LoginHandler(object sender, EventArgs args)
            {
                if (args is LoginEventArgs loginArgs)
                {
                    string log = $"{loginArgs.PersonName} {(loginArgs.Success ? "logged in successfully" : "failed to log in")} at {Utils.Now}";
                    loginEvents.Add(log);
                }
            }

            public static void TransactionHandler(object sender, EventArgs args)
            {
                if (args is TransactionEventArgs transactionArgs)
                {
                    string log = $"{transactionArgs.PersonName} {(transactionArgs.Amount < 0 ? "withdraw" : "deposit")} {transactionArgs.Amount:C} {(transactionArgs.Success == true ? "successfully" : "unsuccessfully")} on {Utils.Now}";
                    transactionEvents.Add(log);
                }
            }

            public static void ShowLoginEvents()
            {
                Console.WriteLine($"Login events at {Utils.Now}:");

                for (int i = 0; i < loginEvents.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {loginEvents[i]}");
                }

                Console.WriteLine();
            }

            public static void ShowTransactionEvents()
            {
                Console.WriteLine($"Transaction events at {Utils.Now}:");

                for (int i = 0; i < transactionEvents.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {transactionEvents[i]}");
                }

                Console.WriteLine();
            }
        }

        public class Person
        {
            private string password;
            public event EventHandler<LoginEventArgs> OnLogin;

            public string Sin { get; }
            public string Name { get; }
            public bool IsAuthenticated { get; private set; }

            public Person(string name, string sin)
            {
                Name = name;
                Sin = sin;
                password = Sin.Substring(0, 3);
                IsAuthenticated = false;
            }

            public void Login(string password)
            {
                if (password != this.password)
                {
                    IsAuthenticated = false;
                    OnLogin?.Invoke(this, new LoginEventArgs(Name, false));
                    throw new AccountException(ExceptionType.PASSWORD_INCORRECT);
                }

                IsAuthenticated = true;
                OnLogin?.Invoke(this, new LoginEventArgs(Name, true));
            }

            public void Logout()
            {
                IsAuthenticated = false;
            }

            public override string ToString()
            {
                return $"[{Name}, {Name}  {(IsAuthenticated ? "Logged in" : "Not logged in")}]";
            }
        }


        public abstract class Account
        {
            private static int LAST_NUMBER = 100_000;

            public readonly List<Person> users = new List<Person>();
            public readonly List<Transaction> transactions = new List<Transaction>();

            public event EventHandler<EventArgs> OnTransaction;

            public string Number { get; }
            public double Balance { get; protected set; }
            public double LowestBalance { get; protected set; }

            public Account(string type, double balance)
            {
                Number = $"{type}{LAST_NUMBER}";
                LAST_NUMBER++;

                Balance = balance;
                LowestBalance = balance;

                transactions = new List<Transaction>();
                users = new List<Person>();
            }

            public void Deposit(double amount, Person person)
            {
                Balance += amount;
                if (Balance < LowestBalance)
                {
                    LowestBalance = Balance;
                }

                Transaction transaction = new Transaction(Number, amount, person);
                transactions.Add(transaction);

                OnTransactionOccur(this, EventArgs.Empty);
            }

            public void AddUser(Person person)
            {
                users.Add(person);
            }

            public bool IsUser(string name)
            {
                return users.Any(user => user.Name == name);
            }

            public abstract void PrepareMonthlyReport();

            public virtual void OnTransactionOccur(object sender, EventArgs args)
            {
                OnTransaction?.Invoke(sender, args);
            }

            public override string ToString()
            {
                string userString = string.Join(", ", users.Select(u => u.Name + "  " + (u.IsAuthenticated ? "Logged in" : "Not logged in")).ToArray());
                string transactionString = string.Join("\n   ", transactions);
                if (Bank.GetAccount == Bank.PrintAccounts)
                {
                    return $"{Number}, {userString} ${Balance:F2} - transactions ({transactions.Count})\n   {transactionString}";
                }
                return $"[{Number}, {userString} ${Balance:F2} - transactions ({transactions.Count})]\n   {transactionString}";

            }
        }

        public class CheckingAccount : Account, ITransaction
        {
            private static readonly double COST_PER_TRANSACTION = 0.05;
            private static readonly double INTEREST_RATE = 0.005;
            private bool hasOverdraft;

            public CheckingAccount(double balance = 0, bool hasOverdraft = false) : base("CK-", balance)
            {
                this.hasOverdraft = hasOverdraft;
            }

            public new void Deposit(double amount, Person person)
            {
                base.Deposit(amount, person);
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            }

            public void Withdraw(double amount, Person person)
            {
                if (!base.IsUser(person.Name))
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
                }

                if (!person.IsAuthenticated)
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
                }

                if (amount > base.Balance && !hasOverdraft)
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.NO_OVERDRAFT);
                }

                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
                base.Deposit(-amount, person);
            }

            public override void PrepareMonthlyReport()
            {
                double serviceCharge = base.transactions.Count * COST_PER_TRANSACTION;
                double interest = base.LowestBalance * INTEREST_RATE / 12;
                base.Balance += interest - serviceCharge;
                base.transactions.Clear();
            }
        }

        public class SavingAccount : Account, ITransaction
        {
            private static readonly double COST_PER_TRANSACTION = 0.5;
            private static readonly double INTEREST_RATE = 0.015;

            public SavingAccount(double balance = 0) : base("SV-", balance)
            {
            }

            public new void Deposit(double amount, Person person)
            {
                base.Deposit(amount, person);
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            }

            public void Withdraw(double amount, Person person)
            {
                if (!base.IsUser(person.Name))
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
                }

                if (!person.IsAuthenticated)
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
                }

                if (amount > base.Balance)
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.NO_OVERDRAFT);
                }

                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
                base.Deposit(-amount, person);
            }

            public override void PrepareMonthlyReport()
            {
                double serviceCharge = base.transactions.Count * COST_PER_TRANSACTION;
                double interest = base.LowestBalance * INTEREST_RATE / 12;
                base.Balance += interest - serviceCharge;
                base.transactions.Clear();
            }
        }

        public class VisaAccount : Account
        {
            private static readonly double INTEREST_RATE = 0.1995;
            private double creditLimit;

            public VisaAccount(double balance = 0, double creditLimit = 1200) : base("VS-", balance)
            {
                this.creditLimit = creditLimit;
            }

            public void DoPayment(double amount, Person person)
            {
                base.Deposit(amount, person);
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            }

            public void DoPurchase(double amount, Person person)
            {
                if (!base.IsUser(person.Name))
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);
                }

                if (!person.IsAuthenticated)
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
                }

                if (amount > (base.Balance + creditLimit))
                {
                    base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                    throw new AccountException(ExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
                }

                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
                base.Deposit(-amount, person);
            }

            public override void PrepareMonthlyReport()
            {
                double interest = base.LowestBalance * INTEREST_RATE / 12;
                base.Balance -= interest;
                base.transactions.Clear();
            }
        }

        public static class Bank
        {
            // Fields HOI
            public static readonly Dictionary<string, Account> ACCOUNTS = new Dictionary<string, Account>();
            public static readonly Dictionary<string, Person> USERS = new Dictionary<string, Person>();

            // Static constructor to initialize the USERS and ACCOUNTS collections
            static Bank()
            {
                //initialize the USERS collection
                AddPerson("Narendra", "1234-5678");    //0
                AddPerson("Ilia", "2345-6789");        //1
                AddPerson("Mehrdad", "3456-7890");     //2
                AddPerson("Vijay", "4567-8901");       //3
                AddPerson("Arben", "5678-9012");       //4
                AddPerson("Patrick", "6789-0123");     //5
                AddPerson("Yin", "7890-1234");         //6
                AddPerson("Hao", "8901-2345");         //7
                AddPerson("Jake", "9012-3456");        //8
                AddPerson("Mayy", "1224-5678");        //9
                AddPerson("Nicoletta", "2344-6789");   //10


                //initialize the ACCOUNTS collection
                AddAccount(new VisaAccount());              //VS-100000
                AddAccount(new VisaAccount(150, -500));     //VS-100001
                AddAccount(new SavingAccount(5000));        //SV-100002
                AddAccount(new SavingAccount());            //SV-100003
                AddAccount(new CheckingAccount(2000));      //CK-100004
                AddAccount(new CheckingAccount(1500, true));//CK-100005
                AddAccount(new VisaAccount(50, -550));      //VS-100006
                AddAccount(new SavingAccount(1000));        //SV-100007 

                //associate users with accounts
                string number = "VS-100000";
                AddUserToAccount(number, "Narendra");
                AddUserToAccount(number, "Ilia");
                AddUserToAccount(number, "Mehrdad");

                number = "VS-100001";
                AddUserToAccount(number, "Vijay");
                AddUserToAccount(number, "Arben");
                AddUserToAccount(number, "Patrick");

                number = "SV-100002";
                AddUserToAccount(number, "Yin");
                AddUserToAccount(number, "Hao");
                AddUserToAccount(number, "Jake");

                number = "SV-100003";
                AddUserToAccount(number, "Mayy");
                AddUserToAccount(number, "Nicoletta");

                number = "CK-100004";
                AddUserToAccount(number, "Mehrdad");
                AddUserToAccount(number, "Arben");
                AddUserToAccount(number, "Yin");

                number = "CK-100005";
                AddUserToAccount(number, "Jake");
                AddUserToAccount(number, "Nicoletta");

                number = "VS-100006";
                AddUserToAccount(number, "Ilia");
                AddUserToAccount(number, "Vijay");

                number = "SV-100007";
                AddUserToAccount(number, "Patrick");
                AddUserToAccount(number, "Hao");

            }


            // Methods
            public static void PrintAccounts()
            {
                foreach (KeyValuePair<string, Account> account in ACCOUNTS)
                {
                    Console.WriteLine(account.Value);
                }
            }

            public static void PrintUsers()
            {
                foreach (KeyValuePair<string, Person> user in USERS)
                {
                    Console.WriteLine(user.Value);
                }
            }

            public static Person GetPerson(string name)
            {
                if (USERS.ContainsKey(name))
                {
                    return USERS[name];
                }
                else
                {
                    throw new AccountException(ExceptionType.ACCOUNT_DOES_NOT_EXIST);
                }
            }

            public static Account GetAccount(string number)
            {
                if (ACCOUNTS.ContainsKey(number))
                {
                    return ACCOUNTS[number];
                }
                else
                {
                    throw new AccountException(ExceptionType.ACCOUNT_DOES_NOT_EXIST);
                }
            }

            public static void AddPerson(string name, string sin)
            {
                Person newPerson = new Person(name, sin);
                newPerson.OnLogin += Logger.LoginHandler;

                USERS.Add(name, newPerson);
            }

            public static void AddAccount(Account account)
            {
                account.OnTransaction += Logger.TransactionHandler;

                ACCOUNTS.Add(account.Number, account);
            }

            public static void AddUserToAccount(string number, string name)
            {
                Account account = GetAccount(number);
                Person person = GetPerson(name);

                account.AddUser(person);
            }

            public static List<Transaction> GetAllTransactions()
            {
                List<Transaction> allTransactions = new List<Transaction>();

                foreach (KeyValuePair<string, Account> account in ACCOUNTS)
                {
                    allTransactions.AddRange(account.Value.transactions);
                }

                return allTransactions;
            }

        }

    }

}
