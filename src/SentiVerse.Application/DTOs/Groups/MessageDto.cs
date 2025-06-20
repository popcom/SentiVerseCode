using System;

namespace SentiVerse.Application.DTOs.Groups;

public record MessageDto(Guid GroupId, string Content);
