using System.Collections.Generic;
using System.IO;           
using System.Text.Json;  
using system = System.Console;

using Models;
namespace DataBase {
public static class Database
{
    // Temp momory store handling : 
    public static List<Student> Students = new List<Student>();
    public static List<FacultyMember> Faculty = new List<FacultyMember>();
    public static List<Card> Cards = new List<Card>();
    public static List<Transactions> Transactions = new List<Transactions>();
    public static List<Attendance> AttendanceRecords = new List<Attendance>();

// Files : 
private static string _studentFile = "students.json";
     private static string _facultyFile = "faculty.json";
      private static string _cardFile = "cards.json";
     private static string _txnFile = "transactions.json";
    private static string _attFile = "attendance.json";

    // Seed UP from seed.cs functionss : 

    public static void Initialize()
    {
        
         if ( File.Exists(_studentFile) ) 
         {

             string Json =   File.ReadAllText(_studentFile)  ;

              Students = JsonSerializer.Deserialize <List<Student>>(Json) ;
         }

         else ///////////// First RUN SEEDING 
        {
            Students = SeedData.GetDefaultStudents();
            SaveStudents(); ///Define later !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        if (File.Exists(_facultyFile) )
        {
            string Json = File.ReadAllText(_facultyFile);

            Faculty = JsonSerializer.Deserialize <List<FacultyMember>> (Json) ; 

        }
        else
        {
            Faculty = SeedData.GetDefaultFaculty() ; 

              SaveFaculty(); ///Define later !!!!!!!!!!!!!

        }

        if (File.Exists(_cardFile))
        {
            string Json = File.ReadAllText(_cardFile); 
            Cards = JsonSerializer.Deserialize <List<Card>> (Json) ; 

        }

        else
        {
            Cards = SeedData.GetDefaultCards() ; 
            SaveCards( ); ///Define later !!!!!!!!!!!

        }
         
        if (File.Exists(_txnFile))
        {
            string Json = File.ReadAllText(_txnFile); 
            Transactions = JsonSerializer.Deserialize <List<Transactions>> (Json) ; 

        }
        else
        {
            Console.WriteLine("No Transactions File Found") ;
        }

        if (File.Exists(_attFile))
        {
            string Json = File.ReadAllText(_attFile); 
            AttendanceRecords = JsonSerializer.Deserialize <List<Attendance>> (Json) ; 

        }
        else
        {
            Console.WriteLine("No Attendance File Found") ;
        }
    } 

        // Save : 

        public static void SaveStudents()
    {
        string json = JsonSerializer.Serialize(Students);
        File.WriteAllText(_studentFile , json);

    }
    public static void SaveFaculty()
    {
        string json = JsonSerializer.Serialize(Faculty);
        File.WriteAllText(_facultyFile , json);

    }
    public static void SaveCards()
    {
        string json = JsonSerializer.Serialize(Cards);
        File.WriteAllText(_cardFile , json);

    }
    public static void SaveTransactions()
    {
        string Json =JsonSerializer.Serialize(Transactions);
        File.WriteAllText(_txnFile , Json ); 
    }

    public static void SaveAttendance()
    {
        string Json =JsonSerializer.Serialize(AttendanceRecords);
        File.WriteAllText(_attFile , Json ); 
    }





}   





}