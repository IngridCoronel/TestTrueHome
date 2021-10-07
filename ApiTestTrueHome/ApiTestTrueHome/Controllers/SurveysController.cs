using ApiTestTrueHome.Models;
using ApiTestTrueHome.Models.Dtos;
using ApiTestTrueHome.Repository.IRepository;
using ApiTestTrueHome.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestTrueHome.Controllers
{
    [Route("Surveys")]
    [ApiController]
    public class SurveysController : Controller
    {
        private readonly ISurveyService _suvService;
        private readonly IMapper _mapper;

        public SurveysController(ISurveyService suvService, IMapper mapper)
        {
            _suvService = suvService;
            _mapper = mapper;
        }

        
        [HttpPost("CreateSurvey")]
        public IActionResult CreateSurvey([FromBody] SurveyDto SurveyDto)
        {
            if (SurveyDto == null)
            {
                return BadRequest(ModelState);
            }

            var survey = _mapper.Map<Survey>(SurveyDto);

            var survCreat = _suvService.CreateSurvey(survey);

            if (survCreat != "Ok")
            {
                ModelState.AddModelError("", survCreat);
                return StatusCode(500, ModelState);
            }

            return Ok(SurveyDto);
        }
        
    }
}
