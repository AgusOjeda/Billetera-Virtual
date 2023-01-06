using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Domain.DTOs
{
    public record UserDto
    (
        Guid Id,
        string Email,
        byte[] PasswordHash,
        byte[] PasswordSalt,
        string? VerificationToken,
        DateTime? VerifiedAt,
        string? PasswordResetToken,
        DateTime? ResetTokenExpires,
        int State
    );
}
