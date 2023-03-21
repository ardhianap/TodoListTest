using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoListTest.Interfaces;
using TodoListTest.Models;

namespace TodoListTest.Providers
{
    public class TodoProvider : ITodoProvider
    {
        private readonly DatabaseContext dbContext;
        public TodoProvider(DatabaseContext db)
        {
            dbContext = db;
        }

        public async Task<IEnumerable<TodoModel>> GetTodo()
        {

            return await dbContext.Todos.ToListAsync();
        }

        public async Task<IEnumerable<TodoModel>> GetTodoId(int id)
        {

            return await dbContext.Todos.Where(x => x.activity_group_id == id).ToListAsync();
        }

        public async Task<TodoModel> GetTodoById(int id)
        {
            return await dbContext.Todos.FindAsync(id);
        }

        public async Task<TodoModel> AddTodo(TodoModel todo)
        {
            if (todo != null)
            {
                todo.created_at = DateTime.Now;
                dbContext.Todos.Add(todo);
                await dbContext.SaveChangesAsync();

                return todo;
            }
            return null;
        }

        public async Task<TodoModel> UpdateTodo(int id, TodoModel todo)
        {
            TodoModel td = dbContext.Todos.Where(x => x.id == id).FirstOrDefault();

            if (td != null)
            {
                if (todo.title == null)
                {
                    td.title = td.title;
                }
                else
                {
                    td.title = todo.title;
                }

                if (todo.activity_group_id == null)
                {
                    td.activity_group_id = td.activity_group_id;
                }
                else
                {
                    td.activity_group_id = todo.activity_group_id;
                }

                if (todo.priority == null)
                {
                    td.priority = td.priority;
                }
                else
                {
                    td.priority = todo.priority;
                }

                await dbContext.SaveChangesAsync();
            }

            if (todo.activity_group_id == null)
            {
                todo.activity_group_id = td.activity_group_id;
            }

            if (todo.title == null)
            {
                todo.title = td.title;
            }

            if (todo.priority == null)
            {
                todo.priority = td.priority;
            }

            todo.id = td.id;
            todo.created_at = td.created_at;

            return todo;
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            dbContext.Todos.Remove(todo);

            await dbContext.SaveChangesAsync();

        }

    }
}
