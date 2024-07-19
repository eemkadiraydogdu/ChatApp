using ChatApp.Data;
using ChatApp.Services;
using ChatApp.Services.Helpers;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
     private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ReturnModel> Login([FromBody] LoginModel loginModel)
    {
        var user = await _userService.GetByUsernameAndPasswordAsync(loginModel.Username, loginModel.Password);
        if (user == null)
        {
            return new ReturnModel
            {
                Success = false,
                Message = "Invalid username or password",
                StatusCode = 400
            };
        }
        var tokenModel = new TokenModel
        {
            UserName = user.UserName,
            Role = user.Role,
            SigningKey = _configuration["Jwt:SigningKey"],
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = JwtToken.GenerateToken(tokenModel);
        return new ReturnModel
        {
            Success = true,
            Message = "Login successful",
            Data = token,
            StatusCode = 200
        };
    }

    [HttpPost("register")]
    public async Task<ReturnModel> Register([FromBody] UserCreateModel userCreateModel)
    {
        var newUser = userCreateModel.Adapt<User>();
        var addedUser = await _userService.AddAsync(newUser);
        return new ReturnModel{
            Success = true,
            Message = "User Created Succefully",
            Data = addedUser,
            StatusCode = 201
        };
    }
}
