using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListTest.Models;

namespace TodoListTest.DTOs
{
    public class GetAllResponseTodoDTO
    {
        public string Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<TodoModel> Data { get; set; }
    }
}
