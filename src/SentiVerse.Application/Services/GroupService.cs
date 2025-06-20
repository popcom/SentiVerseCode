using SentiVerse.Domain.Entities;
using SentiVerse.Domain.Interfaces;

namespace SentiVerse.Application.Services;

public class GroupService
{
    private readonly IUnitOfWork _uow;

    public GroupService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<EmotionGroup>> GetActiveGroupsAsync(string emotionType)
    {
        var groups = await _uow.EmotionGroups.GetAllAsync();
        return groups.Where(g => g.EmotionType == emotionType && g.IsActive);
    }
}
