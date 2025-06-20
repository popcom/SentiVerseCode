using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SentiVerse.Application.DTOs.Groups;
using SentiVerse.Application.Services;
using SentiVerse.Infrastructure.Services;
using SentiVerse.Domain.Entities;

using SentiVerse.Domain.Interfaces;
namespace SentiVerse.Api.Controllers;

[ApiController]
[Route("api/groups")]
[Authorize]
public class GroupsController : ControllerBase
{
    private readonly GroupService _groupService;
    private readonly RedisMessageCache _cache;
    private readonly IUnitOfWork _uow;

    public GroupsController(GroupService groupService, RedisMessageCache cache, IUnitOfWork uow)
    {
        _groupService = groupService;
        _cache = cache;
        _uow = uow;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive([FromQuery] string emotionType)
    {
        var groups = await _groupService.GetActiveGroupsAsync(emotionType);
        return Ok(groups);
    }

    [HttpPost("join")]
    public async Task<IActionResult> Join(JoinGroupDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var membership = new GroupMembership { GroupId = dto.GroupId, UserId = userId };
        await _uow.GroupMemberships.AddAsync(membership);
        await _uow.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{groupId}/messages")]
    public async Task<IActionResult> Messages(Guid groupId)
    {
        var messages = await _cache.GetMessagesAsync(groupId);
        return Ok(messages);
    }
}
