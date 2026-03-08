using ImoblyAI.Api.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ImoblyAI.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
}