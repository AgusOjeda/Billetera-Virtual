using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Authentication.Domain.DTOs.Request;

public class UserLoginRequest
{
    [Required, EmailAddress]
    public String Email { get; set; } = string.Empty;
    [Required]
    public String Password { get; set; } = string.Empty;
}

