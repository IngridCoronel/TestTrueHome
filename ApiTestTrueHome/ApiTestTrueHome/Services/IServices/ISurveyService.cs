using ApiTestTrueHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Services.IServices
{
    public interface ISurveyService
    {
        string CreateSurvey(Survey survey);
        Survey GetSurvey(int surveyId);
        List<Survey> GetSurveys();

    }
}
