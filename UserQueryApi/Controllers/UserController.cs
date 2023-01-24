using ApplicationMediatr.Commands.User;
using ApplicationMediatr.Exceptions;
using ApplicationMediatr.Queries.User;
using Domain.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace APIMediatr.Controllers
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

        [HttpPost("/GetFiltered")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync(UserGetQuery pageCriteria) => Ok(await _mediator.Send(pageCriteria));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync([FromQuery]int id) => Ok(await _mediator.Send(new UserGetByIdQuery { Id = id}));

    }
}
