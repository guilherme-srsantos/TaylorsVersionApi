using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaylorsVersionApi.Data.Entities;

namespace TaylorsVersionApi;

public partial class TaylorapiContext : DbContext
{
    public TaylorapiContext()
    {
    }

    public TaylorapiContext(DbContextOptions<TaylorapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Quote> Quotes { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("User Id=postgres.skvntunvyddlnjwjywxw;Password=Ozzy6495@Luci##;Server=aws-0-us-east-1.pooler.supabase.com;Port=5432;Database=taylorapi;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("albums_pkey");

            entity.ToTable("albums");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsTv).HasColumnName("istv");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Releaseyear).HasColumnName("releaseyear");
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("quotes_pkey");

            entity.ToTable("quotes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FieldType)
                .HasMaxLength(50)
                .HasColumnName("field_type");
            entity.Property(e => e.Quote1).HasColumnName("quote");
            entity.Property(e => e.Songid).HasColumnName("songid");

            entity.HasOne(d => d.Song).WithMany(p => p.Quotes)
                .HasForeignKey(d => d.Songid)
                .HasConstraintName("quotes_songid_fkey");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("songs_pkey");

            entity.ToTable("songs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Albumid).HasColumnName("albumid");
            entity.Property(e => e.Featuredartists)
                .HasMaxLength(255)
                .HasColumnName("featuredartists");
            entity.Property(e => e.Isvault).HasColumnName("isvault");
            entity.Property(e => e.Lyrics).HasColumnName("lyrics");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Tracknumber).HasColumnName("tracknumber");

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.Albumid)
                .HasConstraintName("songs_albumid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
