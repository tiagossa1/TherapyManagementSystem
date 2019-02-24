using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TherapyAPI.Data.Repository;
using TherapyAPI.Dto;
using TherapyAPI.Helpers;
using TherapyAPI.Models;

namespace TherapyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentTypeController : ControllerBase
    {
        private IAppointmentTypeRepository _AppointmentTypeRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AppointmentTypeController(IAppointmentTypeRepository AppointmentTypeRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _AppointmentTypeRepository = AppointmentTypeRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create(AppointmentType AppointmentType)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return BadRequest(errors);
            }

            var name = AppointmentType.Name.Replace(" ", "").ToLower();

            if(_AppointmentTypeRepository.GetByName(name) == true)
            {
                return BadRequest("This appointment type already exists.");
            }

            AppointmentType.Code = AppointmentType.Name.ToUpper();

            if (AppointmentType.Code.Any(x => Char.IsWhiteSpace(x)))
            {
                AppointmentType.Code = Regex.Replace(AppointmentType.Code, @"\s+", "");
            }

            _AppointmentTypeRepository.Create(AppointmentType);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentTypeDto>> Get()
        {
            var appointmentTypes = await _AppointmentTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentTypeDto>> (appointmentTypes);
        }

        [HttpPut]
        public IActionResult Edit(AppointmentType AppointmentType)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return BadRequest(errors);
            }

            _AppointmentTypeRepository.Update(AppointmentType);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appointmentType = _AppointmentTypeRepository.GetById(Id);
            if (appointmentType == null)
            {
                return NotFound("Wrong id.");
            }

            _AppointmentTypeRepository.Delete(Id);
            return Ok("Appointment Type deleted.");
        }
    }
}