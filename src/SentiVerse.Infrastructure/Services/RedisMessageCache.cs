using StackExchange.Redis;
using SentiVerse.Domain.Entities;

namespace SentiVerse.Infrastructure.Services;

public class RedisMessageCache
{
    private readonly IDatabase _db;
    private const string Prefix = "group_messages:";

    public RedisMessageCache(IConnectionMultiplexer connection)
    {
        _db = connection.GetDatabase();
    }

    public async Task AddMessageAsync(Message message)
    {
        var key = Prefix + message.GroupId;
        var serialized = System.Text.Json.JsonSerializer.Serialize(message);
        await _db.ListRightPushAsync(key, serialized);
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(Guid groupId, int count = 50)
    {
        var key = Prefix + groupId;
        var entries = await _db.ListRangeAsync(key, -count, -1);
        return entries.Select(e => System.Text.Json.JsonSerializer.Deserialize<Message>(e!)).Where(m => m != null)!;
    }
}
