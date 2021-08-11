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
        public async Task<IActionResult> Create(CivilStatusDto civilStatusDto)
        {
            var civilStatus = _mapper.Map<CivilStatus>(civilStatusDto);
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

            int result = await _civilStatusRepository.Create(civilStatus).ConfigureAwait(false);

            if (result == 1)
                return Created($"~/api/civilstatus/{civilStatusDto.Id}", new { id = civilStatus.Id });

            return StatusCode(500, "There was a problem trying to create civil status.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var civilStatuses = await _civilStatusRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<List<CivilStatusDto>>(civilStatuses.ToList()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(CivilStatus civilStatus)
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

            int result = await _civilStatusRepository.Update(civilStatus).ConfigureAwait(false);

            if (result == 1)
                return Ok("Civil Status edited.");

            return StatusCode(500, "There was a problem trying to update civil status.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The model that you sent is invalid.");
            }

            var appointment = await _civilStatusRepository.GetById(Id).ConfigureAwait(false);
            if (appointment == null)
            {
                return NotFound("Invalid ID.");
            }

            int result = await _civilStatusRepository.Delete(Id).ConfigureAwait(false);

            if (result == 1)
                return Ok("Civil status deleted.");

            return StatusCode(500, "There was a problem trying to delete civil status.");
        }
    }
}
