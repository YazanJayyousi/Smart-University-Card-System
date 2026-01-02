using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Console = System.Console;

using DataBase ; 
using Models;

   public class AdminService
{


         public static void IssueCard( )
    {
          Console.WriteLine ("\n--- Issue Card ---\n") ; 

          Console.Write ("Enter Card Number : ") ; 
          string newCardNo = Console.ReadLine () ; 

          Console.Write ("Enter Card Type : ") ; 
         string newCardType = Console.ReadLine () ; 
            
        Console.Write ("Enter User ID : ") ; 
         string uI = Console.ReadLine () ; 

           foreach (Card c in Database.Cards )
        {
            if ( newCardNo == c.CardNumber )
             { Console.WriteLine ("Card Already Exists !") ; return;   }
        }
         Card newCard = new Card
         {
               CardNumber = newCardNo ,
               Balance = 50 , 
               Type = newCardType , 
               Status = "unblocked" , 
               UserId = uI 
         };
         
            Database.Cards.Add (newCard) ; 
            Database.SaveCards() ; 
              
              Console.WriteLine (" Card issued Successfully ");


    }    



   public static void BlockCard()
    {
        
        Console.WriteLine("\n---Block Card---\n") ;
          
          foreach (Card c in Database.Cards)
        {
            if ( c.Status == "unblocked")
            {
                Console.WriteLine("Card Number: " + c.CardNumber + " Balance : "+c.Balance +" Type: " +c.Type +" Status: " +c.Status + " User ID : " +c.UserId) ;
            }


        }
           
           Console.Write("Enter Card Number : ") ; 
            string CN = Console.ReadLine() ; 
              
             bool found = false ; 
            
            foreach (Card c in Database.Cards)
        {
             if ( c.CardNumber == CN && c.Status == "unblocked")
            {
                c.Status = "blocked" ; 
                Database.SaveCards() ; 
                 Console.WriteLine ("Card Blocked ") ; 
                 found = true ; 
                 
            }
           
        }
        if (found == false ) { Console.WriteLine("Card not found or Blocked") ; }



    }


   public static void UnblockCard()
    {
        
        Console.WriteLine ("\n---Unblock Card---\n") ; 

            foreach (Card c in Database.Cards)
        {
            if ( c.Status == "blocked")
            {
                Console.WriteLine("Card Number: " + c.CardNumber + " Balance : "+c.Balance +" Type: " +c.Type +" Status: " +c.Status + " User ID : " +c.UserId) ;
            }


        }

            Console.Write("Enter Card Number : ") ; 
            string CN = Console.ReadLine() ; 

              bool found = false ; 
            
            foreach (Card c in Database.Cards)
        {
             if ( c.CardNumber == CN && c.Status == "blocked")
            {
                c.Status = "unblocked" ; 
                Database.SaveCards() ; 
                 Console.WriteLine ("Card Unblocked ") ; 
                 found = true ; 
                 
            }
           
        }
        if (found == false ) { Console.WriteLine("Card not found or not Blocked") ; }
        
    }

     
  public static void ViewAllCards()
    {
                 Database.Cards.Sort((x, y) => x.Type.CompareTo(y.Type)); 
       foreach ( Card c in Database.Cards )
        {
                  Console.WriteLine("Card Number: " + c.CardNumber + " Balance : "+c.Balance +" Type: " +c.Type +" Status: " +c.Status + " User ID : " +c.UserId) ;
            

        }



    }   

   
  public static void ViewALLTxns ()
    {
        
              Database.Transactions.Sort((x, y) => x.TransactionType.CompareTo(y.TransactionType)); 

             foreach ( Transactions t in Database.Transactions)
        {
                Console.WriteLine (" \n|Transaction ID: " + t.TransactionId +"\n|Card Number:"+ t.CardNumber  +"\n|Type : " + t.TransactionType + "\n|Amount : " + t.Amount ) ;




        }
         


    }



























}