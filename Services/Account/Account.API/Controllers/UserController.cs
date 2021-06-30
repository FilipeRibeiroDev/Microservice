using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Account.API.Application.Commands;
using Account.API.Application.Queries;
using Account.API.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly IUserQueries _userQueries;

        public UserController(IMediator mediator,
            IUserQueries userQueries,
         ILogger<UserController> logger)
        {
            _mediator = mediator;
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <returns>List users</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var user = await _userQueries.GetUsers();

            return Ok(user);
        }

        /// <summary>
        /// Get one user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(string id)
        {
            var user = await _userQueries.GetUser(id);

            return Ok(user);
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="createUserCommand"></param>
        /// <returns>User</returns>
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Post([FromBody] CreateUserCommand createUserCommand)
        {
            _logger.LogInformation("----- Creating User - User: {@User}", createUserCommand);

            var result = await _mediator.Send(createUserCommand);
            return Ok(result);
        }

        /// <summary>
        /// Put user
        /// </summary>
        /// <param name="updateUserCommand"></param>
        /// <param name="identityUser"></param>
        /// <returns>User</returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<ActionResult<UserUpdateViewModel>> Put([FromBody] UpdateUserCommand updateUserCommand, [FromHeader(Name = "x-identityUser")] string identityUser)
        {
            var commandResult = new UserUpdateViewModel();

            if (Guid.TryParse(identityUser, out Guid guid) && guid != Guid.Empty)
            {

                UpdateUserCommand updateCommand = new UpdateUserCommand(guid.ToString())
                {
                    Email = updateUserCommand.Email,
                    Name = updateUserCommand.Name,
                    Password = updateUserCommand.Password
                };

                _logger.LogInformation("----- Updating User - User: {@User}", updateUserCommand);

                commandResult = await _mediator.Send(updateCommand);
            }

            if (commandResult == null)
            {
                return BadRequest();
            }
            return Ok(commandResult);
        }

        /// <summary>
        /// Remove User
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize]
        public async Task<IActionResult> Delete([FromHeader(Name = "x-identityUser")] string identityUser)
        {
            bool commandResult = false;

            if (Guid.TryParse(identityUser, out Guid guid) && guid != Guid.Empty)
            {
                RemoveUserCommand removeUserCommand = new RemoveUserCommand(guid.ToString());
                _logger.LogInformation("----- Removing User - User: {@User}", removeUserCommand);

                commandResult = await _mediator.Send(removeUserCommand);
            }
            
            if (!commandResult)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
