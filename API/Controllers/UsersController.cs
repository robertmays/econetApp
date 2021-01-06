using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            //IEnumerable is just simpler form of List. List give more methods but we don't need them for this
            // async makes our api lots more scalable  
            var users = await _userRepository.GetEmployeesAsync();
            return Ok(users);
        }

        //[HttpGet("{username}")] and [HttpGet("{id}")] are the same for routing
        //so you would need to use [HttpGet("id:/{id}")]
       
        [HttpGet("{username}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByUsername(string username)
        {
            return await _userRepository.GetEmployeeByUsernameAsync(username);                      
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLoggedOnUserProfile(EmployeeUpdateDto employeeUpdateDto)
        {
            // this only works for the logged on **user**********
            //we do not need to return an object back to user 
            //as they already ahd it and they are sending the update to us
            //never trust what a user sends you. validate it from the token first
            //
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(employeeUpdateDto, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent(); //no need to send anything back if successful

            return BadRequest("Failed to update logged on user");
        }
    }
}