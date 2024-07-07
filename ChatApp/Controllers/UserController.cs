using ChatApp.Data;
using ChatApp.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery]PaginationModel paginationModel)
    {
        var users = await _userService.ListAllAsync(paginationModel);
        return new ReturnModel{
            Success = true,
            Message = "Users fetched succesfully",
            Data = users.Adapt<List<UserModel>>(),
            StatusCode = 200,
            TotalCount = await _userService.CountAsync()
        };
    }

    [HttpGet("{id}")]
    public async Task<ReturnModel> Get(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        return new ReturnModel{
            Success = true,
            Message = "Success",
            Data = user.Adapt<UserModel>(),
            StatusCode = 200
        };
    }

    [HttpPost]
    
    public async Task<ReturnModel> Post([FromBody] UserCreateModel userCreateModel)
    {
        var userModel = userCreateModel.Adapt<User>();
        var newUser = await _userService.AddAsync(userModel);
        return new ReturnModel{
            Success = true,
            Message = "User created successfully",
            Data = newUser,
            StatusCode = 201
        };
    }

    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] UserUpdateModel userModel)
    {
        var user = userModel.Adapt<User>();
        await _userService.UpdateAsync(user);
        return new ReturnModel{
            Success = true,
            Message = "User updated successfully",
            Data = user.Adapt<UserModel>(),
            StatusCode = 200
        };
    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        await _userService.DeleteAsync(user);
        return new ReturnModel{
            Success = true,
            Message = "User deleted successfully",
            StatusCode = 200
        };
    }
}
