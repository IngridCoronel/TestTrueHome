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
        private readonly ISurveyRepository _surveyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository repository, IPropertyRepository propertyRepository, ISurveyRepository surveyRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _propertyRepository = propertyRepository;
            _surveyRepository = surveyRepository;
            _unitOfWork = unitOfWork;
        }

        public string CreateActivity(Activity activity)
        {

            activity.Status = "Active";
            activity.Created_at = DateTime.UtcNow;
            activity.Updated_at = DateTime.UtcNow;

            if (activity.Schedule <= DateTime.Now)
                return "La fecha programada no es válida";

            var  x = _propertyRepository.Get(p => p.Id == activity.Property_Id).Result.Where(s => s.Status == "Active").FirstOrDefault();
            if (x == null)
            {
                //Propiedad desactivada
                //return false;
                return "No es posible agregar una actividad a una propiedad desactivada o no existente";
            }

            //No traslapar actividades
            DateTime IniDate = activity.Schedule.AddHours(-1);
            DateTime EndDate = activity.Schedule.AddHours(1);

            var scheduleDay = _repository.Get(a => a.Schedule.DayOfYear == activity.Schedule.DayOfYear && a.Schedule.Year == activity.Schedule.Year && a.Schedule >= IniDate && a.Schedule <= EndDate && a.Property_Id == activity.Property_Id && a.Status != "Canceled").Result.ToList();

            if (scheduleDay.Count > 0)
            {
                //return "No es posible agendar esta visita, debido a que se traslapa con la visita " & scheduleDay(0).Title & ", con fecha: " & scheduleDay(0).Schedule;
                return "No es posible agendar esta actividad, debido a que se traslapa con otra";

            }

            _repository.Insert(activity);
            _unitOfWork.Save();
            return "Ok";
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
            return _repository.Get(p => p.Property_Id == idProperty && p.Schedule.DayOfYear == scheduleDay.DayOfYear && p.Schedule.Year == scheduleDay.Year, includeProperties: "Property").Result.OrderBy(a => a.Schedule).ToList();
        }

        public async Task<string> RescheduleActivity(int idActivity, DateTime newScheduleDay)
        {
            //var Act= _repository.Get(p => p.Id == idActivity).Result.OrderBy(a => a.Schedule).ToList();
            
            var act = _repository.Get(p => p.Id == idActivity).Result.FirstOrDefault();
            
            if (act == null)
                return "No existe una actividad con ese Id.";

            //Actividad cancelada
            if (act.Status == "Canceled")
                return "No es posible reagendar una actividad cancelada.";

            //Validación de fecha
            if (newScheduleDay <= DateTime.Now)
                return "La nueva fecha no es válida.";


            ////////////////////
            ///
            /// 
            ////////////////////
            ///
            var x = _propertyRepository.Get(p => p.Id == act.Property_Id).Result.Where(s => s.Status == "Active").FirstOrDefault();
            if (x == null)
                return "No es posible reagendar una actividad de una propiedad desactivada.";

            //No traslapar actividades
            DateTime IniDate = newScheduleDay.AddHours(-1);
            DateTime EndDate = newScheduleDay.AddHours(1);

            var scheduleDay = _repository.Get(a => a.Schedule.DayOfYear == newScheduleDay.DayOfYear && a.Schedule.Year == newScheduleDay.Year && a.Schedule >= IniDate && a.Schedule <= EndDate && a.Property_Id == act.Property_Id && a.Status != "Canceled" && a.Id != act.Id).Result.ToList();

            if (scheduleDay.Count > 0)
            {
                //return "No es posible agendar esta visita, debido a que se traslapa con la visita " & scheduleDay(0).Title & ", con fecha: " & scheduleDay(0).Schedule;
                return "No es posible reagendar esta actividad, debido a que se traslapa con otra";

            }


            act.Schedule = newScheduleDay;
            act.Updated_at = DateTime.UtcNow;

            await _repository.Update(act);
            await _unitOfWork.SaveAsync();

            return "Ok";
        }
        
        public async Task<string> CancelActivity(int idActivity)
        {

            var act = _repository.Get(p => p.Id == idActivity).Result.FirstOrDefault();

            if (act == null)
            {
                return "No existe la actividad con el id proporcionado." ;
            }

            //Actividad cancelada
            if (act.Status == "Canceled")
            {
                return "No es posible cancelar una acitividad cancelada.";
            }

            //Actividad Realizada
            if (act.Status == "Done")
            {
                return "No es posible cancelar una actividad realizada.";
            }

            act.Status = "Canceled";
            act.Updated_at = DateTime.UtcNow;

            await _repository.Update(act);
             await _unitOfWork.SaveAsync();

            return "Ok";
        }

        public List<Activity> GetActivitiesAll()
        {
            DateTime IniDate = DateTime.UtcNow.AddDays(-3);
            DateTime EndDate = DateTime.UtcNow.AddDays(14);

            //return _surveyRepository.Get(p => p.Activity.Schedule >= IniDate && p.Activity.Schedule <= EndDate, includeProperties: "Activity,Activity.Property").Result.OrderBy(a => a.Activity.Schedule).Select(x=>x.Activity).ToList();
            return _repository.Get(s => s.Schedule >= IniDate && s.Schedule <= EndDate, includeProperties: "Property").Result.OrderBy(a => a.Schedule).ToList();
        }

        public List<Activity> GetActivitiesForStatus(string status, DateTime iniSchedule, DateTime endSchedule)
        {
            return _repository.Get(s => s.Status == status && s.Schedule.DayOfYear >= iniSchedule.DayOfYear && s.Schedule.DayOfYear <= endSchedule.DayOfYear, includeProperties: "Property").Result.OrderBy(a => a.Schedule).ToList();
        }
    }
}
