using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace QuizGamePerssistence.Models;

public partial class QuizGameContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public QuizGameContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SqlConnection");
    }

    public QuizGameContext(DbContextOptions<QuizGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<FillBlanckQuestion> FillBlanckQuestions { get; set; }

    public virtual DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategorieId).HasName("PK__Categori__F643AD86E7AC700E");

            entity.Property(e => e.CategorieId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CategorieID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Choice>(entity =>
        {
            entity.HasKey(e => e.ChoiceId).HasName("PK__Choices__76F51686AE218F37");

            entity.Property(e => e.ChoiceId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ChoiceID");
            entity.Property(e => e.MultipleChoiceId).HasColumnName("MultipleChoiceID");
            entity.Property(e => e.Text)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MultipleChoice).WithMany(p => p.Choices)
                .HasForeignKey(d => d.MultipleChoiceId)
                .HasConstraintName("FK__Choices__Multipl__619B8048");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.CollectionId).HasName("PK__Collecti__7DE6BC246677DEB3");

            entity.Property(e => e.CollectionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CollectionID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FillBlanckQuestion>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany()
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__FillBlanc__Quest__46E78A0C");
        });

        modelBuilder.Entity<MultipleChoiceQuestion>(entity =>
        {
            entity.HasKey(e => e.MultipleChoiceId).HasName("PK__Multiple__380BB21C4ED3A4E4");

            entity.Property(e => e.MultipleChoiceId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("MultipleChoiceID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.MultipleChoiceQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__MultipleC__Quest__5CD6CB2B");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C870E0384");

            entity.Property(e => e.QuestionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("QuestionID");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.QuizId).HasColumnName("QuizID");
            entity.Property(e => e.Text)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK__Questions__QuizI__44FF419A");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quizzes__8B42AE6E7CD6E11A");

            entity.Property(e => e.QuizId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("QuizID");
            entity.Property(e => e.CollectionId).HasColumnName("CollectionID");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Collection).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CollectionId)
                .HasConstraintName("FK__Quizzes__Collect__3A81B327");

            entity.HasMany(d => d.Categories).WithMany(p => p.Quizzes)
                .UsingEntity<Dictionary<string, object>>(
                    "QuizCategorie",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__QuizCateg__Categ__412EB0B6"),
                    l => l.HasOne<Quiz>().WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__QuizCateg__QuizI__403A8C7D"),
                    j =>
                    {
                        j.HasKey("QuizId", "CategorieId").HasName("PK__QuizCate__F42694B6649041BD");
                        j.ToTable("QuizCategorie");
                        j.IndexerProperty<Guid>("QuizId").HasColumnName("QuizID");
                        j.IndexerProperty<Guid>("CategorieId").HasColumnName("CategorieID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
