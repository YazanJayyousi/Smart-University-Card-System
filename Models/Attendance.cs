using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using console = System.Console;

namespace Models 
{
  public class Attendance
   {
    public string CourseId { get; set; }

    public string Date { get; set; }

    public List<string> Attendees {get; set; } = new List<string>(); 





  }
}