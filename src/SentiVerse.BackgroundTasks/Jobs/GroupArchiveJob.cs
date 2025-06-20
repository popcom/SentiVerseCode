using Microsoft.Extensions.Logging;
using SentiVerse.Domain.Interfaces;

namespace SentiVerse.BackgroundTasks.Jobs;

public class GroupArchiveJob
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GroupArchiveJob> _logger;

    public GroupArchiveJob(IUnitOfWork uow, ILogger<GroupArchiveJob> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task ArchiveExpiredGroupsAsync()
    {
        var groups = await _uow.EmotionGroups.GetAllAsync();
        var expired = groups.Where(g => g.CreatedAt <= DateTime.UtcNow.AddHours(-6) && g.IsActive).ToList();
        foreach (var group in expired)
        {
            group.IsActive = false;
        }
        await _uow.SaveChangesAsync();
        _logger.LogInformation("Archived {Count} groups", expired.Count);
    }
}
