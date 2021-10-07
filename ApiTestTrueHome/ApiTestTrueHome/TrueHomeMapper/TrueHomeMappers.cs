using ApiTestTrueHome.Models;
using ApiTestTrueHome.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.TrueHomeMapper
{
    public class TrueHomeMappers : Profile
    {
        public TrueHomeMappers()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<Property, PropertyAllDto>().ReverseMap();
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
        }
    }
}
