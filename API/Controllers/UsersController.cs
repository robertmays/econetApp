using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // always use postman to test your api client
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository, 
                                IMapper mapper, 
                                IPhotoService photoService)
        {
            _photoService = photoService;
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
        //remember rob routing to work needs to be unique so if for eg. you have more than one
        //HttpPut request make sure you don't create duplicate paths

        [HttpGet("{username}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployeeByUsername(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLoggedOnUserProfile(EmployeeUpdateDto employeeUpdateDto)
        {
            // this only works for the logged on **user**********
            //we do not need to return an object back to user 
            //as they already have it and they are sending the update to us.

            //never trust what a user sends you. validate it from the token first
            //use the claims principal User that we have access to. 
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            _mapper.Map(employeeUpdateDto, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent(); //no need to send anything back if successful

            return BadRequest("Failed to update logged on user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<UserPhotoDto>> AddPhoto(IFormFile file)
        {
            // we eagerly load our photos here too
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new UserPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
            {
                //CreatedAtRoute returns a 201 and adds a location to the headers returned
                return CreatedAtRoute("GetEmployee", new { username = user.UserName },  _mapper.Map<UserPhotoDto>(photo));               
            }
            

            return BadRequest("Problem adding photo");

        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            // not returning an object as we just updating a resource
            // get the logged on authenticated user
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain) return BadRequest("This is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo");

            // not all photos will be in cloudinery
            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            // now remove from database
            user.Photos.Remove(photo);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete photo");

        }

    }
}