using ChatApp.Data;
using ChatApp.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupController: Controller
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }
    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery] PaginationModel paginationModel)
    {
        var groups = await _groupService.ListAllAsync(paginationModel);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = groups.Adapt<List<GroupModel>>(),
            StatusCode = 200,
            TotalCount = await _groupService.CountAsync()
        };
    }
    [HttpGet("{id}")]
    public async Task<ReturnModel> Get(int id)
    {
        var group = await _groupService.GetByIdAsync(id);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = group.Adapt<GroupModel>(),
            StatusCode = 200
        };
    }
    [HttpPost]
    public async Task<ReturnModel> Post([FromBody] GroupCreateModel groupModel)
    {
        var group = groupModel.Adapt<Group>();
        var groupResult = await _groupService.AddAsync(group);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = groupResult.Adapt<GroupModel>(),
            StatusCode = 200
        };
    }
    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] GroupUpdateModel groupModel)
    {
        var group = groupModel.Adapt<Group>();
        var groupResult = await _groupService.UpdateAsync(group);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = groupResult.Adapt<GroupModel>(),
            StatusCode = 200
        };
    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id)
    {
        var group = await _groupService.GetByIdAsync(id);
        await _groupService.DeleteAsync(group);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            StatusCode = 200
        };
    }
}
