using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SentiVerse.Domain.Entities;
using SentiVerse.Infrastructure.Services;
using System.Security.Claims;

namespace SentiVerse.Api.Hubs;

[Authorize]
public class EmotionGroupHub : Hub
{
    private readonly RedisMessageCache _cache;

    public EmotionGroupHub(RedisMessageCache cache)
    {
        _cache = cache;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string groupId, string content)
    {
        var userId = Guid.Parse(Context.User!.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        var message = new Message { MessageId = Guid.NewGuid(), GroupId = Guid.Parse(groupId), UserId = userId, Content = content };
        await _cache.AddMessageAsync(message);
        await Clients.Group(groupId).SendAsync("ReceiveMessage", new { message.MessageId, message.UserId, message.Content, message.CreatedAt });
    }
}
