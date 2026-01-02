using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Console = System.Console;

using DataBase ; 
using Models;


      public class Auth

{
          public static bool AdminLogin()
    {
            Console.WriteLine ( " Logging in as an Admin ......") ; 
            return true ;
    }




         public static Card CardHolderLogin(string UserType ) // we are taking the card object from the card number -< userINP ///TO BE HANDLED IN MAIN !!!! ((iMPLEMENT LATERR))

    {
        if (UserType =="1")
         {UserType = "student" ;}
        else if (UserType =="2")
         {UserType ="faculty member" ;}
        else 
         {Console.WriteLine("Invalid User Type") ; return null ; }



           Console.WriteLine ("\n---" + UserType +" Login ---\n" ) ; 

           Console.Write( "Enter Card Number :"); 
           string CN_INP = Console.ReadLine( ) ; 
            
              Card CurrentCard = null ; 
               bool found = false ; 
            foreach (Card c in Database.Cards)

        {
            if ( c.CardNumber == CN_INP )
              { CurrentCard = c ; 
               found = true ; 
               break ;
               }                         // Perfromanceeeee baby 

        }          
         if ( found==false  )
         { Console.WriteLine ( "Invalid Card Number or Card not Found ") ;
             return null; 
         
         
          }
            
            if ( CurrentCard.Status == "blocked")
        {
            Console.WriteLine("Card is Blocked") ; 
            return null ; 
        }
           else if (UserType != CurrentCard.Type)
        {
            Console.WriteLine ("you are not a : " +CurrentCard.Type + " Logged out Successfully ! ") ; 
            return null ; 
        }
        else 

             return CurrentCard ; 
          

    }












}