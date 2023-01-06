using CV.Authentication.AccessData.Interfaces;
using CV.Authentication.Application.Interfaces;
using CV.Authentication.Domain.Mappers;
using CV.Authentication.Application.Tools;
using CV.Authentication.Domain.Common;
using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Request;
using CV.Authentication.Domain.DTOs.Response;
using CV.Authentication.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CV.Authentication.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly JwtConfig _appSettings;

        public UserService(IUserRepository repository, IOptions<JwtConfig> appSettings)
        {
            _repository = repository;
            _appSettings = appSettings.Value;
        }

        public ICollection<UserResponse> AllUsers()
        {
            var lista = _repository.GetAllUsers().Select(x => x.ToUserMap()).ToList();
            return lista;
        }
        public async Task<bool> FindByEmail(string email)
        {
            return await _repository.FindByEmailAsync(email);
        }
        public async Task<ServerResponse<UserDto>> CreateAsync(string email, string password)
        {
            var result = new ServerResponse<UserDto>();
            try
            {
                Encrypt.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                var newUser = new User
                {
                    Email = email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerificationToken = Tools.Tools.CreateCode(),
                    State = 4
                };
                await _repository.AddUser(newUser);
                var user = await _repository.GetByEmailAsync(email);
                result.Data = user;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                return result;
            }
        }
        
        public string GetToken(string email, Guid id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
        public Task<UserDto> FindByEmailAsync(string email)
        {
            return _repository.GetByEmailAsync(email);
        }
        public async Task<ServerResponse> UpdateUserAsync(User userToUpdate)
        {
            var response = new ServerResponse();
            try
            {
                await _repository.Update(userToUpdate);
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }
        public async Task<UserDto?> FindByIdAsync(Guid id) => await _repository.GetUserById(id);
        public async Task<ServerResponse<UserDto>> VerifyEmailToken(VerifyEmailRequest request, Guid userId)
        {
            var result = new ServerResponse<UserDto>();
            var userDto = await _repository.GetUserToVerify(userId, request.Token);
            if (userDto == null)
            {
                result.Errors.Add("Token no valido");
                return result;
            }
            var user = userDto.Map();
            user.VerifiedAt = DateTime.UtcNow;
            await _repository.Update(user);
            result.Data = userDto;
            return result;
        }
        
        public async Task<ServerResponse<UserDto>> ForgotPasswordTokenCreation(string email)
        {
            var response = new ServerResponse<UserDto>();
            try
            {
                var userDto = await _repository.GetByEmailAsync(email);
                if (userDto == null)
                {
                    response.Errors.Add("User not found");
                    return response;
                }
                var user = userDto.Map();
                user.PasswordResetToken = Tools.Tools.CreateCode();
                user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
                await _repository.Update(user);
                response.Data = user.Map();
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }
            return response;
        }
        public async Task<ServerResponse> IsValidResetToken(ValidateResetPasswordTokenRequest request, Guid userId)
        {
            var response = new ServerResponse();
            var user = await _repository.GetUserById(userId);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            if (user.PasswordResetToken != request.Token)
            {
                response.Errors.Add("Token no valido");
                return response;
            }
            if (user.ResetTokenExpires < DateTime.UtcNow)
            {
                response.Errors.Add("Token expirado");
                return response;
            }
            return response;
        }
        
        public async Task<ServerResponse> ResetPassword(ResetPasswordRequest request, Guid userId)
        {
            var result = new ServerResponse();
            var user = await _repository.GetUserById(userId);
            if (user == null)
            {
                result.Errors.Add("User not found");
                return result;
            }
            var userToUpdate = user.Map();
            Encrypt.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userToUpdate.PasswordHash = passwordHash;
            userToUpdate.PasswordSalt = passwordSalt;
            userToUpdate.PasswordResetToken = null;
            userToUpdate.ResetTokenExpires = null;
            await _repository.Update(userToUpdate);
            return result;
        }
    }
}