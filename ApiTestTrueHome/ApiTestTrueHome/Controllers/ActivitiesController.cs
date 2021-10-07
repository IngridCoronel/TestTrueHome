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
    [Route("Activities")]
    [ApiController]
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _actService;
        private readonly ISurveyService _surveyService;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityService actService, ISurveyService surveyService, IMapper mapper)
        {
            _actService = actService;
            _surveyService = surveyService; 
            _mapper = mapper;
        }

        [HttpGet("GetActivities/{propertyId:int}")]
        public IActionResult GetActivities(int propertyId)
        {
            var listActivities = _actService.GetActivities(propertyId);

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }
            //var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);

            //return Ok(itemActivity);
            var listSurveys = _surveyService.GetSurveys();

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }

            var qry = listActivities.GroupJoin(
                        listSurveys,
                        lAct => lAct.Id,
                        lSur => lSur.Activity_Id,
                        (act, sur) => new
                        {
                            listActivities = act,
                            listSurveys = sur
                        })
                     .SelectMany(
                         x => x.listSurveys.DefaultIfEmpty(new Survey()),
                         (act, sur) => new ActivityDto
                         {

                             Answers = sur.Answers,
                             Id = act.listActivities.Id,
                             Schedule = act.listActivities.Schedule,
                             Created_at = act.listActivities.Created_at,
                             Status = act.listActivities.Status,
                             Title = act.listActivities.Title,
                             Property = new PropertyDto() { Id = act.listActivities.Property.Id, Title = act.listActivities.Property.Title, Address = act.listActivities.Property.Address, Description = act.listActivities.Property.Description }

                         }
                         );
            return Ok(qry);
        }

        [HttpGet("GetActivity/{activityId:int}")]
        public IActionResult GetActivity(int activityId)
        {
            var itemActivity = _actService.GetActivity(activityId);

            if (itemActivity == null)
            {
                return NotFound();
            }

            var activity = _mapper.Map<ActivityDto>(itemActivity);

            return Ok(activity);
        }

        [HttpPost("CreateActivity")]
        public IActionResult CreateActivity([FromBody] ActivityDto ActivityDto)
        {
            if (ActivityDto == null)
            {
                return BadRequest(ModelState);
            }

            var activity = _mapper.Map<Activity>(ActivityDto);

            var act = _actService.CreateActivity(activity);

            if (act != "Ok")
            {
                ModelState.AddModelError("", $"{act}.");
                return StatusCode(500, ModelState);
            }

            return Ok(ActivityDto);
        }
        
        [HttpGet("GetActivitiesForDay")]
        public IActionResult GetActivitiesForDay([FromQuery] ActivityDtoRequest activityDtoResch)
        {
            var listActivities = _actService.GetActivitiesForDay(activityDtoResch.idActivity, activityDtoResch.newScheduleDay);

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }
            //var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);

            var listSurveys = _surveyService.GetSurveys();

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }

            var qry = listActivities.GroupJoin(
                        listSurveys,
                        lAct => lAct.Id,
                        lSur => lSur.Activity_Id,
                        (act, sur) => new
                        {
                            listActivities = act,
                            listSurveys = sur
                        })
                     .SelectMany(
                         x => x.listSurveys.DefaultIfEmpty(new Survey()),
                         (act, sur) => new ActivityDto
                         {

                             Answers = sur.Answers,
                             Id = act.listActivities.Id,
                             Schedule = act.listActivities.Schedule,
                             Created_at = act.listActivities.Created_at,
                             Status = act.listActivities.Status,
                             Title = act.listActivities.Title,
                             Property = new PropertyDto() { Id = act.listActivities.Property.Id, Title = act.listActivities.Property.Title, Address = act.listActivities.Property.Address, Description = act.listActivities.Property.Description }

                         }
                         );

            return Ok(qry);
        }

        [HttpPut("RescheduleActivitie")]
        public async Task<ActionResult> RescheduleActivity([FromBody] ActivityReschDto activityDtoResch)
        {
            var actResch = await _actService.RescheduleActivity(activityDtoResch.Id, activityDtoResch.Schedule);

            //if (await _actService.RescheduleActivity(activityDtoResch.idActivity, activityDtoResch.newScheduleDay) != "Ok")
            if (actResch != "Ok")
                {
                //ModelState.AddModelError("", $"Algo salió mal al reagendar la actividad.");
                ModelState.AddModelError("", actResch);
                return StatusCode(500, ModelState);
            }


            var itemActivity = _actService.GetActivity(activityDtoResch.Id);
            var activity = _mapper.Map<ActivityDto>(itemActivity);

            return Ok(activity);
        }

        [HttpDelete("CancelActivitie")]
        public async Task<ActionResult> CancelActivity([FromBody] ActivityDtoRequest activityDtoResch)
        {
            var actCanc = await _actService.CancelActivity(activityDtoResch.idActivity);
            if (actCanc != "Ok")
            {
                ModelState.AddModelError("", actCanc);
                return StatusCode(500, ModelState);
            }

            var itemActivity = _actService.GetActivity(activityDtoResch.idActivity);
            var activity = _mapper.Map<ActivityDto>(itemActivity);

            return Ok(activity);

        }
        
        [HttpGet("GetActivitiesAll")]
        public IActionResult GetActivitiesAll()
        {
            var listActivities = _actService.GetActivitiesAll();
            var listSurveys = _surveyService.GetSurveys();

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }

            var qry = listActivities.GroupJoin(
                        listSurveys,
                        lAct => lAct.Id,
                        lSur => lSur.Activity_Id,
                        (act, sur) => new 
                        {
                           listActivities = act, listSurveys = sur
                        })
                     .SelectMany(
                         x => x.listSurveys.DefaultIfEmpty(new Survey()),
                         (act, sur) => new ActivityDto
                            {

                             Answers = sur.Answers,
                             Id = act.listActivities.Id,
                             Schedule = act.listActivities.Schedule,
                             Created_at = act.listActivities.Created_at,
                             Status = act.listActivities.Status,
                             Title = act.listActivities.Title,
                             Property = new PropertyDto() { Id = act.listActivities.Property.Id, Title = act.listActivities.Property.Title, Address = act.listActivities.Property.Address, Description = act.listActivities.Property.Description }

                         }
                         );

            //ActivityDto y = listActivities.SelectMany
            // (
            //     act => listSurveys.Where(sur => act.Id == sur.Activity_Id).DefaultIfEmpty(),
            //     (act, sur) => new ActivityDto
            //     {
            //         Answers = sur.Answers,
            //         Id = act.Id,
            //         Schedule = act.Schedule,
            //         Created_at = act.Created_at,
            //         Status = act.Status,
            //         Property = new PropertyDto() { Id = act.Property.Id, Title = act.Property.Title, Address = act.Property.Address}
            //     }
            // ) ;


            //var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);

            return Ok(qry);
        }

        [HttpGet("GetActivitiesForStatus")]
        public IActionResult GetActivitiesForStatus([FromQuery] ActivityDtoRequest activityListStatusDto)
        {
            var listActivities = _actService.GetActivitiesForStatus(activityListStatusDto.Status, activityListStatusDto.IniSchedule, activityListStatusDto.EndSchedule);

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }
            //var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);

            //return Ok(itemActivity);

            //var listActivities = _actService.GetActivitiesAll();
            var listSurveys = _surveyService.GetSurveys();

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }

            var qry = listActivities.GroupJoin(
                        listSurveys,
                        lAct => lAct.Id,
                        lSur => lSur.Activity_Id,
                        (act, sur) => new
                        {
                            listActivities = act,
                            listSurveys = sur
                        })
                     .SelectMany(
                         x => x.listSurveys.DefaultIfEmpty(new Survey()),
                         (act, sur) => new ActivityDto
                         {

                             Answers = sur.Answers,
                             Id = act.listActivities.Id,
                             Schedule = act.listActivities.Schedule,
                             Created_at = act.listActivities.Created_at,
                             Status = act.listActivities.Status,
                             Title = act.listActivities.Title,
                             Property = new PropertyDto() { Id = act.listActivities.Property.Id, Title = act.listActivities.Property.Title, Address = act.listActivities.Property.Address, Description = act.listActivities.Property.Description }

                         }
                         );

            return Ok(qry);

        }

    }
}
