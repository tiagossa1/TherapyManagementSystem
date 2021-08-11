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
    public class CivilStatusController : ControllerBase
    {
        private readonly ICivilStatusRepository _civilStatusRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CivilStatus> _validator;

        public CivilStatusController(ICivilStatusRepository civilStatusRepository, IMapper mapper, IValidator<CivilStatus> validator)
        {
            _civilStatusRepository = civilStatusRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public IActionResult Create(CivilStatus civilStatus)
        {
            var validationResult = _validator.Validate(civilStatus);

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

            _civilStatusRepository.Create(civilStatus);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<CivilStatusDto>> Get()
        {
            var civilStatuses = await _civilStatusRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<CivilStatusDto>>(civilStatuses);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(CivilStatus civilStatus)
        {
            var validationResult = _validator.Validate(civilStatus);

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

            _civilStatusRepository.Update(civilStatus);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appointment = _civilStatusRepository.GetById(Id);
            if (appointment == null)
            {
                return NotFound("Invalid ID.");
            }

            _civilStatusRepository.Delete(Id);
            return Ok("Civil status deleted.");
        }
    }
}
