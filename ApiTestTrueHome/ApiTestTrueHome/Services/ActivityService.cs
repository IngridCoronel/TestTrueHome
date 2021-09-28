using ApiTestTrueHome.Data.IData;
using ApiTestTrueHome.Models;
using ApiTestTrueHome.Repository.IRepository;
using ApiTestTrueHome.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository repository, IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public bool CreateActivity(Activity activity)
        {

            activity.Status = "Active";
            activity.Created_at = DateTime.UtcNow;
            activity.Updated_at = DateTime.UtcNow;

            var  x = _propertyRepository.Get(p => p.Id == activity.Property_Id).Result.Where(s => s.Status == "Active").FirstOrDefault();
            if (x == null)
            {
                //Propiedad desactivada
                return false;
            }

            //No traslapar actividades
            DateTime IniDate = activity.Schedule.AddHours(-1);
            DateTime EndDate = activity.Schedule.AddHours(1);

            //var scheduleDay = _repository.Get(a => a.Id == activity.Property_Id).Result.Where(s => s.Schedule <= EndDate).FirstOrDefault();
            var scheduleDay = _repository.Get(a => a.Id == activity.Property_Id).Result.Where(s => s.Status != "Canceled" && s.Schedule >= IniDate && s.Schedule <= EndDate).ToList();

            _repository.Insert(activity);
            _unitOfWork.Save();
            return true;
        }

        public Activity GetActivity(int activityId)
        {
            return _repository.Get(p => p.Id == activityId, includeProperties: "Property").Result.FirstOrDefault();
        }

        public List<Activity> GetActivities(int idProperty)
        {
            return _repository.Get(p => p.Property_Id == idProperty, includeProperties: "Property").Result.ToList();
        }
        public List<Activity> GetActivitiesForDay(int idProperty, DateTime scheduleDay)
        {
            return _repository.Get(p => p.Property_Id == idProperty & p.Schedule.DayOfYear == scheduleDay.DayOfYear, includeProperties: "Property").Result.OrderBy(a => a.Schedule).ToList();
        }

        public async Task<int> RescheduleActivity(int idActivity, DateTime newScheduleDay)
        {
            //var Act= _repository.Get(p => p.Id == idActivity).Result.OrderBy(a => a.Schedule).ToList();

            var act = _repository.Get(p => p.Id == idActivity).Result.FirstOrDefault();
            
            if (act == null)
            {
                return 0;
            }

            //Actividad cancelada
            if (act.Status == "Canceled")
            {
                return 0;
            }

            //Validación de fecha
            if (newScheduleDay <= DateTime.Now)
            {
                return 0;
            }

            act.Schedule = newScheduleDay;
            act.Updated_at = DateTime.UtcNow;

            await _repository.Update(act);
            return await _unitOfWork.SaveAsync();
        }
        
        public async Task<int> CancelActivity(int idActivity)
        {

            var act = _repository.Get(p => p.Id == idActivity).Result.FirstOrDefault();

            if (act == null)
            {
                return 0;
            }

            //Actividad cancelada
            if (act.Status == "Canceled" || act.Status == "Done")
            {
                return 0;
            }

            act.Status = "Canceled";
            act.Updated_at = DateTime.UtcNow;

            await _repository.Update(act);
            return await _unitOfWork.SaveAsync();
        }

        public List<Activity> GetActivitiesAll()
        {
            DateTime IniDate = DateTime.UtcNow.AddDays(-3);
            DateTime EndDate = DateTime.UtcNow.AddDays(14);

            //return _repository.Get(includeProperties: "Property").Result.Where(s => s.Status != "Canceled" && s.Schedule >= IniDate && s.Schedule <= EndDate).OrderBy(a => a.Schedule).ToList();
            return _repository.Get(s => s.Schedule >= IniDate && s.Schedule <= EndDate, includeProperties: "Property").Result.OrderBy(a => a.Schedule).ToList();
        }

        public List<Activity> GetActivitiesForStatus(string status, DateTime iniSchedule, DateTime endSchedule)
        {
            return _repository.Get(s => s.Status == status && s.Schedule.DayOfYear >= iniSchedule.DayOfYear && s.Schedule.DayOfYear <= endSchedule.DayOfYear, includeProperties: "Property").Result.OrderBy(a => a.Schedule).ToList();
        }
    }
}
