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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Client> _validator;

        public ClientController(IClientRepository clientRepository, IMapper mapper, IValidator<Client> validator)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            var validationResult = _validator.Validate(client);

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

            int result = await _clientRepository.Create(client).ConfigureAwait(false);

            if (result == 1)
                return Created("~/api/client/register", new { id = clientDto.Id });

            return StatusCode(500, "There was a problem trying to create client.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clients = await _clientRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<List<ClientDto>>(clients.ToList()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            var validationResult = _validator.Validate(client);

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

            int result = await _clientRepository.Update(client).ConfigureAwait(false);

            if (result == 1)
                return Ok("Client updated.");

            return StatusCode(500, "There was a problem trying to update client.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var client = await _clientRepository.GetById(id).ConfigureAwait(false);
            if (client == null)
            {
                return NotFound($"There is not client with ID {id}.");
            }

            int result = await _clientRepository.Delete(id).ConfigureAwait(false);

            if (result == 1)
                return Ok("Client deleted.");

            return StatusCode(500, "There was a problem trying to delete client.");
        }
    }
}