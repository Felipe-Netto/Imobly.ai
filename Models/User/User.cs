using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImoblyAI.Api.Enums.Subscription;

namespace ImoblyAI.Api.Models.User;

[Table("users")]
public class User
{
    public User()
    {
        Id = Guid.NewGuid();
        EmailVerified = false;
        SubscriptionStatus = SubscriptionStatus.Trialing;
        CreditsRemaining = 3;
        Role = Enums.User.Roles.User;
        CreatedAt = DateTime.UtcNow;
    }

    [Key] [Column("id")] public Guid Id { get; set; }

    [Column("name")] public string Name { get; set; }

    [Column("document")] public string Document { get; set; }

    [Column("email")] public string Email { get; set; }
    
    [Column("email_verified")] public bool EmailVerified { get; set; }

    [Column("password")] public string Password { get; set; }

    [Column("stripe_customer_id")] public string? StripeCustomerId { get; set; }

    [Column("subscription_status")] public SubscriptionStatus SubscriptionStatus { get; set; }

    [Column("credits_remaining")] public int CreditsRemaining { get; set; }

    [Column("role")] public Enums.User.Roles Role { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }
}