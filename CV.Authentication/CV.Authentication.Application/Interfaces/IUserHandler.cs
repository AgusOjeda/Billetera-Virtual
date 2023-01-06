using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Request;
using CV.Authentication.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Application.Interfaces
{
    public interface IUserHandler
    {
        Task<ServerResponse<UserDto>> Register(UserRegisterRequest model);
        Task<ServerResponse<UserLoginResponse>> Login(UserLoginRequest model);
        Task<ServerResponse<VerifyResponse>> VerifyEmail(VerifyEmailRequest Request, Guid UserId);
        Task<ServerResponse> ForgotPassword(string email);
        Task<ServerResponse> ResetPassword(ResetPasswordRequest request, Guid UserId);
        Task<ServerResponse> ValidateTokenExpirationDate(ValidateResetPasswordTokenRequest request, Guid UserId);
        string GetToken(string email, Guid id);
        Task<bool> Logout(string? from, string? to);
        Task<ServerResponse> UpdateState(Guid id, int stateId);
        Task<bool> ChangeEmail();
    }
}
