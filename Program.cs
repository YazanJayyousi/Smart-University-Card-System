using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

using DataBase ; 
using Models ;



  public class Program
{
     static void Main ()
    {
        
        Database.Initialize() ; 



         bool System_On = true ; 
        while ( System_On )

        {
             Console.WriteLine("SMART UNIVERSITY CARD SYSTEM"); 
             Console.WriteLine("-------------------------------"); 

             Console.WriteLine("1. Login As an Administrator ");
             Console.WriteLine("2. Login As a Card Holder "); 
             Console.WriteLine ("3. Exit System ") ;
 
              string LoginChoice = Console.ReadLine ();

             if (LoginChoice == "1")
            {
              if (  Auth.AdminLogin() )
                {
                    bool AdminLogged = true ;
                    while ( AdminLogged)
                    {
                    Console.WriteLine ("\n---Admin Services---\n");
                    Console.WriteLine("1. Issue Card ");
                    Console.WriteLine("2. Block Card ");
                    Console.WriteLine("3. Unblock Card ");
                    Console.WriteLine("4. View all cards ");
                    Console.WriteLine("5. View all transactions  ");
                    Console.WriteLine("6. Logout  "); 
                    
                    
                     string ASC = Console.ReadLine() ;    //ASC = ADdmin Service Choice 
                      
                     switch ( ASC )
                    {
                        case "1" : AdminService.IssueCard() ;  break ; 
                        case "2" : AdminService.BlockCard() ;  break ; 
                        case "3" : AdminService.UnblockCard() ; break ; 
                        case "4" : AdminService.ViewAllCards() ; break; 
                        case "5" : AdminService.ViewALLTxns() ; break ;
                        case "6" :AdminLogged =false ; break ; //repeats loop  to login screen  

                        default : Console.WriteLine("Invalid Service Choice") ; break ; 
                    }
                    }
                } 
                
            }
             else if (LoginChoice == "2")
            {
                Console.WriteLine("Choose User Type :") ;
              Console.WriteLine("1. Student ");
              Console.WriteLine("2. Faculty Member ");

              string UT = Console.ReadLine() ; 
                 Card  CurrentCardHolder = Auth.CardHolderLogin(UT ); 

                if (CurrentCardHolder == null ) {continue ; }   // Exits (PRINT OF ERROR FROM AUTH FUN)

                    if (CurrentCardHolder.Type == "student")
                     {
                        bool StudentLogged =true ; 
                        while (StudentLogged)
                        {
                         Console.WriteLine("Student Services") ; 
                         Console.WriteLine("-----------------"); 
                         Console.WriteLine("1. Recharge card");
                         Console.WriteLine("2. Record lecture attendance");
                         Console.WriteLine("3. Pay for cafeteria");
                         Console.WriteLine("4. Pay for bus ride");
                         Console.WriteLine("5. View transaction history");
                         Console.WriteLine("6. Logout  ");

                         string SSC = Console.ReadLine(); //SSC =student service choice 

                         switch (SSC)
                         {
                             case "1" : StudentService.RechargeCard(CurrentCardHolder) ; break;
                             case "2" : StudentService.RecordAttendance(CurrentCardHolder) ; break;
                             case "3" : StudentService.PayForCafeteria(CurrentCardHolder) ; break;
                             case "4" : StudentService.PayForBus(CurrentCardHolder) ;break;
                             case "5" : StudentService.ViewTxnHistory(CurrentCardHolder);break;
                             case "6" : StudentLogged =false; break ;  

                             default : Console.WriteLine("Invalid Service "); break;
                         }
                        }
                     }
                     else if (CurrentCardHolder.Type == "faculty member" )
                     {
                        bool FacultyLogged = true ;
                        while ( FacultyLogged)
                        {
                         Console.WriteLine("Faculty Member Services") ; 
                         Console.WriteLine("-----------------"); 
                         Console.WriteLine("1. Recharge card");
                         Console.WriteLine("2. Access car parking ");
                         Console.WriteLine("3. Generate attendance report");
                         Console.WriteLine("4. Logout ");

                         string FSC = Console.ReadLine() ;

                         switch (FSC)
                         {
                            case "1" : FacultyService.RechargeCard_F(CurrentCardHolder) ;break;
                            case "2" : FacultyService.AccessCarParking(CurrentCardHolder); break;
                            case "3" : FacultyService.Gen_attendance_rep(CurrentCardHolder);break;
                            case "4" : FacultyLogged=false ; break ;  

                            default : Console.WriteLine("Invalid Service "); break;
                        
                         }
                        }






                     }



                

















            }
            else if (LoginChoice == "3") {System_On = false ; }
            else
            {
                
              
             Console.WriteLine("Invalid Choice") ; 

            }

           







        }










    }



}

 

    