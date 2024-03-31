using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace MortgageCalculator
{
    //Mortgage calcualtor class
    public class MortgageCalculator
    {

        //A constant number of months
        const int numMonths = 12;

        private double loan_amount;
        private double annual_interestRate;
        private int loan_termYears;


        //Constructor for Mortgage class
        public MortgageCalculator(double loan_amount, double annual_interestRate, int loan_termYears)
        {
            this.loan_amount = loan_amount;
            this.annual_interestRate = annual_interestRate;
            this.loan_termYears = loan_termYears;
        }

        //Monthly payment method
        public double CalculateMonthlyRepayment(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            //Converting annual interest rate to monthly interest rate
            double monthlyInterest = (annualInterestRate / 100) / numMonths;

            //Converting number of years to months 
            double numOfPayments = loanTermYears * numMonths;

            //Payment formula: M=P(r(1+r)^n)/(1+r)^n - 1

            double numerator = loanAmount * (monthlyInterest * (Math.Pow(1 + monthlyInterest, numOfPayments)));
            double denominator = Math.Pow(1 + monthlyInterest, numOfPayments) - 1;

            double monthlyRepayment = numerator / denominator;
            Console.WriteLine($"Your monthly repayment amount is: {monthlyRepayment:C}");
            return monthlyRepayment;

        }

        //Total interest method
        public double CalculateTotalInterestPaid(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            //Creating monthly payments and number of months
            double monthlyPayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            int numOfPayments = loanTermYears * numMonths;

            //Calculating paid amount of all months
            double totalPayments = numOfPayments * monthlyPayment;

            //Calculating the difference between paid and loan amount
            double totalInterestPaid = totalPayments - loanAmount;

            Console.WriteLine($"Total interest paid over the loan term is: {totalInterestPaid:C}");
            return totalInterestPaid;
        }

        //Total amount paid method
        public double CalculateTotalAmountPaid(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            //Creating monthly payments and number of months 
            double monthlyPayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            int numOfPayments = loanTermYears * numMonths;

            //Total amount paid: Multiplication of the number of months and monthly payment. 
            double totalAmountPaid = numOfPayments * monthlyPayment;

            Console.WriteLine($"Total amount paid over the loan term is: {totalAmountPaid:C}");
            return totalAmountPaid;
        }

        // Class to represent each entry in the amortization schedule
        public class Payment_Details
        {
            public int PaymentNumber { get; set; }
            public double PaymentAmount { get; set; }
            public double InterestPaid { get; set; }
            public double PrincipalPaid { get; set; }
            public double RemainingBalance { get; set; }
        }

        //Amortization method
        public List<Payment_Details> GenerateAmortizationSchedule(double loanAmount, double annualInterestRate, int loanTermYears)
        {
            //Creating monthly payments and number of months 
            double monthlyPayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
            int numOfPayments = loanTermYears * numMonths;

            double remaining_balance = loanAmount;

            //Creating a list of payment details. 
            var amortization = new List<Payment_Details>();
            for (int i = 0; i < numOfPayments; i++)
            {
                double interestPayment = remaining_balance * (annualInterestRate / 100) / numMonths;
                double principalAmount = monthlyPayment - interestPayment;
                remaining_balance = remaining_balance - principalAmount;

                //Addiding Payment_Details information to amortization variable
                amortization.Add(new Payment_Details
                {
                    PaymentNumber = i,
                    PaymentAmount = monthlyPayment,
                    InterestPaid = interestPayment,
                    PrincipalPaid = principalAmount,
                    RemainingBalance = remaining_balance
                });

            }

            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}", "Payment #", "Payment", "Interest", "Principal", "Balance");

            foreach (var entry in amortization)
            {
                Console.WriteLine("{0,-15}{1,-15:C}{2,-15:C}{3,-15:C}{4,-15:C}", entry.PaymentNumber, entry.PaymentAmount, entry.InterestPaid, entry.PrincipalPaid, entry.RemainingBalance);
            }

            return amortization;

        }

        public class Program
        {
            static void Main(string[] args)
            {
                // Create an instance of MortgageCalculator        
                Console.WriteLine("Welcome to the Mortgage calculator");
                Console.WriteLine("1. Start");
                Console.WriteLine("2. Exit");
                string ans = Console.ReadLine();

                if (ans == "1")
                {
                    //  prompt the user for loan details
                    Console.WriteLine("How much is your total loan amount?");
                    double loan_amount = Convert.ToDouble(Console.ReadLine());


                    Console.WriteLine("What is your annual interest rate?");
                    double annual_interestRate = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("What is the term of the loan in years?");
                    int loan_termYears = Convert.ToInt32(Console.ReadLine());

                    //Creating an object obtaing mortgage calculator class
                    MortgageCalculator calculator = new MortgageCalculator(loan_amount, annual_interestRate, loan_termYears);


                    void main2()
                    {
                        //Menu for loan details
                        Console.WriteLine("What would you like to do next?");
                        Console.WriteLine("1. Check Monthly Repayment");
                        Console.WriteLine("2. Check Total Interest Paid");
                        Console.WriteLine("3. Check Total Amount Paid");
                        Console.WriteLine("4. Generate Amortization Schedule");
                        Console.WriteLine("5. Exit");
                        string ans1 = Console.ReadLine();

                        switch (ans1)
                        {
                            case "1":
                                calculator.CalculateMonthlyRepayment(loan_amount, annual_interestRate, loan_termYears);
                                Console.WriteLine();
                                main2();
                                break;
                            case "2":
                                calculator.CalculateTotalInterestPaid(loan_amount, annual_interestRate, loan_termYears);
                                Console.WriteLine();
                                main2();
                                break;
                            case "3":
                                calculator.CalculateTotalAmountPaid(loan_amount, annual_interestRate, loan_termYears);
                                Console.WriteLine();
                                main2();
                                break;
                            case "4":

                                calculator.GenerateAmortizationSchedule(loan_amount, annual_interestRate, loan_termYears);
                                Console.WriteLine();
                                main2();
                                break;
                            case "5":
                                System.Environment.Exit(0);
                                Console.WriteLine("Thank you");
                                break;
                            default:

                                Console.WriteLine("Incorrect Input, Try again.");
                                main2();
                                break;
                        }


                    }

                    main2();



                }
                else if (ans == "2")
                {
                    // Exit the program
                    Console.WriteLine("Thank you. Have a great day");
                    System.Environment.Exit(0);

                }
                else
                {
                    // Prompt for correct input
                    Console.WriteLine("Incorrect Input, Try again.");
                    Main(args);
                }
            }




        }
    }
}