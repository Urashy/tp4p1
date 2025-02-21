using Microsoft.EntityFrameworkCore;
using tp4p1.Models.EntityFramework;

namespace tp4p1.Models.EntityFramework
{
    public class FilmRatingsDBContext : DbContext
    {
        public FilmRatingsDBContext(DbContextOptions<FilmRatingsDBContext> options)
            : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Notation> Notations { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
builder.AddConsole());
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLoggerFactory(MyLoggerFactory)
        .EnableSensitiveDataLogging()
        .UseNpgsql("Server=localhost;port=5432;Database=dbtp4; uid=postgres; password=postgres;")
        //.UseLazyLoadingProxies()
        ;
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            // Film
            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("t_e_film_flm");
                entity.HasKey(e => e.FilmId).HasName("pk_film");

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Resume)
                    .HasColumnType("text");

                entity.Property(e => e.DateSortie)
                    .HasColumnType("date");

                entity.Property(e => e.Duree)
                    .HasColumnType("numeric(3,0)");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnType("varchar(30)");
            });

            // Utilisateur
            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.ToTable("t_e_utilisateur_utl");
                entity.HasKey(e => e.UtilisateurId).HasName("pk_utilisateur");

                entity.Property(e => e.Nom)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Prenom)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Mobile)
                    .HasColumnType("char(10)");

                entity.Property(e => e.Mail)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Pwd)
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.Rue)
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.CodePostal)
                    .HasColumnType("char(5)");

                entity.Property(e => e.Ville)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Pays)
                    .HasColumnType("varchar(50)").HasDefaultValue("France");

                entity.Property(e => e.Latitude)
                    .HasColumnType("real");

                entity.Property(e => e.Longitude)
                    .HasColumnType("real");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()");
            });

            // Notation (Relation entre Film et Utilisateur)
            modelBuilder.Entity<Notation>(entity =>
            {
                entity.ToTable("t_j_notation_not");
                entity.HasKey(e => new { e.UtilisateurId, e.FilmId }).HasName("pk_notation");

                entity.Property(e => e.Note)
                    .HasColumnType("int")
                    .HasDefaultValue(0);

                // Relation avec Utilisateur
                entity.HasOne(n => n.UtilisateurNotant)
                    .WithMany(u => u.NotesUtilisateur)
                    .HasForeignKey(n => n.UtilisateurId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_notation_utilisateur");

                // Relation avec Film
                entity.HasOne(n => n.FilmNote)
                    .WithMany(f => f.NotesFilms)
                    .HasForeignKey(n => n.FilmId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_notation_film");
            });
        }
    }
}

//dotnet ef database drop --project tp4p1 --force
//dotnet ef migrations remove --project tp4p1
//dotnet ef migrations add InitialMigration --project tp4p1
//dotnet ef database update --project tp4p1