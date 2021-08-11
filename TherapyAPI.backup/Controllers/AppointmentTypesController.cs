using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TherapyAPI.Dto;
using TherapyAPI.Helpers;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentTypeController : ControllerBase
    {
        private IAppointmentTypeRepository _appointmentTypeRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AppointmentTypeController(IAppointmentTypeRepository appointmentTypeRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _appointmentTypeRepository = appointmentTypeRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create(AppointmentType appointmentType)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var name = appointmentType.Name.Trim().ToLower();

            if (_appointmentTypeRepository.GetByName(name) == true)
            {
                return BadRequest("This appointment type already exists.");
            }

            _appointmentTypeRepository.Create(appointmentType);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentTypeDto>> Get()
        {
            var appointmentTypes = await _appointmentTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentTypeDto>>(appointmentTypes);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(AppointmentType appointmentType)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            _appointmentTypeRepository.Update(appointmentType);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appointmentType = _appointmentTypeRepository.GetById(Id);
            if (appointmentType == null)
            {
                return NotFound("Wrong id.");
            }

            _appointmentTypeRepository.Delete(Id);
            return Ok("Appointment Type deleted.");
        }
    }
}