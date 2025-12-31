using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using console = System.Console;

namespace Models 
{
      public class FacultyMember 
  {
    public string UserId { get; set; }
    public string Name { get; set; }

    public List<string> TaughtCourses { get; set;}= new List<string>();










  }
}