using System;
using System.Collections.Generic;
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
    public class BillingController : ControllerBase
    {
        private IBillingRepository _billingRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public BillingController(IBillingRepository billingRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _billingRepository = billingRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Register(Billing billing)
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
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Billing>> Get()
        {
            return await _billingRepository.GetAllAsync();
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