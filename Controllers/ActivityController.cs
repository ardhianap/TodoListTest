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
    public class ActivityController : ControllerBase
    {
        private readonly IActivityProvider activityProvider;

        private readonly DatabaseContext dbContext;

        public ActivityController(IActivityProvider act, DatabaseContext db)
        {
            activityProvider = act;
            dbContext = db;
        }

        [HttpGet("/activity-groups")]
        public async Task<IActionResult> GetActivity()
        {
            int statusCode = 400;
            IEnumerable<ActivityModel> dataResponse = null;
            GetAllResponseActivityDTO response = new GetAllResponseActivityDTO();

            try
            {
                statusCode = 200;
                dataResponse = await activityProvider.GetActivity();

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

        [HttpGet("/activity-groups/{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            int statusCode = 400;
            ActivityModel dataResponse = null;
            GetDetailResponseActivityDTO response = new GetDetailResponseActivityDTO();

            try
            {
                var activity = dbContext.Activities.Find(id);

                if (activity != null)
                {
                    statusCode = 200;
                    dataResponse = await activityProvider.GetActivityById(id);

                    response.Success = "Success";
                    response.Message = "Success";
                    response.Data = dataResponse;
                }
                else
                {
                    statusCode = 404;

                    response.Success = "Not Found";
                    response.Message = "Activity with ID " + id + " Not Found";
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

        [HttpPost("/activity-groups")]
        public async Task<IActionResult> AddActivity(ActivityModel act)
        {
            int statusCode = 400;
            ActivityModel dataResponse = null;
            GetDetailResponseActivityDTO response = new GetDetailResponseActivityDTO();

            try
            {
                if (act.title != null)
                {
                    statusCode = 200;
                    dataResponse = await activityProvider.AddActivity(act);

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

        [HttpPatch("/activity-groups/{id}")]
        public async Task<IActionResult> UpdateActivity(int id, ActivityModel act)
        {
            int statusCode = 400;
            ActivityModel dataResponse = null;
            GetDetailResponseActivityDTO response = new GetDetailResponseActivityDTO();

            try
            {
                var activity = dbContext.Activities.Find(id);

                if (activity != null)
                {
                    if (act.title != null)
                    {
                        statusCode = 200;
                        dataResponse = await activityProvider.UpdateActivity(id, act);

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
                    response.Message = "Activity with ID " + id + " Not Found";
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

        [HttpDelete("/activity-groups/{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            int statusCode = 400;
            DeleteHelperDTO emptyResponse = new DeleteHelperDTO { };
            DeleteResponseActivityDTO response = new DeleteResponseActivityDTO();

            try
            {
                var activity = dbContext.Activities.Find(id);

                if (activity != null)
                {
                    statusCode = 200;
                    await activityProvider.DeleteActivity(id);

                    response.Success = "Success";
                    response.Message = "Success";
                    response.Data = emptyResponse;

                }
                else
                {
                    statusCode = 404;

                    response.Success = "Not Found";
                    response.Message = "Activity with ID " + id + " Not Found";
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
