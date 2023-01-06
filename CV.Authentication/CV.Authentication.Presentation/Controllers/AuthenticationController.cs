using Microsoft.AspNetCore.Mvc;
using CV.Authentication.Application.Interfaces;
using CV.Authentication.Domain.Entities;
using CV.Authentication.Domain.DTOs.Request;
using CV.Authentication.Domain.DTOs.Response;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CV.Authentication.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserHandler _handler;
        private readonly IEmailService _emailService;

        public AuthenticationController(IUserHandler handler, IEmailService emailService)
        {
            _emailService = emailService;
            _handler = handler;
        }
        
        [HttpPost("register")]
        [ProducesResponseType(typeof(ServerResponse<UserResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ServerResponse<UserResponse>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _handler.Register(model);
                    if (user.IsValid)
                    {
                        var token = _handler.GetToken(user.Data.Email, user.Data.Id);
                        // Email
                        var emailBody = "<div id= \"m_-7489015765851718993CONTENEDOR\" style= \"background: #fff;margin: 0px auto;width: 90 %;max - width: 800px!important;\">" +
                    "<div style=\"background: #e0dfdd;padding: 40px 25px;color: #333;text-align: center;font-family: Gotham, 'Helvetica Neue', Helvetica, Arial, sans-serif;font-size: 14px;margin-bottom: 1px;\">"
                        +"<h1>Bienvenido a GStick</h1>"+
                        "<span class=\"im\">" +
                            "<p>" +
                                "<strong>Para validar tu correo electronico, ingresa el siguiente codigo:</strong>" +
                                        "<br />" +
                                        "<br />" +
                                        "<br />" +
                                        "<br />" +
                                        "<p style = \"background: #23b582;padding: 17px 50px;border-radius: 3px;color: #fff;text-decoration: none;font-weight: bold;font-size: 12px;border-bottom: solid 3px #1e9c70;\">##CODIGO##" +
                                        "</p>"
                                      + "</p>" +
                                      "<p>&nbsp;</p>" +
                                    "</span>" +
                                  "</div>" +
                                "</div>";


                        var body = emailBody.Replace("##CODIGO##", user.Data.VerificationToken);

                        // send email
                        var x = _emailService.SendEmail(user.Data.Email, "Bienvenido a GStick", body);
                        return StatusCode(201, new { UserId = user.Data.Id, Token = token});
                    }
                    else
                    {
                        string errorMessagge = "";
                        foreach (var error in user.Errors)
                        {
                            errorMessagge += error;
                        }
                        return StatusCode(409, new { Message = errorMessagge });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = ex.Message });
                }
            }
            return BadRequest();
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(ServerResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ServerResponse<UserResponse>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _handler.Login(model);
                    if (user.IsValid)
                    {
                        return StatusCode(200, user);
                    }
                    else
                    {
                        string errorMessagge = "";
                        foreach (var error in user.Errors)
                        {
                            errorMessagge += error;
                        }
                        return StatusCode(400, new { Message = errorMessagge });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = ex.Message });
                }
            }
            return BadRequest();
        }
        [HttpPost("verifyEmail")]
        [ProducesResponseType(typeof(ServerResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Verify([FromQuery] VerifyEmailRequest request)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _handler.VerifyEmail(request, userId);
                    if (user.IsValid)
                    {
                        return StatusCode(200, user);
                    }
                    else
                    {
                        string errorMessagge = "";
                        foreach (var error in user.Errors)
                        {
                            errorMessagge += error;
                        }
                        return StatusCode(400, new { Message = errorMessagge });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = ex.Message });
                }
            }
            return BadRequest();
        }
        [HttpPost("forgotPassword")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _handler.ForgotPassword(email);
                    if (user.IsValid)
                    {
                        

                        return StatusCode(200, user);
                    }
                    else
                    {
                        string errorMessagge = "";
                        foreach (var error in user.Errors)
                        {
                            errorMessagge += error;
                        }
                        return StatusCode(400, new { Message = errorMessagge });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = ex.Message });
                }
            }
            return BadRequest();
        }
        [HttpPost("token/isValid")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> IsValid([FromBody] ValidateResetPasswordTokenRequest request)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _handler.ValidateTokenExpirationDate(request, userId);
                    if (response.IsValid)
                    {
                        return StatusCode(200, response);
                    }
                    else
                    {
                        string errorMessagge = "";
                        foreach (var error in response.Errors)
                        {
                            errorMessagge += error;
                        }
                        return StatusCode(400, new { Message = errorMessagge });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = ex.Message });
                }
            }
            return BadRequest();
        }
        [HttpPost("resetPassword")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _handler.ResetPassword(request, userId);
                    if (user.IsValid)
                    {
                        return StatusCode(200, user);
                    }
                    else
                    {
                        string errorMessagge = "";
                        foreach (var error in user.Errors)
                        {
                            errorMessagge += error;
                        }
                        return StatusCode(400, new { Message = errorMessagge });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = ex.Message });
                }
            }
            return BadRequest();
        }

        [HttpPost("changeState")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> ChangeState([FromBody] ChangeStateRequest request)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _handler.UpdateState(userId, request.State);
                    if (user.IsValid)
                    {
                        return StatusCode(200, user);
                    }
                    else
                    {
                        string errorMessagge = "";
                        foreach (var error in user.Errors)
                        {
                            errorMessagge += error;
                        }
                        return StatusCode(400, new { Message = errorMessagge });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = ex.Message });
                }
            }
            return BadRequest();
        }


    }
}