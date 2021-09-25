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
        private readonly ISurveyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyService(ISurveyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public bool CreateSurvey(Survey survey)
        {
            survey.Created_at = DateTime.UtcNow;

            _repository.Insert(survey);
            _unitOfWork.Save();
            return true;
        }

        public Activity GetSurvey(int surveyId)
        {
            //return _repository.Get(p => p.Id == surveyId).Result.FirstOrDefault();
            return null;
        }

        public List<Activity> GetSurvies(int activityId)
        {
            //return _repository.Get(p => p.Activity_Id == activityId).Result.ToList();
            return null;
        }
    }
}
