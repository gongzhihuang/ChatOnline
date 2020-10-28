using ChatOnline.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatOnline.Server
{
    public class IMDbContext : DbContext
    {
        public DbSet<ChatOnlineUser> ChatOnlineUsers { get; set; }
        public DbSet<FriendsRelation> FriendsRelations { get; set; }
        public DbSet<ChatRecord> ChatRecords { get; set; }

        public IMDbContext(DbContextOptions<IMDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatOnlineUser>()
                .ToTable("chatOnlineUser")
                .HasKey(x => x.Id);

            modelBuilder.Entity<FriendsRelation>()
                .ToTable("friendsRelation")
                .HasKey(x => x.Id);

            modelBuilder.Entity<ChatRecord>()
                .ToTable("chatRecord")
                .HasKey(x => x.Id);
        }
    }
}