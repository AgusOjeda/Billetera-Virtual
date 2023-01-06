using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Response;
using CV.Authentication.Domain.Entities;

namespace CV.Authentication.Domain.Mappers
{
    public static class UsersMapper
    {
        public static UserResponse ToUserMap(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                State = user.UserAccountState.ToStateDto()
            };
        }
        public static StateDto ToStateDto(this UserAccountState state)
        {
            return new StateDto
            {
                StateId = state.Id,
                Description = state.Description
            };
        }

        public static UserDto Map(this User user)
        {
            return new UserDto
                (
                    Id: user.Id,
                    Email: user.Email,
                    PasswordHash: user.PasswordHash,
                    PasswordSalt: user.PasswordSalt,
                    VerificationToken: user.VerificationToken,
                    VerifiedAt: user.VerifiedAt,
                    PasswordResetToken: user.PasswordResetToken,
                    ResetTokenExpires: user.ResetTokenExpires,
                    State: user.State
                );
        }
        public static User Map(this UserDto user)
        {
            return new User
            {
                    Id= user.Id,
                    Email= user.Email,
                    PasswordHash= user.PasswordHash,
                    PasswordSalt= user.PasswordSalt,
                    VerificationToken= user.VerificationToken,
                    VerifiedAt= user.VerifiedAt,
                    PasswordResetToken= user.PasswordResetToken,
                    ResetTokenExpires= user.ResetTokenExpires,
                    State= user.State
            };
        }
    }
}
