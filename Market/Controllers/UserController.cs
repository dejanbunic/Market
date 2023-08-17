using Market.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Market.Dtos;
using UserQueryRequest = Market.Dtos.UserQueryRequest;
using Market.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Market.Controllers
{
    [ApiController]
/*    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]*/
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userPostRequest)
        {
            User user = _mapper.Map<User>(userPostRequest);
            var createdUserDb = await _userService.CreateUserAsync(user);
            var createdUserDto = _mapper.Map<UserDto>(createdUserDb);
            return Ok(createdUserDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryRequest userQueryRequest)
        {
            var queryParams = _mapper.Map<Application.Models.UserQueryRequest>(userQueryRequest);
            var usersResult = await _userService.GetAllAsync(queryParams);
            var users = _mapper.Map<List<UserDto>>(usersResult);
            return Ok(users);
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var usersResult = await _userService.GetByIdAsync(userId);
            var user = _mapper.Map<UserDto>(usersResult);
            return Ok(user);
        }

        [HttpDelete("userId")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            bool userDeleted = await _userService.DeleteUserAsync(userId);
            return Ok(userDeleted);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDto userUpdateRequest)
        {
            User userToUpdate = _mapper.Map<User>(userUpdateRequest);
            User userUpdated = await _userService.UpdateUserAsync(userToUpdate);
            var userResult = _mapper.Map<UserDto>(userUpdated);
            return Ok(userResult);
        }

    }
}
