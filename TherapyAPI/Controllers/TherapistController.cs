using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TherapyAPI.Dto;
using TherapyAPI.Helpers;
using TherapyAPI.Models;

namespace TherapyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TherapistController : ControllerBase
    {
        private ITherapistRepository _therapistRepository;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TherapistController(ITherapistRepository therapistRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _therapistRepository = therapistRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            var user = _therapistRepository.Authenticate(username, password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(TherapistDto developerDto)
        {
            var developer = _mapper.Map<Therapist>(developerDto);

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

            this._therapistRepository.Create(developer, developerDto.Password);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Therapist>> Get()
        {
            return await _therapistRepository.GetAllAsync();
        }

        [HttpPut]
        public IActionResult Edit(Therapist therapist)
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

            _therapistRepository.Update(therapist);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var therapist = _therapistRepository.GetById(Id);
            if(therapist == null)
            {
                return NotFound("Wrong id.");
            }

            _therapistRepository.Delete(Id);
            return Ok("User deleted.");
        }
    }
}