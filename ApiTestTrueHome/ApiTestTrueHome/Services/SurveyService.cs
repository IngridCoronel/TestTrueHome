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
            var x = _activityRepository.Get(p => p.Id == survey.Activity_Id).Result.FirstOrDefault();
           
            if (x == null)
                return "No existe el id de la actividad.";

            //survey.Activity = x;

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
            DateTime IniDate = DateTime.UtcNow.AddDays(-3);
            DateTime EndDate = DateTime.UtcNow.AddDays(14);
            return _repository.Get(p => p.Activity.Schedule >= IniDate && p.Activity.Schedule <= EndDate, includeProperties: "Activity,Activity.Property").Result.OrderBy(a => a.Activity.Schedule).ToList();

            //return null;
        }
    }
}
