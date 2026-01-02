using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;


namespace Models
{
    

public class Transactions
{
    public string TransactionId { get; set; }
    public string CardNumber { get; set; }
    public string TransactionType { get; set; }
     public string Amount { get; set; } // string because N/A req in attendance trans 




}
}