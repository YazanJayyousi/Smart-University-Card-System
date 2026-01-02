using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Console = System.Console;

using DataBase;
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
                 TransactionType = "recharge" ,
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
        TransactionType = "attendance",
        Amount = "N/A",
    };
    Database.Transactions.Add(txn);
    Database.SaveTransactions();
    }



       




       public static void PayForCafeteria( Card card )
    {
        Console.WriteLine( "\n[CAFETERIA MENU]\n"); 

        List<Menu> CafeteriaMenu =  Menu.GetMenu() ;  //Fetches Menu from Menu model (STATIC)



int counter = 1 ; 
        foreach ( Menu m in CafeteriaMenu)
        {
            Console.WriteLine(counter + ". Item:" + m.Item + " Price: " + m.Price) ;
            counter++;
        }
        Console.WriteLine( "0. Finish Order ") ;


                               decimal total = 0;
bool ordering = true ;
          while ( ordering) 
        {
         Console.Write("Enter Item Number :")  ;
         string Item_NO = Console.ReadLine() ;
         int itemIndex ; // to track index of menu 

         if (Item_NO == "0")
          {
            if ( total ==0 ) { Console.WriteLine(" No items selected. Order cancelled.");  break ; }

             else if ( total <= card.Balance)
                {
                   card.Balance -= total ;
                   Database.SaveCards() ; 
                   Console.WriteLine ( "Enter Transaction ID : ") ;
                     string txnID = Console.ReadLine() ;

                   Console.WriteLine ( " Payment Successful of amount : " + total ) ;  

                   Transactions txn = new Transactions
                   {
                       TransactionId = txnID ,
                          CardNumber = card.CardNumber ,
                            TransactionType = "payment" ,
                                Amount = total.ToString() // Defined as string in model

                   };
                     Database.Transactions.Add(txn) ;
                        Database.SaveTransactions() ;
                        ordering = false ;
                       
                     
                }
           else if  ( total > card.Balance)
             {
                Console.WriteLine(" Insufficient Balance , Payment Failed") ;
           
                break ;
             } 

          }


                 
         else if ( int.TryParse(Item_NO , out itemIndex ) &&  itemIndex <= CafeteriaMenu.Count && itemIndex > 0)
            {
                               
               int Index = itemIndex -1 ; // array index fix
               Menu SelectedItem = CafeteriaMenu[ Index]  ; 
                 total = total + SelectedItem.Price ;
                   
                   Console.WriteLine(" Item Added , Total Amount :" + total ) ; 

            }

            else 
            {
                Console.WriteLine(" Invalid Item Number ") ; 
            }
                   




        }

      

        














    }
       
       public static void PayForBus( Card card )
         {
                       Console.WriteLine ( "\n--- Bus Ride ---\n") ;

                       List <Track> busTracks = Track.GetTracks();    
                       
                       int counter  =1 ; 
                       foreach ( Track t in busTracks ) 
                       {
                        Console.WriteLine("Track "+ counter +"\n-->" + "Origin: " +t.Origin+ "   Destination: " + t.Destination + "   Cost: " + t.Cost ) ;
                        counter++ ;
                       }

                       Console.Write("Enter Track Number : "); 
                      string track_NO = Console.ReadLine() ; 
                      int trackIndex ; 
                       
                       int.TryParse(track_NO , out trackIndex ); 
                       if ( trackIndex <= busTracks.Count && trackIndex >0)
                       {
                            int index = trackIndex -1 ;
                            Track selectedTrack = busTracks[index];
                                  decimal cost = selectedTrack.Cost ;
                                  if ( cost <= card.Balance)
                                  {
                                      card.Balance -= cost ; 
                                      Database.SaveCards() ; 

                                      Console.Write( "Enter Transaction ID : ");
                                      string txnID = Console.ReadLine() ; 
                                  
                                      Transactions txn = new Transactions
                                      {
                                        TransactionId = txnID , 
                                         CardNumber = card.CardNumber ,
                                          TransactionType = "payment" ,
                                         Amount = cost 

                                      };
                                      Database.Transactions.Add(txn) ; 
                                      Database.SaveTransactions() ; 



                                  }
                                  else if ( cost > card.Balance )
                                  {
                                     Console.WriteLine ( "Insufficiant Balance !") ; 
                                     return ; 
                                  }
                            
                       }
                       else {
                                Console.WriteLine (" invalid Track or Does not Exist " ); 

                       }
                       
            



         } 



       public static void ViewTxnHistory (Card card)
         {
            
            Console.WriteLine("\n--- Transaction History ---\n") ;

             List<Transactions> Current = new List<Transactions> () ;   /// so we sort a little amount rather than full DB txns to improve performance 
                   foreach (Transactions t in Database.Transactions)
                   {
                     if (card.CardNumber == t.CardNumber)
                     { Current.Add(t); }
                   }
                     
                     if (Current.Count == 0) { Console.WriteLine("No transaction history found."); return;}

              Current.Sort((x, y) => x.TransactionType.CompareTo(y.TransactionType));          // lambda expression considers XY TXN objects auto in Sort fun 

                foreach ( Transactions t in Current)
                 {
                     Console.WriteLine (" Type : " + t.TransactionType + " \n|Transaction ID: " + t.TransactionId + "\n|Amount : " + t.Amount ) ;

                 }


         }


        
      










}
