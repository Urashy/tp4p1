using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace tp4p1.Models.EntityFramework
{
    [Table("t_e_utilisateur_utl")]
    [Index(nameof(Mail),Name ="utl_mail", IsUnique =true)]
    public class Utilisateur
    {
        public Utilisateur()
        {
            NotesUtilisateur = new HashSet<Notation>();
        }


        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }  // PK, auto-incrémenté

        [Column("utl_nom", TypeName = "varchar(50)")]
        public string? Nom { get; set; }  // varchar(50), not null

        [Key]
        [Column("utl_prenom", TypeName = "varchar(50)")]
        public string? Prenom { get; set; }  // varchar(50), not null

        [Column("utl_mobile", TypeName = "char(10)")]
        public string? Mobile { get; set; }

        [Column("utl_mail", TypeName = "varchar(100)")]
        public string? Mail { get; set; }

        [Column("utl_pwd", TypeName = "varchar(64)")]
        public string? Pwd { get; set; }

        [Column("utl_rue", TypeName = "varchar(200)")]
        public string? Rue { get; set; }

        [Column("utl_cp", TypeName = "char(5)")]
        public string? CodePostal { get; set; }

        [Column("utl_ville", TypeName = "varchar(50)")]
        public string? Ville { get; set; }

        [Column("utl_pays", TypeName = "varchar(50)")]
        public string? Pays { get; set; } = "France";

        [Column("utl_latitude", TypeName = "real")]
        public float Latitude { get; set; }

        [Column("utl_longitude", TypeName = "real")]
        public float Longitude { get; set; }

        [Column("utl_datecreation", TypeName = "date not null")]
        public DateTime DateCreation { get; set; }

        
        [InverseProperty(nameof(Notation.UtilisateurNotant))]
        public ICollection<Notation> NotesUtilisateur{ get; set; }
    }
}
