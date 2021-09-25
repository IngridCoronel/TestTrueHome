using ApiTestTrueHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Services.IServices
{
    public interface ISurveyService
    {
        bool CreateSurvey(Survey survey);
        Activity GetSurvey(int surveyId);
        List<Activity> GetSurvies(int activityId);

    }
}
