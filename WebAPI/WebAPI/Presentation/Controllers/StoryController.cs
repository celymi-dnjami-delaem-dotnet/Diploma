using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAPI.Core.Enums;
using WebAPI.Core.Interfaces.Services;
using WebAPI.Models.Models.Models;
using WebAPI.Models.Models.Result;
using WebAPI.Presentation.Utilities;

namespace WebAPI.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }
        
        /// <summary>
        /// Sorts stories in particular order from epic, assigned to team.
        /// </summary>
        /// <param name="teamId">Team identifier.</param>
        /// <param name="epicId">Epic identifier.</param>
        /// <param name="sprintId">Sprint identifier.</param>
        /// <param name="sortType">Sort field.</param>
        /// <param name="orderType">Sort direction.</param>
        /// <response code="200">A collection of sorted stories in particular order.</response>
        /// <response code="401">Failed authentication.</response>
        /// <response code="404">Unable to find stories with provided epic and team id.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CollectionResponse<Story>>> SortStories(
            [FromQuery, BindRequired] Guid teamId,
            [FromQuery, BindRequired] Guid epicId,
            [FromQuery] Guid? sprintId,
            [FromQuery, BindRequired] string sortType,
            [FromQuery, BindRequired] OrderType orderType) => 
                await _storyService.SortStories(epicId, teamId, sprintId, sortType, orderType);
        
        // todo: combine epics, sprints and teams search
        /// <summary>
        /// Gets stories from epic.
        /// </summary>
        /// <param name="epicId">Epic identifier.</param>
        /// <param name="teamId">Team identifier.</param>
        /// <response code="200">A collection of stories from epic.</response>
        /// <response code="401">Failed authentication.</response>
        /// <response code="404">Unable to find any stories with provided epic id.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("epic/id/{epicId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CollectionResponse<Story>>> GetStoriesFormEpic(
            Guid epicId,
            [FromQuery] Guid teamId) => 
            await _storyService.GetStoriesFromEpicAssignedToTeamAsync(epicId, teamId);
        
        /// <summary>
        /// Gets stories from sprint.
        /// </summary>
        /// <param name="id">Story identifier.</param>
        /// <response code="200">A collection of stories from sprint.</response>
        /// <response code="401">Failed authentication.</response>
        /// <response code="404">Unable to find any stories with provided sprint id.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("sprint/id/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResponse<Story>>> GetStoriesFormSprint(Guid id) => 
            await _storyService.GetStoriesFromSprintAsync(id);

        /// <summary>
        /// Gets story by provided id.
        /// </summary>
        /// <param name="id">Story identifier.</param>
        /// <response code="200">Story with provided id.</response>
        /// <response code="401">Failed authentication.</response>
        /// <response code="404">Unable to find story with provided story id.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("id/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Story>> GetStory(Guid id) =>
            await _storyService.GetByIdAsync(id);

        /// <summary>
        /// Gets story full description with provided id
        /// </summary>
        /// <param name="id">Story identifier.</param>
        /// <response code="200">Received story full description with provided id</response>
        /// <response code="401">Failed authentication</response>
        /// <response code="404">Unable to find story with provided story id</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("full/id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FullStory>> GetFullStoryDescription(Guid id) => 
            await _storyService.GetFullDescriptionAsync(id);

        /// <summary>
        /// Creates story.
        /// </summary>
        /// <param name="story"><see cref="Story"/> model.</param>
        /// <response code="201">Created story.</response>
        /// <response code="401">Failed authentication.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Story>> CreateStory([FromBody, BindRequired] Story story)
        {
            var user = ClaimsReader.GetUserClaims(User);
            
            var createdStory = await _storyService.CreateAsync(story, user.UserName);

            return CreatedAtAction(nameof(CreateStory), createdStory);
        }

        /// <summary>
        /// Updates story and writes updates to story history.
        /// </summary>
        /// <param name="story"><see cref="Story"/> model.</param>
        /// <response code="200">Updated story.</response>
        /// <response code="401">Failed authentication.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Story>> UpdateStory([FromBody, BindRequired] Story story)
        {
            var user = ClaimsReader.GetUserClaims(User);
            
            var updatedStory = await _storyService.UpdateAsync(story, user.UserId);

            return updatedStory;
        }

        /// <summary>
        /// Updates story board column with provided story id.
        /// </summary>
        /// <param name="storyPatch"><see cref="JsonPatchDocument"/> with <see cref="Story"/> model.</param>
        /// <response code="200">Updated story with new board column.</response>
        /// <response code="400">Invalid data in request model.</response>
        /// <response code="401">Failed authentication.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPatch]
        [Route("board-move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Story>> UpdateStoryColumn(
            [FromBody, BindRequired] JsonPatchDocument<Story> storyPatch)
        {
            var storyModel = new Story();
            storyPatch.ApplyTo(storyModel, ModelState);

            var user = ClaimsReader.GetUserClaims(User);
            
            var updatedStory = await _storyService.UpdateColumnAsync(storyModel, user.UserName);
            
            return updatedStory;
        }
        
        /// <summary>
        /// Updates story status (active or blocked).
        /// </summary>
        /// <param name="storyPatch"><see cref="JsonPatchDocument"/> with <see cref="Story"/> model.</param>
        /// <response code="200">Updated story with new status.</response>
        /// <response code="400">Invalid data in request model.</response>
        /// <response code="401">Failed authentication.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPatch]
        [Route("change-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Story>> ChangeStoryStatus(
            [FromBody, BindRequired] JsonPatchDocument<Story> storyPatch)
        {
            var storyModel = new Story();
            storyPatch.ApplyTo(storyModel, ModelState);

            var user = ClaimsReader.GetUserClaims(User);
            
            var story = await _storyService.ChangeStatusAsync(storyModel, user.UserName);
            
            return story;
        }

        /// <summary>
        /// Updates story deleted status by provided id.
        /// </summary>
        /// <param name="id">Story identifier.</param>
        /// <response code="204">Story deleted status was set.</response>
        /// <response code="401">Failed authentication.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete]
        [Route("soft-remove/id/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> StorySoftRemove(Guid id)
        {
            await _storyService.SoftRemoveAsync(id);
            
            return NoContent();
        }
        
        /// <summary>
        /// Removes story from DB by provided id.
        /// </summary>
        /// <param name="id">Story identifier.</param>
        /// <response code="204">Story was removed from DB.</response>
        /// <response code="401">Failed authentication.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete]
        [Route("remove/id/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RemoveStory(Guid id)
        {
            await _storyService.RemoveAsync(id);
            
            return NoContent();
        }
    }
}