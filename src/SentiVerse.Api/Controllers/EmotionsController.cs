using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentiVerse.Application.DTOs.Emotions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SentiVerse.Application.Services;
using SentiVerse.Domain.Entities;

namespace SentiVerse.Api.Controllers;

[ApiController]
[Route("api/emotions")]
[Authorize]
public class EmotionsController : ControllerBase
{
    private readonly IEmotionService _emotionService;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmotionsController(IEmotionService emotionService, UserManager<ApplicationUser> userManager)
    {
        _emotionService = emotionService;
        _userManager = userManager;
    }

    [HttpPost("capture")]
    public async Task<IActionResult> Capture(EmotionCaptureDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var emotion = await _emotionService.AnalyzeEmotionAsync(dto.Text);
        var group = await _emotionService.SuggestGroupAsync(userId, emotion);
        return Ok(new { groupId = group.GroupId, emotion });
    }
}
