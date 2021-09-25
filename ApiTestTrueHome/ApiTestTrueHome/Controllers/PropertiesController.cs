﻿using ApiTestTrueHome.Models;
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
    [Route("Properties")]
    [ApiController]
    public class PropertiesController : Controller
    {
        private readonly IPropertyService _ctService;
        private readonly IMapper _mapper;

        public PropertiesController(IPropertyService ctService, IMapper mapper)
        {
            _ctService = ctService;
            _mapper = mapper;
        }

        [HttpGet("GetProperties")]
        public async Task<ActionResult> GetProperties()
        {
            var listProperties = await _ctService.GetProperties();

            var listPropertiesDto = new List<PropertyDto>();

            foreach(var list in listProperties)
            {
                listPropertiesDto.Add(_mapper.Map<PropertyDto>(list));
            }

            return Ok(listPropertiesDto);
        }

        [HttpGet("GetProperty/{propertyId:int}")]
        public IActionResult GetProperty(int propertyId)
        {
            var itemProperty = _ctService.GetProperty(propertyId);

            if (itemProperty == null)
            {
                return NotFound();
            }

            var itemPropertyDto = _mapper.Map<PropertyDto>(itemProperty);
            return Ok(itemProperty);
        }
        
        [HttpPost("CreateProperty")]
        public IActionResult CreateProperty([FromBody] PropertyDto propertyDto)
        {
            if (propertyDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctService.ExistProperty(propertyDto.Title))
            {
                ModelState.AddModelError("","Ya existe una propiedad con ese title.");
                return StatusCode(404, ModelState);
            }

            var property = _mapper.Map<Property>(propertyDto);

            property.Status = "Active";
            property.Created_at = DateTime.UtcNow;
            property.Updated_at = DateTime.UtcNow;

            if (!_ctService.CreateProperty(property))
            {
                ModelState.AddModelError("",$"Algo salió mal al guardar el registro {property.Title}.");
                return StatusCode(500, ModelState);
            }

            //return CreatedAtRoute("GetProperty", new {propertyId = property.Id}, property);
            return NoContent();
        }

        [HttpPut("UpdateProperty")]
        public async Task<ActionResult> UpdateProperty([FromBody] PropertyDto propertyDto)
        { 
            if (propertyDto == null)
            {
                return BadRequest(ModelState);
            }
            
            if (!_ctService.ExistProperty(propertyDto.Id))
            {
                return NotFound();
            }

            var itemProperty = _ctService.GetProperty(propertyDto.Id);
            var propertyCreated = itemProperty.Created_at;
            var propertyStatus = itemProperty.Status;

            var property = _mapper.Map<Property>(propertyDto);
            property.Updated_at = DateTime.UtcNow;
            property.Created_at = propertyCreated;
            property.Status = propertyStatus;

            if (await _ctService.UpdateProperty(property)==0)
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el registro {property.Title}.");
                return StatusCode(500, ModelState);
            }

            //return CreatedAtRoute("GetProperty", new { propertyId = property.Id }, property);
            return NoContent();

        }

        [HttpDelete("DisableProperty")]
        public async Task<IActionResult> DeleteProperty([FromBody] PropertyDto propertyDto)
        {

            if (!_ctService.ExistProperty(propertyDto.Id))
            {
                return NotFound();
            }

            var itemProperty = _ctService.GetProperty(propertyDto.Id);
            var propertyCreated = itemProperty.Created_at;
            var propertyUdated = itemProperty.Updated_at;
            var propertyTitle = itemProperty.Title;
            var propertyAddress = itemProperty.Address;
            var propertyDescription = itemProperty.Description;

            if (itemProperty.Status == "Disabled")
            {
                ModelState.AddModelError("", $"La propiedad {itemProperty.Title} ya está deshabilitada.");
                return StatusCode(404, ModelState);
            }

            var property = _mapper.Map<Property>(propertyDto);
            property.Updated_at = propertyUdated;
            property.Created_at = propertyCreated;
            property.Disabled_at = DateTime.UtcNow;
            property.Title = propertyTitle;
            property.Address = propertyAddress;
            property.Description = propertyDescription;
            property.Status = "Disabled";


            if (await _ctService.UpdateProperty(property) == 0)
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el registro {property.Title}.");
                return StatusCode(500, ModelState);
            }

            //return CreatedAtRoute("GetProperty", new { propertyId = property.Id }, property);
            return NoContent();


        }
    }
}
