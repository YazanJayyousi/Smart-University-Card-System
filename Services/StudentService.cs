using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Console = System.Console;

using Models;

public class StudentService
{
    public static void RechargeCard (Card card)
    {
        
       Console.WriteLine("\n--- Recharge Card ---");
       
       Console.WriteLine($"Card Number  : {card.CardNumber}") ;

        Console.WriteLine($" Balance : {card.Balance}") ;
        Console.WriteLine($" Type : {card.Type}") ;
        Console.WriteLine($" Status : {card.Status}") ;
        Console.WriteLine($" UserId : {card.UserId}") ;
        Console.WriteLine("----------------------------") ;

        Console.Write(" Enter amount to recharge: ");
        string amount_INP= Console.ReadLine();
        decimal amount;
        bool isValid = decimal.TryParse(amount_INP , out amount ); // saved in amount if VALID 

        if ( isValid && amount > 0 )
        {
           decimal oldBalance = card.Balance ; ////// for displayy  ----------------
                                                                                   //
               card.Balance = card.Balance + amount ;                              //
               Console.WriteLine ($"Old Balance : {oldBalance}");   // <--------   //
               Console.WriteLine ($"New Balance : {card.Balance}") ; 

               Console.WriteLine("Enter Transaction ID : "); 
             string txnID = Console.ReadLine(); 

             Transactions txn = new Transactions 
             {
                 TransactionId = txnID , 
                 CardNumber = card.CardNumber ,
                 TransactionType = "Recharge" ,
                 Amount = amount.ToString() 
             };

                Database.Transactions.Add(txn); 
                Database.SaveTransactions();
                Database.SaveCards();     // added this to save updated card balance as its lost if not saved
        }
         else 
          {   Console.WriteLine ( "Invalid Amount ") ;}

    }

    public static void RecordAttendance(Card card) // - fix: used card not Student parameter for CardNo in Transaction Save 
    {
        Console.WriteLine("\n--- Record lecture attendance ---");
       
       //DataBase Search : 

          Student CurrentSTD = null ; //ref 

        foreach (Student s in Database.Students )
        {
            if ( s.UserId == card.UserId)
            {
                CurrentSTD = s ; 
                break ; 
            }

            

        }

        if (CurrentSTD == null) return ;
        
        Console.WriteLine( " Your Registered Courses : ") ;

int counter= 1;
         foreach (string course in CurrentSTD.RegisteredCourses)
        {

            Console.WriteLine(counter +": "+ course ) ; 
             counter++ ;
        }

          Console.Write(" Enter course ID: ") ; 
          string Course_ID_ATT = Console.ReadLine( ) ; 
            
           Console.Write(" Enter Date: ") ;  
           string Date_ATT = Console.ReadLine() ; 

           Attendance foundRecord = null; //reff updated to avoid the NullException error after test 

        
        foreach (Attendance att in Database.AttendanceRecords)
       {
        if (att.Date == Date_ATT && att.CourseId == Course_ID_ATT)
        {
            foundRecord = att;
            break; 
        }
          }
          
          if (foundRecord != null)
    {
        foundRecord.Attendees.Add(card.UserId);
    }
    else
    {
        Attendance newAtt = new Attendance
        {
            CourseId = Course_ID_ATT,
            Date = Date_ATT,
            Attendees = new List<string>()
        };
        newAtt.Attendees.Add(card.UserId);
        Database.AttendanceRecords.Add(newAtt);
    }

    Database.SaveAttendance();

    Console.Write("Enter Transaction ID: ");
    string txnID = Console.ReadLine();

    Transactions txn = new Transactions
    {
        TransactionId = txnID,
        CardNumber = card.CardNumber,
        TransactionType = "Attendance",
        Amount = "N/A",
    };
    Database.Transactions.Add(txn);
    Database.SaveTransactions();
}







}
