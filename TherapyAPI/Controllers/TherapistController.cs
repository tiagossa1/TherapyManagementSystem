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
    public class TherapistController : ControllerBase
    {
        private readonly ITherapistRepository _therapistRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Therapist> _validator;

        public TherapistController(ITherapistRepository therapistRepository, IMapper mapper, IValidator<Therapist> validator)
        {
            _therapistRepository = therapistRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("register")]
        public IActionResult Register(TherapistDto therapistDto)
        {
            var therapist = _mapper.Map<Therapist>(therapistDto);
            var validationResult = _validator.Validate(therapist);

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

            Therapist result = _therapistRepository.Create(therapist, therapistDto.Password);

            if (result != null)
                return Ok("Therapist created.");

            return StatusCode(500, "There was a problem trying to create therapist.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var therapists = await _therapistRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<List<TherapistDto>>(therapists.ToList()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(TherapistDto therapistDto)
        {
            var therapist = _mapper.Map<Therapist>(therapistDto);
            var validationResult = _validator.Validate(therapist);

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

            int result = await _therapistRepository.Update(therapist).ConfigureAwait(false);

            if (result == 1)
                return Ok("Therapist updated.");

            return StatusCode(500, "There was a problem trying to update therapist.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var therapist = await _therapistRepository.GetById(id).ConfigureAwait(false);
            if (therapist == null)
            {
                return NotFound($"There was no therapist with ID {id}.");
            }

            int result = await _therapistRepository.Delete(id).ConfigureAwait(false);

            if (result == 1)
                return Ok("Therapist deleted.");

            return StatusCode(500, "There was a problem trying to delete therapist.");
        }
    }
}