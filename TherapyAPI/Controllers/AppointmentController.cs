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
        public IActionResult Create(Appointment appointment)
        {
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

            _appointmentRepository.Create(appointment);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentDto>> Get()
        {
            //var includes = new List<string>() { "AppointmentType" };

            var appointments = await _appointmentRepository.GetAllAsync().ConfigureAwait(false);

            //foreach (var item in appointments)
            //{
            //    item.AppointmentType = await _appointmentTypeRepository.GetById(item.AppointmentType.Id).ConfigureAwait(false);
            //    item.Client = await _clientRepository.GetById(item.Client.Id).ConfigureAwait(false);
            //    item.Therapist = await _therapistRepository.GetById(item.Therapist.Id).ConfigureAwait(false);
            //}

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Appointment appointment)
        {
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

            _appointmentRepository.Update(appointment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appointment = _appointmentRepository.GetById(Id);
            if (appointment == null)
            {
                return NotFound("Invalid ID.");
            }

            _appointmentRepository.Delete(Id);
            return Ok("Appointment deleted.");
        }
    }
}