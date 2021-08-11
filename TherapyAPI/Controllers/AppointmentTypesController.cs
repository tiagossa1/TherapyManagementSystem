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

namespace TherapyAPI.Controllers
{
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
        public async Task<IActionResult> Create(AppointmentTypeDto appointmentTypeDto)
        {
            var appointmentType = _mapper.Map<AppointmentType>(appointmentTypeDto);
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

            if (await _appointmentTypeRepository.AnyByName(appointmentType.Name).ConfigureAwait(false))
            {
                return BadRequest("This appointment type already exists.");
            }

            await _appointmentTypeRepository.Create(appointmentType).ConfigureAwait(false);
            return Ok("Appointment type created.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var appointmentTypes = await _appointmentTypeRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<List<AppointmentTypeDto>>(appointmentTypes.ToList()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(AppointmentTypeDto appointmentTypeDto)
        {
            var appointmentType = _mapper.Map<AppointmentType>(appointmentTypeDto);
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

            await _appointmentTypeRepository.Update(appointmentType).ConfigureAwait(false);
            return Ok("Appointment type updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var appointmentType = await _appointmentTypeRepository.GetById(id).ConfigureAwait(false);
            if (appointmentType == null)
            {
                return NotFound($"There was no appointment type with ID {id}.");
            }

            await _appointmentTypeRepository.Delete(id).ConfigureAwait(false);
            return Ok("Appointment type deleted.");
        }
    }
}