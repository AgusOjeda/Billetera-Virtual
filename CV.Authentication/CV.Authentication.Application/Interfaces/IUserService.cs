using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Request;
using CV.Authentication.Domain.DTOs.Response;
using CV.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServerResponse<UserDto>> CreateAsync(string email, string password);
        Task<UserDto?> FindByIdAsync(Guid id);
        Task<bool> FindByEmail(string email);
        Task<UserDto> FindByEmailAsync(string email);
        Task<ServerResponse<UserDto>> VerifyEmailToken(VerifyEmailRequest request, Guid userId);
        Task<ServerResponse<UserDto>> ForgotPasswordTokenCreation(string email);
        Task<ServerResponse> IsValidResetToken(ValidateResetPasswordTokenRequest request, Guid userId);
        Task<ServerResponse> ResetPassword(ResetPasswordRequest request, Guid userId);
        Task<ServerResponse> UpdateUserAsync(User userToUpdate);
        string GetToken(string email, Guid id);
    }
}
