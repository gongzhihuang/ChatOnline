using ChatOnline.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatOnline.Server
{
    public class IMDbContext : DbContext
    {
        public DbSet<IMUser> IMUsers { get; set; }

        public DbSet<FriendsRelation> FriendsRelations { get; set; }

        public IMDbContext(DbContextOptions<IMDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IMUser>()
                .ToTable("iMUser")
                .HasKey(x => x.Id);

            modelBuilder.Entity<FriendsRelation>()
            .ToTable("friendsRelation")
            .HasKey(x => x.Id);
        }
    }
}