using CV.Authentication.Application.Interfaces;
using CV.Authentication.Application.Tools;
using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Request;
using CV.Authentication.Domain.DTOs.Response;
using CV.Authentication.Domain.Entities;
using CV.Authentication.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Application.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserHandler(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        
        public async Task<ServerResponse<UserDto>> Register(UserRegisterRequest request)
        {
            var response = new ServerResponse<UserDto>();
            // check if user exists
            var userExist = await _userService.FindByEmail(request.Email);
            if (userExist)
            {
                response.Errors.Add("User already exists");
                return response;
            }
            // create user
            var newUser = await _userService.CreateAsync(request.Email, request.Password);


            return newUser;
        }
        public async Task<ServerResponse<UserLoginResponse>> Login(UserLoginRequest request)
        {
            var response = new ServerResponse<UserLoginResponse>();
            // check if user exists
            var user = await _userService.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            // check if verified
            if (user.VerifiedAt == null)
            {
                response.Errors.Add("User not verified");
                return response;
            }

            if (!Encrypt.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Errors.Add("Invalid password");
                return response;
            }
            response.Data = new UserLoginResponse
            {
                Email = user.Email,
                Token = _userService.GetToken(user.Email, user.Id)
            };

            return response;

        }
        public async Task<ServerResponse<VerifyResponse>> VerifyEmail(VerifyEmailRequest request, Guid userId)
        {
            var response = new ServerResponse<VerifyResponse>();
            var user = await _userService.VerifyEmailToken(request, userId);
            if (!user.IsValid)
            {
                response.Errors.AddRange(user.Errors);
                return response;
            }
            response.Data = new VerifyResponse
            {
                Success = true,
                Message = $"User verified at: " + user.Data.VerifiedAt.ToString()
            };
            return response;


        }
        public async Task<ServerResponse> ForgotPassword(string email)
        {
            var response = new ServerResponse();
            var forgotPasswordUser = await _userService.ForgotPasswordTokenCreation(email);
            if (!forgotPasswordUser.IsValid)
            {
                response.Errors.AddRange(forgotPasswordUser.Errors);
                return response;
            }

            var emailBody =
                "<div id= \"m_-7489015765851718993CONTENEDOR\" style= \"background: #fff;margin: 0px auto;width: 90 %;max - width: 800px!important;\">" +
                    "<div style=\"background: #e0dfdd;padding: 40px 25px;color: #333;text-align: center;font-family: Gotham, 'Helvetica Neue', Helvetica, Arial, sans-serif;font-size: 14px;margin-bottom: 1px;\">"
                        + "<span class=\"im\">" +
                            "<p>" +
                                "<strong>Para restablecer tu contraseña, haz click en el siguiente link</strong>" +
                                        "<br />" +
                                        "<br />" +
                                        "<br />" +
                                        "<br />" +
                                        "<p style = \"background: #23b582;padding: 17px 50px;border-radius: 3px;color: #fff;text-decoration: none;font-weight: bold;font-size: 12px;border-bottom: solid 3px #1e9c70;\">#CODIGO#" +
                                        "</p>"
                                      + "</p>" +
                                      "<p>&nbsp;</p>" +
                                    "</span>" +
                                  "</div>" +
                                "</div>";
           

            var body = emailBody.Replace("#CODIGO#", forgotPasswordUser.Data.PasswordResetToken);
            

            // send email
            _emailService.SendEmail(email, "Reset Password", body);

            return response;
        }
        public async Task<ServerResponse> ResetPassword(ResetPasswordRequest request, Guid userId)
        {
            return await _userService.ResetPassword(request, userId);
        }
        public async Task<ServerResponse> UpdateState(Guid id, int stateId)
        {
            var response = new ServerResponse();
            // check if user exists
            var user = await _userService.FindByIdAsync(id);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            // update state
            var userToUpdate = user.Map();
            userToUpdate.State = stateId;
            var success = await _userService.UpdateUserAsync(userToUpdate);
            if (!success.IsValid)
            {
                response.Errors.AddRange(success.Errors);
                return response;
            }
            return success;
        }
        public Task<bool> ChangeEmail()
        {
            throw new NotImplementedException();
        }
        public Task<bool> Logout(string? from, string? to)
        {
            throw new NotImplementedException();
        }
        public async Task<ServerResponse> ValidateTokenExpirationDate(ValidateResetPasswordTokenRequest request, Guid userId) 
        {
            var response = new ServerResponse();
            var user = await _userService.IsValidResetToken(request, userId);
            if (!user.IsValid)
            {
                response.Errors.AddRange(user.Errors);
                return response;
            }
            return response;
        }

        public string GetToken(string email, Guid id)
        {
            return _userService.GetToken(email, id);
        }
    }
}
