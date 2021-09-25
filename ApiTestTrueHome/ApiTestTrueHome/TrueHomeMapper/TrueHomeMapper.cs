using ApiTestTrueHome.Models;
using ApiTestTrueHome.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.TrueHomeMapper
{
    public class TrueHomeMapper : Profile
    {
        public TrueHomeMapper()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
        }
    }
}
