using SentiVerse.Domain.Entities;
using SentiVerse.Domain.Interfaces;
using SentiVerse.Infrastructure.Persistence;

namespace SentiVerse.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IRepository<EmotionGroup> EmotionGroups { get; }
    public IRepository<GroupMembership> GroupMemberships { get; }
    public IRepository<Message> Messages { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        EmotionGroups = new Repository<EmotionGroup>(context);
        GroupMemberships = new Repository<GroupMembership>(context);
        Messages = new Repository<Message>(context);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
