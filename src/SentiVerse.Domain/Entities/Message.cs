using System;

namespace SentiVerse.Domain.Entities;

public class Message
{
    public Guid MessageId { get; set; }
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
