using System;

namespace SentiVerse.Domain.Entities;

public class EmotionCapture
{
    public Guid CaptureId { get; set; }
    public Guid UserId { get; set; }
    public string EmotionType { get; set; } = string.Empty;
    public string CaptureContent { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
