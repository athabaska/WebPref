using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Lobby;
using WebPref.Core.Playing;

namespace WebPref.Core
{
    internal class PrefContext : DbContext
    {
        /*
        // когда более-менее устаканится, думаю, надо использовать конструктор, а пока для миграций как-то надо строку соединения задать
        public PrefContext(DbContextOptions<PrefContext> options) : base(options)
        {

        }
        */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Preferance;Trusted_Connection=True;");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(p => p.Id);
            modelBuilder.Entity<Player>().Property(p => p.Id).HasMaxLength(450);
            modelBuilder.Entity<Player>().Property(p => p.Name).HasMaxLength(256);

            modelBuilder.Entity<Table>().HasKey(t => t.Id);
            modelBuilder.Entity<Table>().Property(t => t.Id).HasMaxLength(32);


            modelBuilder.Entity<TablePlayers>().HasKey(tp => new { tp.PlayerId, tp.TableId });
            
            modelBuilder.Entity<TablePlayers>()
                .HasOne(tp => tp.Player)
                .WithMany(p => p.Tables)
                .HasForeignKey(tp => tp.PlayerId);

            modelBuilder.Entity<TablePlayers>()
                .HasOne(tp => tp.Table)
                .WithMany(t => t.TablePlayers)
                .HasForeignKey(tp => tp.TableId);
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Player> Players { get; set; }

        
        
    }
}
