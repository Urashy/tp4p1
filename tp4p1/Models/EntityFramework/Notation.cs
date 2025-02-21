using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp4p1.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public class Notation
    {
        [Key, Column("utl_id")]
        public int UtilisateurId { get; set; } // PK, FK vers Utilisateur

        [Key, Column("flm_id")]
        public int FilmId { get; set; } // PK, FK vers Film

        [Required]
        [Column("not_note")]
        [Range(0, 5, ErrorMessage = "La note doit être comprise entre 0 et 5.")]
        public int Note { get; set; } // Int, not null, entre 0 et 5

        // Navigation vers Utilisateur et Film (relations correctes)
        [ForeignKey("UtilisateurId")]
        public virtual Utilisateur UtilisateurNotant { get; set; }

        [ForeignKey("FilmId")]
        public virtual Film FilmNote { get; set; }
    }
}
