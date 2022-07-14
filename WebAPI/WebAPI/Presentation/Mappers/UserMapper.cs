using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models.Models.Result;

using UserEntity = WebAPI.Core.Entities.User;
using UserModel = WebAPI.Models.Models.Models.User;
using ProjectEntity = WebAPI.Core.Entities.Project;
using TeamEntity = WebAPI.Core.Entities.Team;
using UserRoleCore = WebAPI.Core.Enums.UserRole;
using UserRoleModel = WebAPI.Models.Enums.UserRole;
using UserPositionCore = WebAPI.Core.Enums.UserPosition;
using UserPositionModel = WebAPI.Models.Enums.UserPosition;
using SignInUserModel = WebAPI.Presentation.Models.Action.SignInUser;

namespace WebAPI.Presentation.Mappers
{
    public static class UserMapper
    {
        public static UserEntity Map(UserModel user)
        {
            if (user == null)
            {
                return new UserEntity();
            }
            
            var userEntity = new UserEntity
            {
                Id = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                IsActive = user.IsActive,
                AvatarLink = user.AvatarLink,
                WorkSpaceId = user.WorkSpaceId,
                Email = user.Email,
                CreationDate = user.CreationDate,
                UserRole = Enum.Parse<UserRoleCore>(user.UserRole.ToString(), ignoreCase: true),
                UserPosition = Enum.Parse<UserPositionCore>(user.UserPosition.ToString(), ignoreCase: true),
            };

            return userEntity;
        }

        public static UserModel Map(UserEntity user)
        {
            if (user == null)
            {
                return new UserModel();
            }
            
            var userModel = new UserModel();
            
            MapBase(userModel, user);
            
            return userModel;
        }

        public static UserEntity Map(SignInUserModel user)
        {
            if (user == null)
            {
                return new UserEntity();
            }
            
            var userEntity = new UserEntity
            {
                Email = user.Email,
                Password = user.Password
            };

            return userEntity;
        }

        public static FullUser Map(
            UserEntity user, 
            IEnumerable<ProjectEntity> projects, 
            IEnumerable<TeamEntity> teams)
        {
            var fullUser = new FullUser();
            
            MapBase(fullUser, user);
 
            fullUser.Projects = projects?.Select(Map).ToList() ?? new List<UserProject>();
            fullUser.Teams = teams?.Select(Map).ToList() ?? new List<UserTeam>();
            
            return fullUser;
        }
        
        
        private static void MapBase(UserModel userModel, UserEntity userEntity)
        {
            userModel.UserId = userEntity.Id;
            userModel.UserName = userEntity.UserName;
            userModel.IsActive = userEntity.IsActive;
            userModel.AvatarLink = userEntity.AvatarLink;
            userModel.WorkSpaceId = userEntity.WorkSpaceId;
            userModel.Email = userEntity.Email;
            userModel.CreationDate = userEntity.CreationDate;
            userModel.WorkSpaceId = userEntity.WorkSpaceId;
            userModel.UserRole = Enum.Parse<UserRoleModel>(userEntity.UserRole.ToString(), ignoreCase: true);
            userModel.UserPosition = Enum.Parse<UserPositionModel>(userEntity.UserPosition.ToString(), ignoreCase: true);
        }
        
        private static UserTeam Map(TeamEntity team) => 
            new UserTeam
            {
                TeamId = team.Id,
                TeamName = team.TeamName,
                ProjectId = team.ProjectId
            };
        
        private static UserProject Map(ProjectEntity project) => 
            new UserProject
            {
                ProjectId = project.Id,
                ProjectName = project.ProjectName,
            };
    }
}