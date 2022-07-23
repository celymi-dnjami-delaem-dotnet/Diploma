using System;
using System.Threading.Tasks;
using System.Transactions;
using WebAPI.ApplicationLogic.Mappers;
using WebAPI.ApplicationLogic.Providers;
using WebAPI.ApplicationLogic.Utilities;
using WebAPI.Core.Configuration;
using WebAPI.Core.Enums;
using WebAPI.Core.Exceptions;
using WebAPI.Core.Interfaces.Database;
using WebAPI.Core.Interfaces.Services;
using WebAPI.Models.Models.Result;
using WebAPI.Models.Models.Models;
using WebAPI.Presentation.Constants;
using WebAPI.Presentation.Models.Request;
using WebAPI.Presentation.Models.Response;

using UserEntity = WebAPI.Core.Entities.User;
using TeamUserEntity = WebAPI.Core.Entities.TeamUser;

namespace WebAPI.ApplicationLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheContext _cacheContext;
        private readonly AppSettings _appSettings;

        public UserService(IUnitOfWork unitOfWork, ICacheContext cacheContext, AppSettings appSettings)
        {
            _unitOfWork = unitOfWork;
            _cacheContext = cacheContext;
            _appSettings = appSettings;
        }
        
        public async Task<FullUser> GetFullDescriptionByIdAsync(Guid id) => 
            await new UserProvider(_unitOfWork, _cacheContext, _appSettings).GetFullUser(id);

        public async Task<User> GetByIdAsync(Guid id)
        {
            var userEntity = await _unitOfWork.UserRepository.SearchForItemById(id);

            if (userEntity == null)
            {
                throw new UserFriendlyException(
                    ErrorStatus.NOT_FOUND, 
                    ExceptionMessageGenerator.GetMissingEntityMessage(nameof(id)));
            }

            return UserMapper.Map(userEntity);
        }

        public async Task<AuthenticationUserResponseModel> AuthenticateUser(SignInUserRequestModel signInUser)
        {
            var fullUserModel = await new UserProvider(_unitOfWork, _cacheContext, _appSettings)
                .AuthenticateAndGetFullUser(signInUser);
            
            var accessToken = TokenGenerator.GenerateAccessToken(
                _appSettings, 
                fullUserModel.UserId, 
                fullUserModel.UserName, 
                fullUserModel.UserRole.ToString());
            
            string refreshToken = null;

            if (_appSettings.Token.EnableRefreshTokenVerification)
            {
                refreshToken = await GenerateRefreshTokenForAuthedUser(fullUserModel.UserId);
            }

            var tokenPair = new AuthenticationUserResponseModel
            {
                AccessToken = new Token(TokenTypes.Access, accessToken),
                User = fullUserModel
            };

            if (_appSettings.Token.EnableRefreshTokenVerification)
            {
                tokenPair.RefreshToken = new Token(TokenTypes.Refresh, refreshToken);
            }

            return tokenPair;
        }

        public async Task<User> CreateUserWithTeamAsync(User user, Guid teamId)
        {
            var userEntity = UserMapper.Map(user);
            userEntity.TeamUsers.Add(new TeamUserEntity { TeamId = teamId });
            
            return await CreateUser(userEntity);
        }

        public async Task<User> CreateCustomerAsync(SignUpUserRequestModel userRequestModel)
        {
            var customerEntity = UserUtilities.CreateCustomerEntity(userRequestModel);

            return await CreateUser(customerEntity);
        }
        
        public async Task<User> CreateAsync(User user)
        {
            var entityUser = UserMapper.Map(user);

            return await CreateUser(entityUser);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var entityUser = UserMapper.Map(user);

            _unitOfWork.UserRepository.UpdateItem(
                entityUser, 
                prop => prop.Email,
                prop => prop.UserName,
                prop => prop.AvatarLink,
                prop => prop.IsActive,
                prop => prop.UserRole,
                prop => prop.UserPosition);

            await _unitOfWork.CommitAsync();

            return UserMapper.Map(entityUser);
        }

        public async Task<EmailResponseModel> CheckEmailExistenceAsync(string email)
        {
            var emailExists = await _unitOfWork.UserRepository
                .ExistsAsync(user => user.Email.ToLower() == email.ToLower());

            return new EmailResponseModel
            {
                IsEmailExist = emailExists
            };
        }

        public async Task UpdatePasswordAsync(Guid userId, PasswordUpdateRequestModel passwordUpdateRequestModel)
        {
            var oldHashedPassword = PasswordHashing.CreateHashPassword(passwordUpdateRequestModel.OldPassword);
            var newHashedPassword = PasswordHashing.CreateHashPassword(passwordUpdateRequestModel.NewPassword);
            
            var userEntity = await _unitOfWork.UserRepository
                .SearchForSingleItemAsync(user => user.Id == userId && 
                                                  user.Password == oldHashedPassword);
 
            if (userEntity == null)
            {
                throw new UserFriendlyException(
                    ErrorStatus.NOT_FOUND, 
                    "Unable to find user with provided id and password");
            }

            userEntity.Password = newHashedPassword;

            await _unitOfWork.UserRepository.UpdateUserPasswordAsync(userEntity);
        }

        public async Task UpdateAvatarAsync(User user)
        {
            var userEntity = UserMapper.Map(user);

            await _unitOfWork.UserRepository.UpdateUserAvatarLinkAsync(userEntity);
        }

        public async Task ChangeActivityStatusAsync(User user)
        {
            var userEntity = UserMapper.Map(user);

            await _unitOfWork.UserRepository.ChangeUserActivityStatusAsync(userEntity);
        }

        public async Task RemoveAsync(Guid id)
        {
            using var scope = new TransactionScope
            (
                TransactionScopeOption.Required, 
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable,
                },
                TransactionScopeAsyncFlowOption.Enabled
            );
            
            _unitOfWork.RefreshTokenRepository.Remove(refreshToken => refreshToken.UserId == id);
            _unitOfWork.UserRepository.Remove(user => user.Id == id);

            await _unitOfWork.CommitAsync();
            
            scope.Complete();
        }


        private async Task<User> CreateUser(UserEntity user)
        {
            user.Password = PasswordHashing.CreateHashPassword(user.Password);
            user.CreationDate = DateTime.UtcNow;
            
            await _unitOfWork.UserRepository.CreateAsync(user);
            
            await _unitOfWork.CommitAsync();

            return UserMapper.Map(user);
        }

        private async Task<string> GenerateRefreshTokenForAuthedUser(Guid userId)
        {
            var existingToken = await _unitOfWork.RefreshTokenRepository
                .SearchForSingleItemAsync(token => token.UserId == userId);
 
            if (existingToken != null)
            {
                return existingToken.Value;
            }
            
            var refreshTokenEntity = TokenGenerator.GenerateRefreshTokenEntity(
                userId,
                _appSettings.Token.LifeTime);

            await _unitOfWork.RefreshTokenRepository.CreateAsync(refreshTokenEntity);

            await _unitOfWork.CommitAsync();

            return refreshTokenEntity.Value;
        }
    }
}