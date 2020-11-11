using System.Linq;
using WebAPI.Core.Entities;
using WebAPI.Core.Interfaces.Mappers;
using WebAPI.Models.Result;

namespace WebAPI.Presentation.Mappers
{
    public class EpicMapper : IEpicMapper
    {
        private readonly ISprintMapper _sprintMapper;
        
        public EpicMapper(ISprintMapper sprintMapper)
        {
            _sprintMapper = sprintMapper;
        }
        
        public Epic MapToEntity(Models.Models.Epic epic)
        {
            var entityEpic = new Epic
            {
                EpicId = epic.EpicId,
                EpicName = epic.EpicName,
                EpicDescription = epic.EpicDescription,
                StartDate = epic.StartDate,
                EndDate = epic.EndDate,
                Progress = epic.Progress,
            };

            return entityEpic;
        }

        public Models.Models.Epic MapToModel(Epic epic)
        {
            var epicModel = new Models.Models.Epic
            {
                EpicId = epic.EpicId,
                EpicName = epic.EpicName,
                EpicDescription = epic.EpicDescription,
                StartDate = epic.StartDate,
                EndDate = epic.EndDate,
                Progress = epic.Progress,
            };

            return epicModel;
        }

        public FullEpic MapToFullModel(Epic epic)
        {
            var epicModel = new FullEpic
            {
                EpicId = epic.EpicId,
                EpicName = epic.EpicName,
                EpicDescription = epic.EpicDescription,
                StartDate = epic.StartDate,
                EndDate = epic.EndDate,
                Progress = epic.Progress,
                Sprints = epic.Sprints.Select(_sprintMapper.MapToModel).ToList(),
            };

            return epicModel;
        }
    }
}