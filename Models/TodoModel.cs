using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListTest.Models
{
    public class TodoModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public int? activity_group_id { get; set; }
        public string title { get; set; }
        public string priority { get; set; }
        public DateTime? created_at { get; set; }
    }
}
