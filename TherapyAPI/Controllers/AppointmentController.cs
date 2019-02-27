using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TherapyAPI.Data.Repository;
using TherapyAPI.Dto;
using TherapyAPI.Helpers;
using TherapyAPI.Models;

namespace TherapyAPI.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase {
        private IAppointmentRepository _appointmentRepository;
        private IAppointmentTypeRepository _appointmentTypeRepository;
        private IClientRepository _clientRepository;
        private ITherapistRepository _therapistRepository;
        private IMapper _mapper;

        public AppointmentController (IAppointmentRepository appointmentRepository, IAppointmentTypeRepository appointmentTypeRepository, IClientRepository clientRepository, ITherapistRepository therapistRepository, IMapper mapper) {
            _appointmentRepository = appointmentRepository;
            _appointmentTypeRepository = appointmentTypeRepository;
            _clientRepository = clientRepository;
            _therapistRepository = therapistRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create (Appointment appointment) {
            if (!ModelState.IsValid) {
                var errors = new List<string> ();
                foreach (var state in ModelState) {
                    foreach (var error in state.Value.Errors) {
                        errors.Add (error.ErrorMessage);
                    }
                }

                return BadRequest (errors);
            }

            _appointmentRepository.Create (appointment);
            return Ok ();
        }

        [HttpGet]
        public async Task<IEnumerable<AppointmentDto>> Get () {
            var appointments = await _appointmentRepository.GetAllAsync ();

            foreach (var item in appointments) {
                item.AppointmentType = await _appointmentTypeRepository.GetById (item.AppointmentTypeId) ?? null;
                item.Client = await _clientRepository.GetById (item.ClientId) ?? null;
                item.Therapist = await _therapistRepository.GetById (item.TherapistId) ?? null;
            }
            return _mapper.Map<IEnumerable<AppointmentDto>> (appointments);
        }

        [HttpPut("{id}")]
        public IActionResult Edit (Appointment appointment) {
            if (!ModelState.IsValid) {
                var errors = new List<string> ();
                foreach (var state in ModelState) {
                    foreach (var error in state.Value.Errors) {
                        errors.Add (error.ErrorMessage);
                    }
                }

                return BadRequest (errors);
            }

            _appointmentRepository.Update (appointment);
            return Ok ();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (Guid Id) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var appointment = _appointmentRepository.GetById (Id);
            if (appointment == null) {
                return NotFound ("Wrong id.");
            }

            _appointmentRepository.Delete (Id);
            return Ok ("Appointment deleted.");
        }
    }
}