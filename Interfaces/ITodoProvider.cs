using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListTest.Models;

namespace TodoListTest.Interfaces
{
    public interface ITodoProvider
    {
        Task<IEnumerable<TodoModel>> GetTodo();
        Task<IEnumerable<TodoModel>> GetTodoId(int id);
        Task<TodoModel> GetTodoById(int id);
        Task<TodoModel> AddTodo(TodoModel todo);
        Task<TodoModel> UpdateTodo(int id, TodoModel todo);
        Task DeleteTodo(int id);
    }
}
