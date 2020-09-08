using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FootballProject
{
    public partial class FootballProjectContext : DbContext
    {
        public FootballProjectContext()
        {
        }

        public FootballProjectContext(DbContextOptions<FootballProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlayerStats> PlayerStats { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FootballProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerStats>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.StatId)
                    .HasColumnName("StatID")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Player)
                    .WithMany()
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK__PlayerSta__Playe__5BE2A6F2");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PK__tmp_ms_x__4A4E74A8655E0258");

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.HeightInches).HasColumnName("Height(inches)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nationality).IsUnicode(false);

                entity.Property(e => e.Position).IsUnicode(false);

                entity.Property(e => e.StrongestFoot).IsUnicode(false);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__Players__TeamID__4E88ABD4");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.HasKey(e => e.TeamId)
                    .HasName("PK__tmp_ms_x__123AE7B92C7B4DAB");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.TeamDescription).IsUnicode(false);

                entity.Property(e => e.TeamName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
