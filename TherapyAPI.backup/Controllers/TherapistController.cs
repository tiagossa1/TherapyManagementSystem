using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TherapyAPI.Dto;
using TherapyAPI.Helpers;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class TherapistController : ControllerBase {
        private readonly ITherapistRepository _therapistRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TherapistController (ITherapistRepository therapistRepository, IMapper mapper, IOptions<AppSettings> appSettings) {
            _therapistRepository = therapistRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        //[AllowAnonymous]
        //[HttpPost ("authenticate")]
        //public IActionResult Authenticate (Authentication authenticationObj) {
        //    var user = _therapistRepository.Authenticate (authenticationObj.Username, authenticationObj.Password);
        //    if (user == null)
        //        return BadRequest ("Username or password is incorrect.");

        //    var tokenHandler = new JwtSecurityTokenHandler ();
        //    var key = Encoding.ASCII.GetBytes (_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor {
        //        Subject = new ClaimsIdentity (new Claim[] {
        //        new Claim (ClaimTypes.Name, user.Name.ToString ()),
        //        new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ())
        //        }),

        //        Expires = DateTime.UtcNow.AddHours (1),
        //        SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken (tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken (token);

        //    // return basic user info (without password) and token to store client side
        //    return Ok (new {
        //        Id = user.Id,
        //            Username = user.Username,
        //            Token = tokenString
        //    });
        //}

        [AllowAnonymous]
        [HttpPost ("register")]
        public IActionResult Register (TherapistDto therapistDto) {
            var therapist = _mapper.Map<Therapist> (therapistDto);

            if (!ModelState.IsValid) {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest (errors);
            }

            this._therapistRepository.Create (therapist, therapistDto.Password);
            return Ok ();
        }

        [HttpGet]
        public async Task<IEnumerable<TherapistDto>> Get () {
            var therapists = await _therapistRepository.GetAllAsync ();
            return _mapper.Map<IEnumerable<TherapistDto>> (therapists);
        }

        [HttpPut("{id}")]
        public IActionResult Edit (Therapist therapist) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest (errors);
            }

            _therapistRepository.Update (therapist);
            return Ok ();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (Guid Id) {
            if (!ModelState.IsValid) {
                return BadRequest ();
            }

            var therapist = _therapistRepository.GetById (Id);
            if (therapist == null) {
                return NotFound ("Wrong id.");
            }

            _therapistRepository.Delete (Id);
            return Ok ("Therapist deleted.");
        }
    }
}