using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventory.Models
{
    public partial class disk_inventoryjmContext : DbContext
    {
        public disk_inventoryjmContext()
        {
        }

        public disk_inventoryjmContext(DbContextOptions<disk_inventoryjmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Disk> Disks { get; set; }
        public virtual DbSet<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual DbSet<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual DbSet<DiskType> DiskTypes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ViewIndividualArtist> ViewIndividualArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=disk_inventoryjm;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.ArtistFname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("artistFName");

                entity.Property(e => e.ArtistLname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("artistLName");

                entity.Property(e => e.ArtistTypeCode).HasColumnName("artistTypeCode");

                entity.HasOne(d => d.ArtistTypeCodeNavigation)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artist__artistTy__2B3F6F97");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.HasKey(e => e.ArtistTypeCode)
                    .HasName("PK__artistTy__529C0FA8E9993FCE");

                entity.ToTable("artistType");

                entity.Property(e => e.ArtistTypeCode).HasColumnName("artistTypeCode");

                entity.Property(e => e.ArtistTypeDesc)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("artistTypeDesc");
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrowerID");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("lname");

                entity.Property(e => e.Mi)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mi");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Disk>(entity =>
            {
                entity.ToTable("disk");

                entity.Property(e => e.DiskId).HasColumnName("diskID");

                entity.Property(e => e.DiskName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("diskName");

                entity.Property(e => e.DiskTypeId).HasColumnName("diskTypeID");

                entity.Property(e => e.GenreCode).HasColumnName("genreCode");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("releaseDate");

                entity.Property(e => e.StatusCode).HasColumnName("statusCode");

                entity.HasOne(d => d.DiskType)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.DiskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__diskTypeID__31EC6D26");

                entity.HasOne(d => d.GenreCodeNavigation)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.GenreCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__genreCode__30F848ED");

                entity.HasOne(d => d.StatusCodeNavigation)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.StatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__statusCode__300424B4");
            });

            modelBuilder.Entity<DiskHasArtist>(entity =>
            {
                entity.ToTable("diskHasArtist");

                entity.Property(e => e.DiskHasArtistId).HasColumnName("diskHasArtistID");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.DiskId).HasColumnName("diskID");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.DiskHasArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__diskHasAr__artis__3A81B327");

                entity.HasOne(d => d.Disk)
                    .WithMany(p => p.DiskHasArtists)
                    .HasForeignKey(d => d.DiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__diskHasAr__diskI__3B75D760");
            });

            modelBuilder.Entity<DiskHasBorrower>(entity =>
            {
                entity.ToTable("diskHasBorrower");

                entity.Property(e => e.DiskHasBorrowerId).HasColumnName("diskHasBorrowerID");

                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("borrowedDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BorrowerId).HasColumnName("borrowerID");

                entity.Property(e => e.DiskId).HasColumnName("diskID");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("dueDate")
                    .HasDefaultValueSql("(getdate()+(30))");

                entity.Property(e => e.ReturnedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("returnedDate");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__diskHasBo__borro__36B12243");

                entity.HasOne(d => d.Disk)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.DiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__diskHasBo__diskI__37A5467C");
            });

            modelBuilder.Entity<DiskType>(entity =>
            {
                entity.HasKey(e => e.DiskTypeCode)
                    .HasName("PK__diskType__AB3D76D7BA9948D6");

                entity.ToTable("diskType");

                entity.Property(e => e.DiskTypeCode).HasColumnName("diskTypeCode");

                entity.Property(e => e.DiskTypeDesc)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("diskTypeDesc");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreCode)
                    .HasName("PK__genre__3DFFACF42E11187C");

                entity.ToTable("genre");

                entity.Property(e => e.GenreCode).HasColumnName("genreCode");

                entity.Property(e => e.GenreDesc)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("genreDesc");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StatusCode)
                    .HasName("PK__status__AD4366F7242281E6");

                entity.ToTable("status");

                entity.Property(e => e.StatusCode).HasColumnName("statusCode");

                entity.Property(e => e.StatusDesc)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("statusDesc");
            });

            modelBuilder.Entity<ViewIndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Individual_Artist");

                entity.Property(e => e.ArtistFname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("artistFName");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.ArtistLname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("artistLName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
