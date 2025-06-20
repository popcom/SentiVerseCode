using SentiVerse.Domain.Entities;

namespace SentiVerse.Domain.Interfaces;

public interface IUnitOfWork
{
    IRepository<EmotionGroup> EmotionGroups { get; }
    IRepository<GroupMembership> GroupMemberships { get; }
    IRepository<Message> Messages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
}
