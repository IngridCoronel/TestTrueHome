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
    public class SurveyService : ISurveyService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ISurveyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyService(ISurveyRepository repository, IActivityRepository activityRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _activityRepository = activityRepository;
            _unitOfWork = unitOfWork;
        }

        public string CreateSurvey(Survey survey)
        {

            //var x = _activityRepository.Get(p => p.Id == survey.Activity_Id).Result.Where(s => s.Status == "Active").FirstOrDefault();
            var act = _activityRepository.Get(p => p.Id == survey.Activity_Id).Result.FirstOrDefault();
           
            if (act == null)
                return "No existe el id de la actividad.";

            if (act.Status == "Canceled")
                return "La actividad está cancelada, no es posible agregar una encuesta.";

            var surv = _repository.Get(p => p.Activity_Id == survey.Activity_Id).Result.FirstOrDefault();

            if (surv != null)
                return "La actividad ya tiene asociada una encuesta.";

            _repository.Insert(survey);
            _unitOfWork.Save();
            return "Ok";
        }

        public Survey GetSurvey(int surveyId)
        {
            //return _repository.Get(p => p.Id == surveyId).Result.FirstOrDefault();
            return null;
        }

        public List<Survey> GetSurveys()
        {
            //DateTime IniDate = DateTime.UtcNow.AddDays(-3);
            //DateTime EndDate = DateTime.UtcNow.AddDays(14);
            //return _repository.Get(p => p.Activity.Schedule >= IniDate && p.Activity.Schedule <= EndDate, includeProperties: "Activity,Activity.Property").Result.OrderBy(a => a.Activity.Schedule).ToList();
            return _repository.Get(includeProperties: "Activity,Activity.Property").Result.OrderBy(a => a.Activity.Schedule).ToList();

            //return null;
        }
    }
}
