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
    public class GenderController : ControllerBase
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Gender> _validator;

        public GenderController(IGenderRepository genderRepository, IMapper mapper, IValidator<Gender> validator)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public IActionResult Create(Gender gender)
        {
            var validationResult = _validator.Validate(gender);

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

            _genderRepository.Create(gender);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<GenderDto>> Get()
        {
            var genders = await _genderRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<GenderDto>>(genders);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Gender gender)
        {
            var validationResult = _validator.Validate(gender);

            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            else if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => $"{x.PropertyName} failed validation: ${x.ErrorMessage}.");
                return BadRequest(string.Join(";", validationErrors));
            }

            _genderRepository.Update(gender);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appointment = _genderRepository.GetById(Id);
            if (appointment == null)
            {
                return NotFound("Invalid ID.");
            }

            _genderRepository.Delete(Id);
            return Ok("Gender deleted.");
        }
    }
}
