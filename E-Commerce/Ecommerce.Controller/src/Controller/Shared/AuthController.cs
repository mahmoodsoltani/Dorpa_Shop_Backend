using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.AuthService;
using Ecommerce.Service.src.UserServiceAggregate.UserAggregate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller
{
    [ApiController]
    [Route("api/V1/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(
            [FromBody] UserCredentials userCredentials
        )
        {
            if (
                userCredentials == null
                || string.IsNullOrEmpty(userCredentials.Email)
                || string.IsNullOrEmpty(userCredentials.Password)
            )
            {
                throw new InvalidInputDataException("Email and password are required.");
            }

            var loginResponse = await _authService.LoginAsync(userCredentials);

            if (string.IsNullOrEmpty(loginResponse.Token))
            {
                throw new AuthenticationException("Invalid Email or Password.");
            }

            return Ok(loginResponse);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                throw new AuthenticationException();
            }

            await _authService.LogoutAsync(token);
            return Ok(new { Message = "Logged out successfully" });
        }

        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle(string returnUrl = "/")
        {
            // var redirectUrl = Url.Action(
            //     nameof(GoogleResponse),
            //     "Auth",
            //     new { ReturnUrl = returnUrl },
            //     Request.Scheme
            // );
            var newReturnUrl =
                "https://dorpa.azurewebsites.net/api/v1/auth/google-response?returnUrl="
                + returnUrl;
            var properties = new AuthenticationProperties { RedirectUri = newReturnUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // Handle the response from Google after user login
        [HttpPost("google-login")]
        public async Task<ActionResult<LoginResponse>> GoogleLogin(UserCreateDto createDto)
        {
            var loginResponse = await _authService.LoginWithGoogleAsync(createDto);

            // Assuming `LoginWithGoogleAsync` returns a valid token
            if (string.IsNullOrEmpty(loginResponse.Token))
            {
                throw new AuthenticationException("Google authentication failed.");
            }

            return Ok(loginResponse);
        }
    }
}
