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
using TherapyAPI.Helpers;
using TherapyAPI.Models;

namespace TherapyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentTypesController : ControllerBase
    {
        private IAppointmentTypesRepository _appointmentTypesRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AppointmentTypesController(IAppointmentTypesRepository appointmentTypesRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _appointmentTypesRepository = appointmentTypesRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create(AppointmentTypes appointmentTypes)
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

            var name = appointmentTypes.Name.Replace(" ", "").ToLower();

            if(_appointmentTypesRepository.GetByName(name) == true)
            {
                return BadRequest("This appointment type already exists.");
            }

            appointmentTypes.Code = appointmentTypes.Name.ToUpper();

            if (appointmentTypes.Code.Any(x => Char.IsWhiteSpace(x)))
            {
                appointmentTypes.Code = Regex.Replace(appointmentTypes.Code, @"\s+", "");
            }

            _appointmentTypesRepository.Create(appointmentTypes);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentTypes>> Get()
        {
            return await _appointmentTypesRepository.GetAllAsync();
        }

        [HttpPut]
        public IActionResult Edit(AppointmentTypes appointmentTypes)
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

            _appointmentTypesRepository.Update(appointmentTypes);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appointmentType = _appointmentTypesRepository.GetById(Id);
            if (appointmentType == null)
            {
                return NotFound("Wrong id.");
            }

            _appointmentTypesRepository.Delete(Id);
            return Ok("Billing deleted.");
        }
    }
}