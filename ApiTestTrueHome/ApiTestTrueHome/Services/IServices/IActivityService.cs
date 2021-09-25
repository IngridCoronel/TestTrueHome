using ApiTestTrueHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Services.IServices
{
    public interface IActivityService
    {
        bool CreateActivity(Activity activity);
        Activity GetActivity(int activityId);
        List<Activity> GetActivities(int idProperty);
        List<Activity> GetActivitiesForDay(int idProperty, DateTime scheduleDay);
        Task<int> RescheduleActivity(int idActivity, DateTime newScheduleDay);
        Task<int> CancelActivity(int idActivity);
        List<Activity> GetActivitiesAll();
        List<Activity> GetActivitiesForStatus(string status, DateTime iniSchedule, DateTime endSchedule);

    }
}
