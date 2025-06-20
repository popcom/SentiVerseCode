using System;

namespace SentiVerse.Domain.Entities;

public class GroupMembership
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}
