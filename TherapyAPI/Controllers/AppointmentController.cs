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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Appointment> _validator;

        public AppointmentController(IAppointmentRepository appointmentRepository, IValidator<Appointment> validator, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            var validationResult = _validator.Validate(appointment);

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

            int result = await _appointmentRepository.Create(appointment).ConfigureAwait(false);

            if (result == 1)
                return Ok("Appointment created.");

            return BadRequest("There was an error trying to create the appointment.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<List<AppointmentDto>>(appointments.ToList()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(AppointmentDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            var validationResult = _validator.Validate(appointment);

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

            int result = await _appointmentRepository.Update(appointment).ConfigureAwait(false);

            if (result == 1)
                return Ok("Appointment updated.");

            return BadRequest("There was an error trying to update the appointment.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var appointment = await _appointmentRepository.GetById(id).ConfigureAwait(false);
            if (appointment == null)
            {
                return NotFound($"There was no appointment with ID {id}.");
            }

            int result = await _appointmentRepository.Delete(id).ConfigureAwait(false);

            if (result == 1)
                return Ok("Appointment deleted.");

            return BadRequest("There was an error trying to delete the appointment.");
        }
    }
}