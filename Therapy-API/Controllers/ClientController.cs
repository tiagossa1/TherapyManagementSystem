using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BuddhaTerapias_API.Interfaces;
using BuddhaTerapiasAPI.Dtos;
using BuddhaTerapiasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuddhaTerapias_API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase {
        private readonly IClientRepository repository;

        public ClientController (IClientRepository Repository) {
            repository = Repository;
        }

        // GET api/client
        [HttpGet]
        public async Task<IActionResult> Get () {
            var users = await repository.GetAll ();
            return Ok (Mapper.Map<IEnumerable<ClientDto>> (users));
            //return Ok(users);
        }

        // GET api/client/5
        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (Guid id) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var user = await repository.GetById (id);
            if (user == null) {
                return NotFound ("User not found");
            }

            return Ok (Mapper.Map<ClientDto> (user));
            //return Ok(user);
        }

        // POST api/client
        [HttpPost]
        public async Task<IActionResult> Create (Client client) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            await repository.Create (client);
            return StatusCode (201);
        }

        // PUT api/client/{id}
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update (Client client) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var user = repository.GetById (client.Id);
            if (user == null) {
                return NotFound ("User not found.");
            }

            await repository.Update (client);
            return Ok ("Client info changed.");
        }

        // DELETE api/client/{id}
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (Guid id) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var user = repository.GetById (id);
            if (user == null) {
                return NotFound ("User not found.");
            }

            await repository.Delete (id);
            return Ok ("User deleted.");
        }
    }
}