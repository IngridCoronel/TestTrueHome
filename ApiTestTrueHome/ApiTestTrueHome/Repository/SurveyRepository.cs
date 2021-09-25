using ApiTestTrueHome.Data;
using ApiTestTrueHome.Data.IData;
using ApiTestTrueHome.Models;
using ApiTestTrueHome.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Repository
{
    public class SurveyRepository : Repository<Survey>, ISurveyRepository
    {
        public SurveyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
