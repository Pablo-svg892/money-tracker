using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker
{
    internal class Program
    {
        //Enum for transaction types
        enum TransactionType
        {
            Income,
            Expense,
        }

        //Enum for alla categories
        enum Category
        {
            Rent,
            Groceries,
            Utilities,
            Entertainment,
            Transportation,
            Other,

        }


        class Transaction
        {
            // Class defines the structure of each record
            public string StudentName { get; set; } = "anonymous"; // Optional name
            public TransactionType Type { get; set; } // Enum for Income or Expense
            public Category Category { get; set; } // Used enum for the tupe of catergory (Rent, Groceries, etc.)
            public double Amount { get; set; } // Value of transaction
            public DateTime Timestamp { get; set; } // Date and time
        }



        static void Main(string[] args)
        {
            // use Dynamic list to store transactions
            List<Transaction> transactions = new List<Transaction>();

            // variables to track overall totals 
            double totalIncome = 0;
            double totalExpenses = 0;


           /*------------------------------------------------------
            main program loop and it runs until theuser chooses exit(false)
            -----------------------------------------------------------*/
            Console.WriteLine("\n--- Welcome to your Budget Tracker!! ---");
            while (true) // Main menu loop
            {
                Console.WriteLine("\n--- Budget Tracker Menu ---");
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. Add Expense");
                Console.WriteLine("3. View Summary");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                /* ------------------------------------------------------------------------------
                  IF STATEMENT THAT HANDELS choice 1 or choice2 of adding an income or an expense
                 --------------------------------------------------------------------------------*/

                if (choice == "1" || choice == "2")
                {
                    Console.Write("Enter your name (or press Enter to skip): ");  // ask for student name ( optional)   
                    string name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name)) name = "anonymous";   // defauits to anonymous if no name is provided

                    // asks for catergory that must match the enum values (Rent, Groceries, etc.)
                    Console.WriteLine("Choose category: Rent, Groceries, Utilities, Entertainment,Transportation, Other");
                    string categoryInput = Console.ReadLine();

                    // Try to parse/ convert user input to a valid category  enum
                    if (!Enum.TryParse(categoryInput, true, out Category category))
                    {
                        Console.WriteLine("Error: Invalid category.");
                        continue; 
                    }
                    // prompt the user for the amount and validate that it is a positive number
                    Console.Write("Enter amount: ");
                    string amountInput = Console.ReadLine();
                    if (!double.TryParse(amountInput, out double amount) || amount <= 0) // 'out double amount' means if the conversion succeeds
                                                                                         // then store the results in amount
                    {
                        Console.WriteLine("Error: Amount must be a positive number.");
                        continue;   // skip invalid input
                    }

                    // Determine transaction type using enum
                    TransactionType type = (choice == "1") ? TransactionType.Income : TransactionType.Expense;

                    /* -----------------------------------------------------------------------------
                     * Create new transaction object with the information we have gotten for the user
                     * ------------------------------------------------------------------------------*/
                    Transaction newTransaction = new Transaction
                    {
                        StudentName = name,
                        Type = type,
                        Category = category,
                        Amount = amount,
                        Timestamp = DateTime.Now
                    };

                    // Add transaction to the list
                    transactions.Add(newTransaction);

                    // Update totals
                    //---------------
                    if (type == TransactionType.Income)
                    {
                        totalIncome += amount;
                    }
                    else
                    {
                        totalExpenses += amount;
                    }

                    Console.WriteLine($"{type} transaction added successfully.");
                }
                /* -----------------------------------------
                 view summary option
                -------------------------------------------*/
                else if (choice == "3")
                {
                    Console.WriteLine("\n--- Transaction Details ---");

                    // loop thriugh all transactionsin the list
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"{transaction.Category}: {transaction.Amount} ({transaction.Type} by {transaction.StudentName} on {transaction.Timestamp})");
                    }

                    // calulate and display totals and balance
                    double balance = totalIncome - totalExpenses;
                    Console.WriteLine($"\nTotal Income: {totalIncome}");
                    Console.WriteLine($"Total Expenses: {totalExpenses}");
                    Console.WriteLine($"Balance: {balance}");
                }
                /* -------------------
                 * exit option
                 ----------------------*/
                else if (choice == "4")
                {
                    Console.WriteLine("Exiting program...");
                    break; // exit the loop and end the program
                }
                /* --------------------------------------
                 * handle invalid menu options
                 --------------------------------------*/
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");



                }



            }
        }
    }
}