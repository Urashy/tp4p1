using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace tp4p1.Models.EntityFramework
{
    [Table("t_e_film_flm")]
    [Index(nameof(Titre), Name = "flm_titre", IsUnique = false)]
    public class Film
    {
        public Film() 
        {
            NotesFilms = new HashSet<Notation>();
        }



        [Key]
        [Column("flm_id")]
        public int FilmId { get; set; }  // PK, auto-incrémenté

        [Column("flm_titre", TypeName = "varchar(100)")]
        public string? Titre { get; set; }  

        [Column("flm_resume", TypeName = "text")]
        public string? Resume { get; set; }  

        [Column("flm_datesortie", TypeName = "date")]
        public DateTime DateSortie { get; set; }

        [Column("flm_duree", TypeName = "numeric(3,0)")]
        public decimal Duree { get; set; }

        [Column("flm_genre", TypeName = "varchar(30)")]
        public string Genre { get; set; }

        [InverseProperty(nameof(Notation.FilmNote))]
        public ICollection<Notation> NotesFilms { get; set; }
    }
}
