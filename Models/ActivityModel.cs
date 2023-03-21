using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListTest.Models
{
    public class ActivityModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public DateTime? created_at { get; set; }
    }
}
