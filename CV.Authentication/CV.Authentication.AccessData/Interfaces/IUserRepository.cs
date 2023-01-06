using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Response;
using CV.Authentication.Domain.Entities;

namespace CV.Authentication.AccessData.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task Update(User user);
        Task Delete(User user);
        Task<bool> FindByEmailAsync(string email);
        Task<UserDto?> GetUserToVerify(Guid id, string token);
        Task<UserDto> GetByEmailAsync(string email);
        Task<UserResponse> GetState(string email);
        List<User> GetAllUsers();
        Task<UserDto?> GetUserById(Guid id);
    }
}
