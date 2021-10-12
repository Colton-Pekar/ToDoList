using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.ListComponents
{
    public class ListComponents
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string ItemToDo { get; set; }
        public string CompletedBy { get; set; }
        public bool ItemComplete { get; set; }
    }
}
