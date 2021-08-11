using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyAPI.Dto;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;
using TherapyAPI.Validators;

namespace TherapyAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentTypeController : ControllerBase
    {
        private readonly IAppointmentTypeRepository _appointmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AppointmentType> _validator;

        public AppointmentTypeController(IAppointmentTypeRepository appointmentTypeRepository, IMapper mapper, IValidator<AppointmentType> validator)
        {
            _appointmentTypeRepository = appointmentTypeRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public IActionResult Create(AppointmentType appointmentType)
        {
            var validationResult = _validator.Validate(appointmentType);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            else if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => $"{x.PropertyName} failed validation: ${x.ErrorMessage}.");
                return BadRequest(string.Join(";", validationErrors));
            }

            var name = appointmentType.Name.Trim().ToLower();

            if (_appointmentTypeRepository.GetByName(name))
            {
                return BadRequest("This appointment type already exists.");
            }

            _appointmentTypeRepository.Create(appointmentType);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentTypeDto>> Get()
        {
            var appointmentTypes = await _appointmentTypeRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<AppointmentTypeDto>>(appointmentTypes);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(AppointmentType appointmentType)
        {
            var validationResult = _validator.Validate(appointmentType);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            else if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => $"{x.PropertyName} failed validation: ${x.ErrorMessage}.");
                return BadRequest(string.Join(";", validationErrors));
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