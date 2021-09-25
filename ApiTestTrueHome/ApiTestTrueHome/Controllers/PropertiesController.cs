using ApiTestTrueHome.Models.Dtos;
using ApiTestTrueHome.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Controllers
{
    [Route("api/Properties")]
    [ApiController]
    public class PropertiesController : Controller
    {
        private readonly IPropertyRepository _ctRepo;
        private readonly IMapper _mapper;

        public PropertiesController(IPropertyRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProperties()
        {
            var listProperties = _ctRepo.GetProperties();

            var listPropertiesDto = new List<PropertyDto>();

            return Ok(listProperties);
        }
    }
}
