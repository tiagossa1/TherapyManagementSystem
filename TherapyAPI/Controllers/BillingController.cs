using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingRepository _billingRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Billing> _validator;
        public BillingController(IBillingRepository billingRepository, IMapper mapper, IValidator<Billing> validator)
        {
            _billingRepository = billingRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public IActionResult Create(Billing billing)
        {
            var validationResult = _validator.Validate(billing);

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

            _billingRepository.Create(billing);
            return new ObjectResult(billing) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        public async Task<IEnumerable<BillingDto>> Get()
        {
            var billings = await _billingRepository.GetAllAsync().ConfigureAwait(false);

            //foreach (var item in billings)
            //{
            //    item.Appointment = await _appointmentRepository.GetById(item.Appointment.Id).ConfigureAwait(false);
            //    item.Appointment.Client = await _clientRepository.GetById(item.Appointment.Client.Id).ConfigureAwait(false);
            //    item.Appointment.Therapist = await _therapistRepository.GetById(item.Appointment.Therapist.Id).ConfigureAwait(false);
            //    item.Appointment.AppointmentType = await _appointmentTypeRepository.GetById(item.Appointment.AppointmentType.Id).ConfigureAwait(false);
            //}

            return _mapper.Map<IEnumerable<BillingDto>>(billings);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Billing billing)
        {
            var validationResult = _validator.Validate(billing);

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

            _billingRepository.Update(billing);
            return Ok();
        }

        [HttpDelete("{id}")]
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