using Microsoft.AspNetCore.Mvc;
using RegistrationApi12.Interfaces;
using RegistrationApi12.Models;

namespace RegistrationApi12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _service;

        public RegistrationController(IRegistrationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllUsersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegistrationRequest req)
        {
            var result = await _service.CreateUserAsync(req);
            return result ? Ok("User created") : BadRequest("Error creating user");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegistrationRequest req)
        {
            var result = await _service.UpdateUserAsync(id, req);
            return result ? Ok("User updated") : NotFound("User not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUserAsync(id);
            return result ? Ok("User deleted") : NotFound("User not found");
        }
    }
}
