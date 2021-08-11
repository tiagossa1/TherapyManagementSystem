using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TherapyAPI.Dto;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentRepository _appointmentRepository;
        private IAppointmentTypeRepository _appointmentTypeRepository;
        private IClientRepository _clientRepository;
        private ITherapistRepository _therapistRepository;
        private IMapper _mapper;

        public AppointmentController(IAppointmentRepository appointmentRepository, IAppointmentTypeRepository appointmentTypeRepository, IClientRepository clientRepository, ITherapistRepository therapistRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentTypeRepository = appointmentTypeRepository;
            _clientRepository = clientRepository;
            _therapistRepository = therapistRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            _appointmentRepository.Create(appointment);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentDto>> Get()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            foreach (var item in appointments)
            {
                item.AppointmentType = await _appointmentTypeRepository.GetById(item.AppointmentType.Id) ?? null;
                item.Client = await _clientRepository.GetById(item.Client.Id) ?? null;
                item.Therapist = await _therapistRepository.GetById(item.Therapist.Id) ?? null;
            }

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
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