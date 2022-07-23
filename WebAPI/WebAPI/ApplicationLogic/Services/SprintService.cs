using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ApplicationLogic.Mappers;
using WebAPI.ApplicationLogic.Utilities;
using WebAPI.Core.Enums;
using WebAPI.Core.Exceptions;
using WebAPI.Core.Interfaces.Database;
using WebAPI.Core.Interfaces.Services;
using WebAPI.Models.Models.Models;
using WebAPI.Models.Models.Result;

namespace WebAPI.ApplicationLogic.Services
{
    public class SprintService : ISprintService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SprintService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CollectionResponse<FullSprint>> GetAllSprintsFromEpicAsync(Guid epicId, Guid? teamId)
        {
            var sprintEntities = await _unitOfWork.SprintRepository.GetFullSprintsByEpicId(epicId, teamId);
           
            var sprintsCollectionResponse = new CollectionResponse<FullSprint>
            {
                Items = sprintEntities.Select(SprintMapper.MapToFullModel).ToList()
            };

            return sprintsCollectionResponse;
        }

        public async Task<Sprint> GetSprintByIdAsync(Guid sprintId)
        {
            var sprintEntity = await _unitOfWork.SprintRepository
                .SearchForSingleItemAsync(sprint => sprint.Id == sprintId);

            if (sprintEntity == null)
            {
                throw new UserFriendlyException(
                    ErrorStatus.NOT_FOUND, 
                    ExceptionMessageGenerator.GetMissingEntityMessage(nameof(sprintId)));
            }

            var sprintModel = SprintMapper.Map(sprintEntity);

            return sprintModel;
        }

        public async Task<FullSprint> GetFullSprintAsync(Guid sprintId)
        {
            var sprintEntity = await _unitOfWork.SprintRepository
                .SearchForSingleItemAsync(
                    sprint => sprint.Id == sprintId,
                    include => include.Stories);

            if (sprintEntity == null)
            {
                throw new UserFriendlyException(
                    ErrorStatus.NOT_FOUND,
                    ExceptionMessageGenerator.GetMissingEntityMessage(nameof(sprintId)));
            }

            var sprintFullModel = SprintMapper.MapToFullModel(sprintEntity);

            return sprintFullModel;
        }

        public async Task<Sprint> CreateSprintAsync(Sprint sprint)
        {
            var sprintEntity = SprintMapper.Map(sprint);
            sprintEntity.CreationDate = DateTime.UtcNow;

            await _unitOfWork.SprintRepository.CreateAsync(sprintEntity);

            await _unitOfWork.CommitAsync();
            
            var sprintModel = SprintMapper.Map(sprintEntity);

            return sprintModel;
        }

        public async Task<Sprint> UpdateSprintAsync(Sprint sprint)
        {
            var sprintEntity = SprintMapper.Map(sprint);

            _unitOfWork.SprintRepository.UpdateItem(sprintEntity);
            await _unitOfWork.CommitAsync();

            var sprintModel = SprintMapper.Map(sprintEntity);

            return sprintModel;
        }

        public async Task RemoveSprintSoftAsync(Sprint sprint)
        {
            _unitOfWork.SprintRepository.DeleteSoft(sprint.SprintId);

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveSprintAsync(Guid sprintId)
        {
            _unitOfWork.SprintRepository.Remove(x => x.Id == sprintId);
            
            await _unitOfWork.CommitAsync();
        }
    }
}