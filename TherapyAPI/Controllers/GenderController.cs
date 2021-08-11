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
        public async Task<IActionResult> Create(GenderDto genderDto)
        {
            var gender = _mapper.Map<Gender>(genderDto);
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

            int result = await _genderRepository.Create(gender).ConfigureAwait(false);

            if (result == 1)
                return Ok("Gender created.");

            return StatusCode(500, "There was a problem trying to create gender.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genders = await _genderRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<IEnumerable<GenderDto>>(genders.ToList()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(GenderDto genderDto)
        {
            var gender = _mapper.Map<Gender>(genderDto);
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

            int result = await _genderRepository.Update(gender).ConfigureAwait(false);

            if (result == 1)
                return Ok("Gender updated.");

            return StatusCode(500, "There was a problem trying to update gender.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var appointment = await _genderRepository.GetById(id).ConfigureAwait(false);
            if (appointment == null)
            {
                return NotFound($"There is not gender with ID {id}.");
            }

            int result = await _genderRepository.Delete(id).ConfigureAwait(false);

            if(result == 1)
                return Ok("Gender deleted.");

            return StatusCode(500, "There was a problem trying to delete gender.");
        }
    }
}
