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
    public class ActivityProvider : IActivityProvider
    {
        private readonly DatabaseContext dbContext;
        public ActivityProvider(DatabaseContext db)
        {
            dbContext = db;
        }

        public async Task<IEnumerable<ActivityModel>> GetActivity()
        {
            return await dbContext.Activities.ToListAsync();
        }

        public async Task<ActivityModel> GetActivityById(int id)
        {
            return await dbContext.Activities.FindAsync(id);
        }

        public async Task<ActivityModel> AddActivity(ActivityModel activity)
        {
            if (activity != null)
            {
                activity.created_at = DateTime.Now;
                dbContext.Activities.Add(activity);
                await dbContext.SaveChangesAsync();

                return activity;
            }
            return null;
        }

        public async Task<ActivityModel> UpdateActivity(int id, ActivityModel activity)
        {
            ActivityModel act = dbContext.Activities.Where(x => x.id == id).FirstOrDefault();

            if (act != null)
            {
                if (activity.title == null)
                {
                    act.title = act.title;
                }
                else
                {
                    act.title = activity.title;
                }

                if (activity.email == null)
                {
                    act.email = act.email;
                }
                else
                {
                    act.email = activity.email;
                }

                await dbContext.SaveChangesAsync();
            }

            if (activity.email == null)
            {
                activity.email = act.email;
            }

            if (activity.title == null)
            {
                activity.title = act.title;
            }

            activity.id = act.id;
            activity.created_at = act.created_at;

            return activity;
        }

        public async Task DeleteActivity(int id)
        {
            var activity = await dbContext.Activities.FindAsync(id);

            dbContext.Activities.Remove(activity);

            await dbContext.SaveChangesAsync();

        }

    }
}