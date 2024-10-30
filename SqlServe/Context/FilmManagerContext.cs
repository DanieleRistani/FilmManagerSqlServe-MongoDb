using System;
using System.Collections.Generic;
using FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;
using Microsoft.EntityFrameworkCore;

namespace FilmManagerSqlServe_MongoDb.SqlServe.Context;

public partial class FilmManagerContext : DbContext
{
    public FilmManagerContext()
    {
    }

    public FilmManagerContext(DbContextOptions<FilmManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmsTagsPivot> FilmsTagsPivots { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MJCAS12\\SQLEXPRESS;Initial Catalog=FilmManager;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("Category_id");
            entity.Property(e => e.CategoryDescription).HasColumnName("Category_description");
            entity.Property(e => e.CategoryImg).HasColumnName("Category_img");
            entity.Property(e => e.CategoryName).HasColumnName("Category_name");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.Property(e => e.FilmId).HasColumnName("Film_id");
            entity.Property(e => e.FilmCategoryId).HasColumnName("Film_category_id");
            entity.Property(e => e.FilmDirector).HasColumnName("Film_director");
            entity.Property(e => e.FilmName).HasColumnName("Film_name");
            entity.Property(e => e.FilmRelaseDate).HasColumnName("Film_relase_date");
            entity.Property(e => e.FilmUrlImg).HasColumnName("Film_UrlImg");

            entity.HasOne(d => d.FilmCategory).WithMany(p => p.Films)
                .HasForeignKey(d => d.FilmCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Films_Categories");
        });

        modelBuilder.Entity<FilmsTagsPivot>(entity =>
        {
            entity.HasKey(e => e.FilmTagId);

            entity.ToTable("Films_Tags_Pivot");

            entity.Property(e => e.FilmTagId)
                .ValueGeneratedNever()
                .HasColumnName("Film_Tag_id");
            entity.Property(e => e.FilmTagFilmId).HasColumnName("Film_Tag_Film_id");
            entity.Property(e => e.FilmTagTagId).HasColumnName("Film_Tag_Tag_id");

            entity.HasOne(d => d.FilmTagFilm).WithMany(p => p.FilmsTagsPivots)
                .HasForeignKey(d => d.FilmTagFilmId)
                .HasConstraintName("FK_Films_Tags_Pivot_Films_Film_Tag_id");

            entity.HasOne(d => d.FilmTagTag).WithMany(p => p.FilmsTagsPivots)
                .HasForeignKey(d => d.FilmTagTagId)
                .HasConstraintName("FK_Films_Tags_Pivot_Tags_Film_Tag_id");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.LogId).HasColumnName("Log_id");
            entity.Property(e => e.LogDateError).HasColumnName("Log_dateError");
            entity.Property(e => e.LogErrorMessage).HasColumnName("Log_errorMessage");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.TagId).HasColumnName("Tag_id");
            entity.Property(e => e.TagDescription).HasColumnName("Tag_description");
            entity.Property(e => e.TagName).HasColumnName("Tag_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
