using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Models 
{
      public class Menu 
  {
            
            public string Item
        {
            get; set; 
        }

            public decimal Price
        {
            get; set; 
        }
           
           public static List<Menu> GetMenu()
        {
            return new List<Menu>
            {
                 new Menu { Item = "Steak", Price = 8 } ,
                 new Menu {Item = " Soup" , Price = 2}, 
                 new Menu  {Item = " Sandwich" , Price = 3},
                 new Menu {Item = " Salad" , Price = 4},
                 new Menu {Item = " Tea" , Price = 2} ,
                 new Menu {Item = " Juice" , Price = 3},
                 new Menu {Item = "Cake" , Price = 5},
                 new Menu {Item = "Water" , Price =1}
                
                
            };
        }

  }





}