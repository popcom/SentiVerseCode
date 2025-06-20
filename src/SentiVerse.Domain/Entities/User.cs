using System;
using Microsoft.AspNetCore.Identity;

namespace SentiVerse.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
