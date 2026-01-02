using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Models 
{


    public class Track 
        {
       public string Origin { get; set; }
        
        public string Destination { get ; set; }

        public decimal Cost { get; set;   }
         
         public static List<Track> GetTracks( )
    {
        return new List<Track>
        {
          new Track { Origin = "Main Gate", Destination = " Northern buildings", Cost = 3  },
          new Track { Origin = "Main Gate", Destination = "Southern buildings", Cost = 4},
          new Track { Origin = "Main Gate", Destination = " Library", Cost = 5 }  


        };



    }




        }
















}