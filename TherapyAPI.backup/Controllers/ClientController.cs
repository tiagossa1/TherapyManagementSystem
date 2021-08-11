using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TherapyAPI.Dto;
using TherapyAPI.Helpers;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;
using TherapyAPI.Validators;

namespace TherapyAPI.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase {
        private IClientRepository _clientRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private ClientValidator _validator;

        public ClientController (IClientRepository clientRepository, IMapper mapper, IOptions<AppSettings> appSettings) {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _validator = new ClientValidator ();
        }

        [HttpPost]
        public IActionResult Register (Client client) {
            ValidationResult result = _validator.Validate (client);

            if (!ModelState.IsValid) {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest (errors);
            }

            if (!result.IsValid) {
                var validationErrors = result.Errors.Select(x => $"{x.PropertyName} failed validation: ${x.ErrorMessage}.");
                return BadRequest(string.Join(";", validationErrors));
            }

            this._clientRepository.Create (client);
            return Ok ();
        }

        [HttpGet]
        public async Task<IEnumerable<ClientDto>> Get () {
            var clients = await _clientRepository.GetAllAsync ();
            return _mapper.Map<IEnumerable<ClientDto>> (clients);
        }

        [HttpPut ("{id}")]
        public IActionResult Edit (Client client) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest (errors);
            }

            _clientRepository.Update (client);
            return Ok ();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (Guid id) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var client = _clientRepository.GetById (id);
            if (client == null) {
                return NotFound ("Wrong id.");
            }

            _clientRepository.Delete (id);
            return Ok ("User deleted.");
        }
    }
}