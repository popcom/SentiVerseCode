using System.Net.Http.Json;
using SentiVerse.Domain.Entities;
using SentiVerse.Domain.Interfaces;

namespace SentiVerse.Application.Services;

public class EmotionService : IEmotionService
{
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _uow;

    public EmotionService(HttpClient httpClient, IUnitOfWork uow)
    {
        _httpClient = httpClient;
        _uow = uow;
    }

    public async Task<string> AnalyzeEmotionAsync(string text)
    {
        var response = await _httpClient.PostAsJsonAsync("/analyze-sentiment", new { text });
        var result = await response.Content.ReadFromJsonAsync<SentimentResponse>() ?? new SentimentResponse();
        return result.label ?? "";
    }

    public async Task<EmotionGroup> SuggestGroupAsync(Guid userId, string emotionType)
    {
        var groups = await _uow.EmotionGroups.GetAllAsync();
        var group = groups.FirstOrDefault(g => g.EmotionType == emotionType && g.IsActive);
        if (group == null)
        {
            group = new EmotionGroup { GroupId = Guid.NewGuid(), EmotionType = emotionType };
            await _uow.EmotionGroups.AddAsync(group);
            await _uow.SaveChangesAsync();
        }
        return group;
    }

    private record SentimentResponse(string? label = null, double score = 0);
}
