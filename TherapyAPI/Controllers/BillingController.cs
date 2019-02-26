using System;
using System.Collections.Generic;
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
    public class BillingController : ControllerBase
    {
        private IBillingRepository _billingRepository;
        private IClientRepository _clientRepository;
        private ITherapistRepository _therapistRepository;
        private IAppointmentRepository _appointmentRepository;
        private IAppointmentTypeRepository _appointmentTypeRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public BillingController(IBillingRepository billingRepository, IClientRepository clientRepository,
        ITherapistRepository therapistRepository, IAppointmentRepository appointmentRepository, IAppointmentTypeRepository appointmentTypeRepository,
        IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _billingRepository = billingRepository;
            _clientRepository = clientRepository;
            _therapistRepository = therapistRepository;
            _appointmentRepository = appointmentRepository;
            _appointmentTypeRepository = appointmentTypeRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create(Billing billing)
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

            _billingRepository.Create(billing);
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IEnumerable<Billing>> Get()
        {
            var billings = await _billingRepository.GetAllAsync();
            foreach (var item in billings)
            {
                item.Appointment = await _appointmentRepository.GetById(item.AppointmentId);
                item.Appointment.Client = await _clientRepository.GetById(item.Appointment.ClientId);
                item.Appointment.Therapist = await _therapistRepository.GetById(item.Appointment.TherapistId);
                item.Appointment.AppointmentType = await _appointmentTypeRepository.GetById(item.Appointment.AppointmentTypeId);
            }
            return billings;
            //return _mapper.Map<IEnumerable<BillingDto>> (billings);
        }

        [HttpPut]
        public IActionResult Edit(Billing billing)
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

            _billingRepository.Update(billing);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var billing = _billingRepository.GetById(Id);
            if (billing == null)
            {
                return NotFound("Wrong id.");
            }

            _billingRepository.Delete(Id);
            return Ok("Billing deleted.");
        }
    }
}