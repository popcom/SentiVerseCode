using SentiVerse.Domain.Entities;

namespace SentiVerse.Domain.Interfaces;

public interface IEmotionService
{
    Task<string> AnalyzeEmotionAsync(string text);
    Task<EmotionGroup> SuggestGroupAsync(Guid userId, string emotionType);
}
