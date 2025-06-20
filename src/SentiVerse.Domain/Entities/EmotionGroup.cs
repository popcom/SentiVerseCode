using System;
using System.Collections.Generic;

namespace SentiVerse.Domain.Entities;

public class EmotionGroup
{
    public Guid GroupId { get; set; }
    public string EmotionType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public ICollection<GroupMembership> Members { get; set; } = new List<GroupMembership>();
}
