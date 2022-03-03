using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenApp.Models
{
    public class Question
    {
        public Question()
        {
        ListItems = new List<ItemList>() {
        new ItemList { Text = "A", Value = 1 },
        new ItemList { Text = "B", Value = 2 },
        new ItemList { Text = "C", Value = 3 },
        new ItemList { Text = "D", Value = 4 },
        };
        }

        public int Id { get; set; }
        public string QuestionUnique { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public string Option { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectOption { get; set; }
        public string CorrectId { get; set; }
        [NotMapped]

        public virtual List<ItemList> ListItems { get; set; }

    }
}
