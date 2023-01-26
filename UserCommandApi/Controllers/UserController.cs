using Application.Commands.User;
using Application.Exceptions;
using Domain.DTOs.User;
using Domain.EntitiesModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace UserCommandApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<ActionResult<UserDTO>> Create(UserCreateUpdateCommand user) => Ok(await _mediator.Send(user));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<ActionResult> Delete(int id) => Ok(await _mediator.Send(new UserDeleteCommand { Id=id}));
    }
}
