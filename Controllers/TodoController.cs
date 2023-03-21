using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListTest.Interfaces;
using TodoListTest.Models;
using TodoListTest.DTOs;

namespace TodoListTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoProvider todoProvider;

        private readonly DatabaseContext dbContext;

        public TodoController(ITodoProvider td, DatabaseContext db)
        {
            todoProvider = td;
            dbContext = db;
        }

        [HttpGet("/todo-items")]
        public async Task<IActionResult> GetTodo([FromQuery] int activity_group_id)
        {
            int statusCode = 400;
            IEnumerable<TodoModel> dataResponse = null;
            GetAllResponseTodoDTO response = new GetAllResponseTodoDTO();

            try
            {
                statusCode = 200;

                if (activity_group_id == 0)
                {
                    dataResponse = await todoProvider.GetTodo();
                }
                else
                {
                    dataResponse = await todoProvider.GetTodoId(activity_group_id);
                }

                response.Success = "Successs";
                response.Message = "Successs";
                response.Data = dataResponse;

            }
            catch (Exception err)
            {
                statusCode = 500;

                response.Success = "System Error";
                response.Message = err.ToString();
            }

            return StatusCode(statusCode, response);
        }

        [HttpGet("/todo-items/{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            int statusCode = 400;
            TodoModel dataResponse = null;
            GetDetailResponseTodoDTO response = new GetDetailResponseTodoDTO();

            try
            {
                var todo = dbContext.Todos.Find(id);

                if (todo != null)
                {
                    statusCode = 200;
                    dataResponse = await todoProvider.GetTodoById(id);

                    response.Success = "Success";
                    response.Message = "Success";
                    response.Data = dataResponse;
                }
                else
                {
                    statusCode = 404;

                    response.Success = "Not Found";
                    response.Message = "Todo with ID " + id + " Not Found";
                }
            }
            catch (Exception err)
            {
                statusCode = 500;

                response.Success = "System Error";
                response.Message = err.ToString();
            }

            return StatusCode(statusCode, response);
        }

        [HttpPost("/todo-items")]
        public async Task<IActionResult> AddTodo(TodoModel td)
        {
            int statusCode = 400;
            TodoModel dataResponse = null;
            GetDetailResponseTodoDTO response = new GetDetailResponseTodoDTO();

            try
            {
                if (td.title != null)
                {
                    statusCode = 200;
                    dataResponse = await todoProvider.AddTodo(td);

                    response.Success = "Success";
                    response.Message = "Success";
                }
                else
                {
                    statusCode = 400;

                    response.Success = "Bad Request";
                    response.Message = "Title Cannot be Null";
                }

                response.Data = dataResponse;
            }
            catch (Exception err)
            {
                statusCode = 500;

                response.Success = "System Error";
                response.Message = err.ToString();
            }

            return StatusCode(statusCode, response);
        }

        [HttpPatch("/todo-items/{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoModel td)
        {
            int statusCode = 400;
            TodoModel dataResponse = null;
            GetDetailResponseTodoDTO response = new GetDetailResponseTodoDTO();

            try
            {
                var todo = dbContext.Todos.Find(id);

                if (todo != null)
                {
                    if (td.title != null)
                    {
                        statusCode = 200;
                        dataResponse = await todoProvider.UpdateTodo(id, td);

                        response.Success = "Success";
                        response.Message = "Success";
                        response.Data = dataResponse;
                    }
                    else
                    {
                        statusCode = 400;

                        response.Success = "Bad Request";
                        response.Message = "Title Cannot be Null";
                    }             
                }
                else
                {
                    statusCode = 404;

                    response.Success = "Not Found";
                    response.Message = "Todo with ID " + id + " Not Found";
                }
            }
            catch (Exception err)
            {
                statusCode = 500;

                response.Success = "System Error";
                response.Message = err.ToString();
            }

            return StatusCode(statusCode, response);
        }

        [HttpDelete("/todo-items/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            int statusCode = 400;
            DeleteHelperDTO emptyResponse = new DeleteHelperDTO { };
            DeleteResponseTodoDTO response = new DeleteResponseTodoDTO();

            try
            {
                var todo = dbContext.Todos.Find(id);

                if (todo != null)
                {
                    statusCode = 200;
                    await todoProvider.DeleteTodo(id);

                    response.Success = "Success";
                    response.Message = "Success";
                    response.Data = emptyResponse;

                }
                else
                {
                    statusCode = 404;

                    response.Success = "Not Found";
                    response.Message = "Todo with ID " + id + " Not Found";
                    response.Data = emptyResponse;
                }
            }
            catch (Exception err)
            {
                statusCode = 500;

                response.Success = "System Error";
                response.Message = err.ToString();
            }

            return StatusCode(statusCode, response);
        }
    }
}
