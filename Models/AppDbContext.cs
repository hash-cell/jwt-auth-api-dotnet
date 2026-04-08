using System.Security.Cryptography.X509Certificates;
using LoginJWT.Models;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Core.Tokens;

namespace LoginJWT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Login> Logins { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
