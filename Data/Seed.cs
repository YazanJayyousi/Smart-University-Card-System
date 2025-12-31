// this is used to Initiate the PreData ( Tables ) to Json in all cases to avoid " File not Found error " 
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using console = System.Console;

using Models;

public static class SeedData // static-> No changes allowed

{
 public static List<Student> GetDefaultStudents()
    {
        return new List<Student> 
        {
            new Student { UserId = "S01", Name = "Ali" , RegisteredCourses = new List<string> {"CPE100" , "SE400"} },

            new Student { UserId = "S02", Name = "Omar" , RegisteredCourses = new List<string> {"CPE100" , "NES200"} },

            new Student { UserId = "S03", Name = "Reem" , RegisteredCourses = new List<string> {"NES200" ,"CIS300"  , "SE400"} },

            new Student { UserId = "S04", Name = "Maher" , RegisteredCourses = new List<string> {"CPE100" , "SE400"} }


        };
    }


public static List<FacultyMember> GetDefaultFaculty()
    {
        return new List<FacultyMember>
        {
          new FacultyMember
          {
              UserId = "F01" , Name = "Sami" , TaughtCourses = new List<string> {"CPE100" , "CIS300"}
          }  ,

          new FacultyMmeber
          {
              UserID = "F02" , nameof = "Eman" , TaughtCourses = new List<string> { "NES200" , "SE400"}
              
          }




        };

    }


public static List<Card> GetDefaultCards()
    {
    return new List<Card>
    {
      new Card {CardNumber ="10" , Balance = 80 , Type = "faculty member" , Status= "Unblocked" , UserId="F02" },
      new Card {CardNumber ="20" , Balance = 110 , Type = "student" , Status= "Unblocked" , UserId="S02" },
      new Card {CardNumber ="30" , Balance = 95 , Type = "student" , Status= "Blocked" , UserId="S03" },
      new Card {CardNumber ="40" , Balance = 160 , Type = "student" , Status= "Unblocked" , UserId="S04" }  


    };

    }



}
