using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TherapyAPI.Dto;
using TherapyAPI.Helpers;
using TherapyAPI.Models;

namespace TherapyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ClientController(IClientRepository clientRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Register(Client client)
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

            client.DateOfBirth = Convert.ToDateTime(client.DateOfBirth.ToShortDateString());
            this._clientRepository.Create(client);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<ClientDto>> Get()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>> (clients);
        }

        [HttpPut]
        public IActionResult Edit(Client client)
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

            _clientRepository.Update(client);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var therapist = _clientRepository.GetById(Id);
            if (therapist == null)
            {
                return NotFound("Wrong id.");
            }

            _clientRepository.Delete(Id);
            return Ok("User deleted.");
        }
    }
}