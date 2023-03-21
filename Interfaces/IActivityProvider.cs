using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListTest.Models;

namespace TodoListTest.Interfaces
{
    public interface IActivityProvider
    {
        Task<IEnumerable<ActivityModel>> GetActivity();
        Task<ActivityModel> GetActivityById(int id);
        Task<ActivityModel> AddActivity(ActivityModel activity);
        Task<ActivityModel> UpdateActivity(int id, ActivityModel activity);
        Task DeleteActivity(int id);
    }
}


