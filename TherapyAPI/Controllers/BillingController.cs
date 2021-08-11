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
        public async Task<IActionResult> Create(BillingDto billingDto)
        {
            var billing = _mapper.Map<Billing>(billingDto);
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

            int result = await _billingRepository.Create(billing).ConfigureAwait(false);

            if (result == 1)
                return Created($"~/api/billing/{billing.Id}", new { id = billing.Id });

            return BadRequest("There was an error creating billing.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var billings = await _billingRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<List<BillingDto>>(billings.ToList()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(BillingDto billingDto)
        {
            var billing = _mapper.Map<Billing>(billingDto);
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

            int result = await _billingRepository.Update(billing).ConfigureAwait(false);

            if (result == 1)
                return Ok("Billing updated.");

            return BadRequest("There was an error updating billing.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var billing = await _billingRepository.GetById(id).ConfigureAwait(false);
            if (billing == null)
            {
                return NotFound($"There is no billing with ID {id}.");
            }

            int result = await _billingRepository.Delete(id).ConfigureAwait(false);

            if (result == 1)
                return Ok("Billing deleted.");

            return BadRequest("There was an error trying to delete the billing.");
        }
    }
}