using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShopbyDerrick
{
    class Program
    {
        static void Main(string[] args)
        {
            BankBranch DBS = new BankBranch("DBS Clementi Brach", "Derrick");
            Customer a = new Customer("Htein", "Clementi", new DateTime(1994, 5, 1));
            Customer b = new Customer("Waddy", "Buorna Vista", new DateTime(1994, 11, 23));
            Customer c = new Customer("Terry", "Clark Query", new DateTime(1991, 6, 12));

            SavingAcc sa = new SavingAcc("010-220-333", a, 5000);
            CurrentAcc ca = new CurrentAcc("010-2210-323", a, 6000);
            try
            {
                sa.Withdraw(2000);
                sa.TransferTo(ca,4000);
            }
            catch (InsufficientBalanceException e)
            {
                Console.WriteLine(e.Message);
            }

            


            //DBS.AddAccount(new SavingAcc("010-220-333", a, 5000));
            //DBS.AddAccount(new OverDraftAcc("010-220-333", a, 5000));
            //DBS.AddAccount(new OverDraftAcc("010-111-555", b, 5000));
            //DBS.AddAccount(new CurrentAcc("010-123-345", c, 5000));
            //DBS.AddAccount(new CurrentAcc("010-123-345", b, 5000));

            //DBS.PrintCustomers();
            //DBS.PrintBankAccount();

            //Console.WriteLine(DBS.TotalInterestEarned());
            //Console.WriteLine(DBS.TotalInterestPaid());

            //DBS.CalculateInterest();
            //DBS.PrintBankAccount();



        }
    }
    class Customer
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private DateTime dob;

        public DateTime Date
        {
            get { return dob; }
            set { dob = value; }
        }

        public Customer() : this("Name", "Address", new DateTime(1994, 5, 1))
        { }

        public Customer(string name, string address, DateTime dob)
        {

            this.name = name;
            this.address = address;
            this.dob = dob;
        }

        public int GetAge()
        {
            return (DateTime.Now.Year - dob.Year);
        }

        public override string ToString()
        {
            return string.Format("Name : {0} \t Address: {1}\t Date of Birth : {2}", Name, Address, Date);
        }


    }

    class BankBranch
    {
        public BankBranch()
        { }

        public BankBranch(string name, string manager)
        {
            this.name = name;
            this.manager = manager;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string manager;

        public string Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        List<BankAccount> acc = new List<BankAccount>();

        public void AddAccount(BankAccount acc)
        {
            this.acc.Add(acc);

        }

        public void PrintBankAccount()
        {
            foreach (BankAccount accs in acc)
            {
                Console.WriteLine(accs);
            }
        }
        public void PrintCustomers()
        {
            List<Customer> cust = new List<Customer>();
            for (int i = 0; i < acc.Count; i++)
            {
                BankAccount a = acc[i];
                Customer b = a.Owner;
                int c = cust.IndexOf(b);
                if (c < 0)
                {
                    cust.Add(b);
                }

            }
            foreach (Customer custs in cust)
            {
                Console.WriteLine(custs);
            }
        }

        public double TotalDeposit()
        {
            double total = 0;
            for (int i = 0; i < acc.Count; i++)
            {
                BankAccount a = acc[i];
                double bal = a.Balance;
                if (bal > 0)
                {
                    total += bal;
                }

            }
            return total;
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            for (int i = 0; i < acc.Count; i++)
            {
                BankAccount a = acc[i];
                double b = a.Interestrate();
                if (b > 0)
                    total += b;

            }


            return total;
        }

        public double TotalInterestEarned()
        {
            double total = 0;
            for (int i = 0; i < acc.Count; i++)
            {
                BankAccount a = acc[i];
                double b = a.Interestrate();
                if (b < 0)
                {
                    total += (-b);
                }

            }

            return total;
        }

        public void CalculateInterest()
        {
            for (int i = 0; i < acc.Count; i++)
            {
                BankAccount a = acc[i];
                a.Calculateinterest();
            }
        }





    }
    abstract class BankAccount
    {
        protected double interestrate = 0.0;
        private string accnumber;


        public string Accnumber
        {
            get { return accnumber; }
            set { accnumber = value; }
        }

        private Customer owner;

        public Customer Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected double balance;

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public BankAccount() : this("Name", new Customer(), 0.0)
        { }

        public BankAccount(string accnumber, Customer owner, double balance)
        {
            this.accnumber = accnumber;
            this.owner = owner;
            this.balance = balance;
        }

        public abstract double Interestrate();
        public abstract void Calculateinterest();

        public abstract void Deposit(double amount);

        public abstract void Withdraw(double amount);

        public abstract bool TransferTo(BankAccount acc, double amount);





    }
    class CurrentAcc : BankAccount
    {
        public override string ToString()
        {
            return string.Format("Current Account \nAccount Number: {0} \nAccount Holder: {1}\n Balance : {2} ", Accnumber, Owner.ToString(), Balance);
        }
        public CurrentAcc()
        { }

        public CurrentAcc(string accnumber, Customer owner, double balance) : base(accnumber, owner, balance)
        { }


        public override double Interestrate()
        {
            if (balance > 0)
            { interestrate = balance * 0.25 / 100; }
            else
            {
                Console.WriteLine("Balance is lower than minimum interestrate amount!");
            }
            return interestrate;
        }

        public override void Calculateinterest()
        {
            balance += interestrate;
        }

        public override void Deposit(double amount)
        {
            balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (amount > balance)
            {
                throw new InsufficientBalanceException("Your Balance is low");
            }

            balance -= amount;
            Console.WriteLine("Transition Successful");
        }

        public override bool TransferTo(BankAccount acc, double amount)
        {
            try
            {
                Withdraw(amount);
                acc.Deposit(amount);
                return true;
            }
            catch (InsufficientBalanceException e)
            {

                Console.WriteLine(e.Message);
            }

            return false; 
        }


    }

    class SavingAcc : BankAccount
    {
        public SavingAcc()
        { }

        public SavingAcc(string accnumber, Customer owner, double balance) : base(accnumber, owner, balance)
        { }
        public override double Interestrate()
        {
            if (balance > 0)
            { interestrate = balance * 1 / 100; }
            else
            {
                Console.WriteLine("Balance is lower than minimum interestrate amount!");
            }
            return interestrate;

        }

        public override void Calculateinterest()
        {
            balance += interestrate;
        }

        public override void Deposit(double amount)
        {
            balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (amount > balance)
            {
                throw new InsufficientBalanceException("Your Balance is Low");
            }

            balance -= amount;
            Console.WriteLine("Transition Successful");


        }

        public override bool TransferTo(BankAccount acc, double amount)
        {
            try
            {
                Withdraw(amount);
                acc.Deposit(amount);
                return true;
            }
            catch (InsufficientBalanceException e)
            {

                Console.WriteLine(e.Message);
            }

            return false;
            
        }


        public override string ToString()
        {
            return string.Format("Saving Account \nAccount Number: {0} \nAccount Holder: {1}\n Balance : {2} ", Accnumber, Owner.ToString(), Balance);
        }
    }
    class OverDraftAcc : BankAccount
    {
        public OverDraftAcc()
        { }

        public OverDraftAcc(string accnumber, Customer owner, double balance) : base(accnumber, owner, balance)
        { }

        public override double Interestrate()
        {
            if (balance > 0)
            { interestrate = balance * 0.25 / 100; }
            else
            {
                interestrate = balance * 6 / 100;
            }
            return interestrate;

        }

        public override void Calculateinterest()
        {
            balance += interestrate;
        }

        public override void Deposit(double amount)
        {
            balance += amount;
        }

        public override void Withdraw(double amount)
        {

            balance -= amount;

        }

        public override bool TransferTo(BankAccount acc, double amount)
        {

            acc.Deposit(amount);

            Console.WriteLine("Transition Successful!");
            return true;
        }
        public override string ToString()
        {
            return string.Format("OverDraft Account \nAccount Number: {0} \nAccount Holder: {1}\n Balance : {2} ", Accnumber, Owner.ToString(), Balance);
        }
    }

}

