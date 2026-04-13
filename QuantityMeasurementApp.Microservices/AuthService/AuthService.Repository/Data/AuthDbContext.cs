using AuthService.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    public DbSet<UserEntity> Users => Set<UserEntity>();
}