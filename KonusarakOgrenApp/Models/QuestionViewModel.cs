using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KonusarakOgrenApp.Models
{
    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
        ListItems = new List<ItemList>() {
        new ItemList { Text = "A", Value = 1 },
        new ItemList { Text = "B", Value = 2 },
        new ItemList { Text = "C", Value = 3 },
        new ItemList { Text = "D", Value = 4 },
        };
        }
        public int Id { get; set; }
        public string QuestionId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]

        public virtual List<Question> Questions { get; set; }
        [NotMapped]

        public virtual List<ItemList> ListItems { get; set; }
    }
}
