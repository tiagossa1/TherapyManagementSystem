using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TherapyAPI.Dto;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;
using TherapyAPI.Validators;

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
        public IActionResult Register(Client client)
        {
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

            _clientRepository.Create(client);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<ClientDto>> Get()
        {
            var clients = await _clientRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Client client)
        {
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

            _clientRepository.Update(client);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound("Wrong id.");
            }

            _clientRepository.Delete(id);
            return Ok("User deleted.");
        }
    }
}