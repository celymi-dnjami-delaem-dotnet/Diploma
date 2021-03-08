using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAPI.Core.Interfaces.Services;
using WebAPI.Models.Models;
using WebAPI.Models.Result;
using WebAPI.Presentation.Filters;

namespace WebAPI.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/sprint")]
    [ServiceFilter(typeof(UserAuthorizationFilter))]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _sprintService;

        public SprintController(ISprintService sprintService)
        {
            _sprintService = sprintService;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CollectionResponse<Sprint>>> GetAllSprints() => await _sprintService.GetALlSprints();

        [HttpGet]
        [Route("epic/{epicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CollectionResponse<FullSprint>>> GetAllSprintsFromEpic(Guid epicId)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            if (userId == null)
            {
                return BadRequest();
            }
            
            var boardResponse = await _sprintService.GetAllSprintsFromEpic(epicId, new Guid(userId));

            return boardResponse;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Sprint>> GetSprint(Guid id)
        {
            var sprint = await _sprintService.GetSprint(id);

            if (sprint == null)
            {
                return NotFound();
            }
            
            return sprint;
        }
        
        [HttpGet]
        [Route("full/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FullSprint>> GetFullSprint(Guid id)
        {
            var fullSprint =  await _sprintService.GetFullSprint(id);

            if (fullSprint == null)
            {
                return NotFound();
            }
            
            return fullSprint;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Sprint>> CreateSprint([FromBody, BindRequired] Sprint sprint)
        {
            var createdSprint = await _sprintService.CreateSprint(sprint);

            return CreatedAtAction(nameof(CreateSprint), createdSprint);
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateSprint([FromBody, BindRequired] Sprint sprint)
        {
            var updatedSprint = await _sprintService.UpdateSprint(sprint);

            return Ok(updatedSprint);
        }
        
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveSprint(Guid id)
        {
            await _sprintService.RemoveSprint(id);

            return NoContent();
        }
    }
}