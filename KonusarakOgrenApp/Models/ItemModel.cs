using System;
using System.Collections.Generic;

namespace KonusarakOgrenApp.Models
{
    public class ItemModel
    {
        public ItemModel()
        {
        EmployeesList = new List<ItemList>() {
        new ItemList { Text = "A", Value = 1 },
        new ItemList { Text = "B", Value = 2 },
        new ItemList { Text = "C", Value = 3 },
        new ItemList { Text = "D", Value = 4 },
        };
        }
        public int EmployeeId { get; set; }

        public virtual List<ItemList> EmployeesList { get; set; }
    }
}
