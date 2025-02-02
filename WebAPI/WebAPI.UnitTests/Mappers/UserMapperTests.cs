using System;
using WebAPI.ApplicationLogic.Mappers;
using Xunit;

using UserEntity = WebAPI.Core.Entities.User;
using UserModel = WebAPI.Models.Models.Models.User;

namespace WebAPI.UnitTests.Mappers
{
    public class UserMapperTests
    {
        [Fact]
        public void ShouldReturnEmptyModelOnNullEntity()
        {
            //Arrange & Act
            var mappedResult = UserMapper.Map((UserEntity)null);
            
            //Assert
            Assert.NotNull(mappedResult);
        }
        
        [Fact]
        public void ShouldReturnEmptyEntityOnNullModel()
        {
            //Arrange & Act
            var mappedResult = UserMapper.Map((UserModel)null);
            
            //Assert
            Assert.NotNull(mappedResult);
        }
        
        [Fact]
        public void ShouldMapUserEntityToModel()
        {
            //Arrange
            var userId = new Guid("b593238f-87e6-4e86-93fc-ab79b8804dec");
            var workSpaceId = new Guid("1113238f-87e6-4e86-93fc-ab79b8804111");
            const string userName = "SomeUser"; 
            const string avatarLink = "AvatarLink"; 
            const string email = "test@mail.com"; 
            var creationDate = DateTime.UtcNow;
            const bool isActive = true;
            
            var userEntity = new UserEntity
            {
                Id = userId,
                UserName = userName,
                UserPosition = Core.Enums.UserPosition.Developer,
                UserRole = Core.Enums.UserRole.Engineer,
                WorkSpaceId = workSpaceId,
                AvatarLink = avatarLink,
                Email = email,
                CreationDate = creationDate,
                IsActive = isActive
            };

            var userModel = new UserModel
            {
                UserId = userId,
                UserName = userName,
                UserPosition = Models.Enums.UserPosition.Developer,
                UserRole = Models.Enums.UserRole.Engineer,
                WorkSpaceId = workSpaceId,
                AvatarLink = avatarLink,
                Email = email,
                CreationDate = creationDate,
                IsActive = isActive
            };
            
            //Act
            var mappedResult = UserMapper.Map(userEntity);

            //Assert
            Assert.Equal(userModel.UserId, mappedResult.UserId);
            Assert.Equal(userModel.UserName, mappedResult.UserName);
            Assert.Equal(userModel.UserPosition.ToString(), mappedResult.UserPosition.ToString());
            Assert.Equal(userModel.UserRole.ToString(), mappedResult.UserRole.ToString());
            Assert.Equal(userModel.AvatarLink, mappedResult.AvatarLink);
            Assert.Equal(userModel.Email, mappedResult.Email);
            Assert.Equal(userModel.IsActive, mappedResult.IsActive);
            Assert.Equal(userModel.CreationDate, mappedResult.CreationDate);
        }
        
        [Fact]
        public void ShouldMapUserModelToEntity()
        {
            //Arrange
            var userId = new Guid("b593238f-87e6-4e86-93fc-ab79b8804dec");
            var workSpaceId = new Guid("1113238f-87e6-4e86-93fc-ab79b8804111");
            const string userName = "SomeUser"; 
            const string avatarLink = "AvatarLink"; 
            const string email = "test@mail.com"; 
            var creationDate = DateTime.UtcNow;
            const bool isActive = true;
            
            var userEntity = new Core.Entities.User
            {
                Id = userId,
                UserName = userName,
                UserPosition = Core.Enums.UserPosition.Developer,
                UserRole = Core.Enums.UserRole.Engineer,
                WorkSpaceId = workSpaceId,
                AvatarLink = avatarLink,
                Email = email,
                CreationDate = creationDate,
                IsActive = isActive
            };

            var userModel = new UserModel
            {
                UserId = userId,
                UserName = userName,
                UserPosition = Models.Enums.UserPosition.Developer,
                UserRole = Models.Enums.UserRole.Engineer,
                WorkSpaceId = workSpaceId,
                AvatarLink = avatarLink,
                Email = email,
                CreationDate = creationDate,
                IsActive = isActive
            };
            
            //Act
            var mappedResult = UserMapper.Map(userModel);

            //Assert
            Assert.Equal(userEntity.Id, mappedResult.Id);
            Assert.Equal(userEntity.UserName, mappedResult.UserName);
            Assert.Equal(userEntity.UserPosition.ToString(), mappedResult.UserPosition.ToString());
            Assert.Equal(userEntity.UserRole.ToString(), mappedResult.UserRole.ToString());
            Assert.Equal(userEntity.AvatarLink, mappedResult.AvatarLink);
            Assert.Equal(userEntity.Email, mappedResult.Email);
            Assert.Equal(userEntity.IsActive, mappedResult.IsActive);
            Assert.Equal(userModel.CreationDate, mappedResult.CreationDate);
        }
    }
}