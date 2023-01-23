using Domain.DTOs.ProjectCharter;
using Domain.Interfaces.Services.ProjectCharterBLL;
using Microsoft.AspNetCore.Mvc;


namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCharterController : ControllerBase
    {
        private readonly IProjectCharterBLL _projectCharterBLL;
        public ProjectCharterController(IProjectCharterBLL projectCharterBLL) => _projectCharterBLL = projectCharterBLL;

        /// <summary>
        /// Get All ProjectCharter
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectCharterDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ProjectCharterDTO>>> GetAll() => Ok(await _projectCharterBLL.GetAll());

        /// <summary>
        /// Get ProjectCharter By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectCharterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProjectCharterDTO>> GetById(int id) => Ok(await _projectCharterBLL.GetById(id));


        /// <summary>
        /// Delete ProjectCharter By idLOB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectCharterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProjectCharterDTO>> Delete(int id) => Ok(await _projectCharterBLL.Delete(id));


        /// <summary>
        /// Create ProjectCharter
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectCharterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProjectCharterDTO>> Create(ProjectCharterCreate project) => Ok(await _projectCharterBLL.Create(project));


        /// <summary>
        /// Update ProjectCharter
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectCharterDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProjectCharterDTO>> Update(ProjectCharterUpdate project) => Ok(await _projectCharterBLL.Update(project));
    }
}
