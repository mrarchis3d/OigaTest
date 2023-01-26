using Application.Exceptions;
using Application.Queries.User;
using UserEntity = Domain.EntitiesModels.User;
using Domain.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace UserQueryApi.Controllers
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

        [HttpPost("GetFiltered")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync(UserGetQuery pageCriteria) => Ok(await _mediator.Send(pageCriteria));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
        public async Task<ActionResult<UserEntity>> GetUserByIdAsync(int id) => Ok(await _mediator.Send(new UserGetByIdQuery { Id = id}));

    }
}
