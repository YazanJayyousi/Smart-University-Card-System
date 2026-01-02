using System;
using System.Collections.Generic;
using DataBase; 
using Models;

public class FacultyService
{
    public static void RechargeCard_F (Card card)
    {
        Console.WriteLine("\n--- Recharge Card ---");

        Console.WriteLine("Card Number : " + card.CardNumber);
        Console.WriteLine(" Balance : " + card.Balance);
        Console.WriteLine(" Type : " + card.Type);
        Console.WriteLine(" Status : " + card.Status);
        Console.WriteLine(" UserId : " + card.UserId);
        Console.WriteLine("----------------------------");

        Console.Write(" Enter amount to recharge: ");
        string amount_INP = Console.ReadLine();
        decimal amount;
        bool isValid = decimal.TryParse(amount_INP , out amount ); 

        if (isValid && amount > 0)
        {
             decimal oldBalance = card.Balance; 
             card.Balance = card.Balance + amount; 

             Console.WriteLine("Old Balance : " + oldBalance);
             Console.WriteLine("New Balance : " + card.Balance);

             Console.WriteLine("Enter Transaction ID : ");
             string txnID = Console.ReadLine();

             Transactions txn = new Transactions
             {
                 TransactionId = txnID ,
                 CardNumber = card.CardNumber ,
                 TransactionType = "recharge" ,
                 Amount = amount.ToString()
             };

             Database.Transactions.Add(txn);
             Database.SaveTransactions();
             Database.SaveCards();
        }
        else
        {
            Console.WriteLine("Invalid Amount");
        }
    }

    public static void AccessCarParking (Card card)
    {
        Console.WriteLine("\n--- Car Parking ---");
        Console.WriteLine("1st hour: 5 JD");
        Console.WriteLine("2nd hour: 4 JD");
        Console.WriteLine("3rd hour: 3 JD");
        Console.WriteLine("4th hour: 2 JD");
        Console.WriteLine("5th hour: 1 JD");
        Console.WriteLine("Above 5 hours for free.");
        Console.WriteLine("----------------------------");

        Console.Write(" number of hours: ");
        string hours_INP = Console.ReadLine();
        decimal parking_hours;
        bool isValid = decimal.TryParse(hours_INP , out parking_hours);

        if (isValid && parking_hours > 0)
        {
            decimal fees = 0;

                 
            if (parking_hours <= 1)
            {
                fees = 5;
            }
            else if (parking_hours <= 2)
            {
                fees = 9; 
            }
            else if (parking_hours <= 3)
            {
                fees = 12;
            }
            else if (parking_hours <= 4)
            {
                fees = 14; 
            }
            else
            {
                fees = 15; 
            }

            Console.WriteLine("--Your parking fees is " + fees + " JD");

            if (card.Balance >= fees)
            {
                card.Balance = card.Balance - fees;
                Database.SaveCards();

                Console.WriteLine("Enter Transaction ID : ");
                string txnID = Console.ReadLine();

                Transactions txn = new Transactions
                {
                    TransactionId = txnID ,
                    CardNumber = card.CardNumber ,
                    TransactionType = "payment" ,
                    Amount = fees.ToString()
                };

                Database.Transactions.Add(txn);
                Database.SaveTransactions();

            }
            else
            {
                Console.WriteLine("Insufficient Balance!");
            }
        }
        else
        {
            Console.WriteLine("Invalid Choice");
        }
    }

    public static void Gen_attendance_rep (Card card)
    {
        Console.WriteLine("\n--- Generate Attendance Report ---");

        FacultyMember currentFaculty = null;
        foreach (FacultyMember f in Database.FacultyMembers)
        {
            if (f.UserId == card.UserId)
            {
                currentFaculty = f;
                break;
            }
        }

        if (currentFaculty == null) 
        { 
            Console.WriteLine("Faculty not found"); 
            return; 
        }

        Console.WriteLine(" Taught Courses: ");
        int counter = 1;
        foreach (string course in currentFaculty.TaughtCourses)
        {
            Console.WriteLine(counter + ": " + course);
            counter++;
        }

        Console.Write(" Enter Course ID: ");
        string Course_ID_REP = Console.ReadLine();

        Console.Write(" Enter Date: ");
        string Date_REP = Console.ReadLine();

        Console.WriteLine("\n--- Attendees List ---");

        bool found = false;

        foreach (Attendance att in Database.AttendanceRecords)
        {
            if (att.Date == Date_REP && att.CourseId == Course_ID_REP)
            {
                foreach (string studentID in att.Attendees)
                {
                    Console.WriteLine("Student ID: " + studentID);
                }
                found = true;
            }
        }

        if (found == false)
        {
            Console.WriteLine("No attendance records found.");
        }
    }
}
