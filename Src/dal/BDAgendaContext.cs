using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using model;

namespace dal
{
    public partial class BDAgendaContext : DbContext
    {
        public BDAgendaContext()
        {
        }

        public BDAgendaContext(DbContextOptions<BDAgendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Commentaire> Commentaire { get; set; }
        public virtual DbSet<Evenement> Evenement { get; set; }
        public virtual DbSet<NombreJaime> NombreJaime { get; set; }
        public virtual DbSet<Participation> Participation { get; set; }
        public virtual DbSet<Presentation> Presentation { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("connexionstring.json")
                                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();
            });

            modelBuilder.Entity<Commentaire>(entity =>
            {
                entity.Property(e => e.AuteurId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Signaler).HasDefaultValueSql("((0))");

                entity.Property(e => e.Texte)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Auteur)
                    .WithMany(p => p.Commentaire)
                    .HasForeignKey(d => d.AuteurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AuteurId_FK_Commentaire").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Evenement)
                    .WithMany(p => p.Commentaire)
                    .HasForeignKey(d => d.EvenementId)
                    .HasConstraintName("EvenementId_FK_Commentaire").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.Property(e => e.CreateurId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateCreationEvenement)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1500);

                entity.Property(e => e.ImageUrl).HasMaxLength(100);

                entity.Property(e => e.Localite)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Rue)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Categorie)
                    .WithMany(p => p.Evenement)
                    .HasForeignKey(d => d.CategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CategorieId_FK_Evenement");

                entity.HasOne(d => d.Createur)
                    .WithMany(p => p.Evenement)
                    .HasForeignKey(d => d.CreateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreateurId_FK_Evenement").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<NombreJaime>(entity =>
            {
                entity.Property(e => e.UtilisateurId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Evenement)
                    .WithMany(p => p.NombreJaime)
                    .HasForeignKey(d => d.EvenementId)
                    .HasConstraintName("EvenementId_FK_NombreJaime").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Participation>(entity =>
            {
                entity.Property(e => e.ParticipantId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.Evenement)
                    .WithMany(p => p.Participation)
                    .HasForeignKey(d => d.EvenementId)
                    .HasConstraintName("EvenementId_FK_Participation").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.Participation)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AuteurId_FK_Participation").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Presentation>(entity =>
            {
                entity.Property(e => e.DateHeureDebut).HasColumnType("datetime");

                entity.Property(e => e.DateHeureFin).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.Evenement)
                    .WithMany(p => p.Presentation)
                    .HasForeignKey(d => d.EvenementId)
                    .HasConstraintName("EvenementId_FK_Presentation").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.Pseudo)
                    .HasName("PseudoUtilisateurPK");

                entity.Property(e => e.Pseudo)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMail")
                    .HasMaxLength(100);

                entity.Property(e => e.MotDePasse)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();
            });
        }
    }
}
