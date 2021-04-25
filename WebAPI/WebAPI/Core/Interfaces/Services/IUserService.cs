using System;
using System.Threading.Tasks;
using WebAPI.Models.Models;
using WebAPI.Models.Models.Authentication;
using WebAPI.Models.Models.Result;

namespace WebAPI.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<CollectionResponse<User>> GetAllUsers();

        Task<FullUser> GetFullUser(Guid id);
        
        Task<User> GetUser(Guid id);

        Task<User> CreateUser(User user);

        Task<User> CreateCustomer(SignUpUser user);

        Task<User> UpdateUser(User user);

        Task UpdateUserPasswordAsync(Guid userId, PasswordUpdate passwordUpdate);
        
        Task UpdateUserAvatarAsync(User user);
        
        Task ChangeUserActivityStatusAsync(User user);

        Task RemoveUser(Guid id);
    }
}