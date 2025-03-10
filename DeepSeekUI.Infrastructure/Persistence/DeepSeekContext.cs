using DeepSeekUI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeepSeekUI.Infrastructure.Persistence
{
    public class DeepSeekContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DeepSeekContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Chat>()
                .HasKey(x => x.Id);

            builder.Entity<Chat>()
                .HasOne<IdentityUser<Guid>>()
                .WithMany()
                .HasPrincipalKey(x=>x.Id)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Chat>()
                .HasMany(x => x.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);

            builder.Entity<Chat>()
                .Metadata
                .FindNavigation(nameof(Chat.Messages))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Entity<Message>()
                .HasKey(x => x.Id);

            builder.Entity<IdentityUser<Guid>>()
                .HasData(new IdentityUser<Guid>("Usuario prueba")
                {
                    Id = Guid.NewGuid(),
                    Email = "user@mail.com",
                    NormalizedEmail = "USER@MAIL.COM",
                    NormalizedUserName = "USUARIO PRUEBA",
                    PasswordHash = new PasswordHasher<IdentityUser<Guid>>().HashPassword(null, "123456"),
                    SecurityStamp = Guid.NewGuid().ToString()
                });
        }
    }
}
