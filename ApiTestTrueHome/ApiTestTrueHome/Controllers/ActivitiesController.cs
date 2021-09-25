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
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityService actService, IMapper mapper)
        {
            _actService = actService;
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
            var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);
        
            return Ok(itemActivity);
        }

        [HttpGet("GetActivity/{activityId:int}")]
        public IActionResult GetActivity(int activityId)
        {
            var itemActivity = _actService.GetActivity(activityId);

            if (itemActivity == null)
            {
                return NotFound();
            }

            return Ok(itemActivity);
        }

        [HttpPost("CreateActivity")]
        public IActionResult CreateActivity([FromBody] ActivityDto ActivityDto)
        {
            if (ActivityDto == null)
            {
                return BadRequest(ModelState);
            }

            var activity = _mapper.Map<Activity>(ActivityDto);

            if (!_actService.CreateActivity(activity))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el registro {activity.Title}.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        
        [HttpGet("GetActivitiesForDay/{propertyId}/{scheduleDay}")]
        public IActionResult GetActivitiesForDay(int propertyId, DateTime scheduleDay)
        {
            var listActivities = _actService.GetActivitiesForDay(propertyId, scheduleDay);

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }
            var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);

            return Ok(itemActivity);
        }

        [HttpPut("RescheduleActivitie")]
        public async Task<ActionResult> RescheduleActivity(int idActivity, DateTime newScheduleDay)
        {
            
            var itemActivity = _actService.GetActivity(idActivity);

            //var activity = _mapper.Map<Activity>(ActivityDto);

            if (await _actService.RescheduleActivity(idActivity, newScheduleDay) == 0)
            {
                ModelState.AddModelError("", $"Algo salió mal al reagendar la actividad.");
                return StatusCode(500, ModelState);
            }

            //return CreatedAtRoute("GetProperty", new { propertyId = property.Id }, property);
            return NoContent();

        }

        [HttpDelete("CancelActivitie")]
        public async Task<ActionResult> CancelActivity(int idActivity)
        {

            var itemActivity = _actService.GetActivity(idActivity);

            //var activity = _mapper.Map<Activity>(ActivityDto);

            if (await _actService.CancelActivity(idActivity) == 0)
            {
                ModelState.AddModelError("", $"Algo salió mal al cancelar la actividad.");
                return StatusCode(500, ModelState);
            }

            //return CreatedAtRoute("GetProperty", new { propertyId = property.Id }, property);
            return NoContent();

        }
        
        [HttpGet("GetActivitiesAll")]
        public IActionResult GetActivitiesAll()
        {
            var listActivities = _actService.GetActivitiesAll();

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }
            var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);

            return Ok(itemActivity);
        }

        [HttpGet("GetActivitiesForStatus")]
        public IActionResult GetActivitiesForStatus([FromQuery] ActivityListStatusDto activityListStatusDto)
        {
            var listActivities = _actService.GetActivitiesForStatus(activityListStatusDto.Status, activityListStatusDto.IniSchedule, activityListStatusDto.EndSchedule);

            if (listActivities == null || listActivities.Count == 0)
            {
                return NotFound();
            }
            var itemActivity = _mapper.Map<List<ActivityDto>>(listActivities);

            return Ok(itemActivity);
        }

    }
}
