using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model
{
    public partial class Evenement
    {
        public Evenement()
        {
            Commentaire = new HashSet<Commentaire>();
            NombreJaime = new HashSet<NombreJaime>();
            Participation = new HashSet<Participation>();
            Presentation = new HashSet<Presentation>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nom { get; set; }
        [Required]
        [StringLength(1500)]
        public string Description { get; set; }
        [Required]
        [StringLength(200)]
        public string Rue { get; set; }
        [Required]
        [StringLength(10)]
        public string Numero { get; set; }
        [Required]
        public int CodePostal { get; set; }
        [Required]
        [StringLength(50)]
        public string Localite { get; set; }
        [Required]
        public DateTime DateCreationEvenement { get; set; }
        [Required]
        public int CategorieId { get; set; }
        [Required]
        public string CreateurId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public string ImageUrl { get; set; }

        public virtual Categorie Categorie { get; set; }
        public virtual Utilisateur Createur { get; set; }
        public virtual ICollection<Commentaire> Commentaire { get; set; }
        public virtual ICollection<NombreJaime> NombreJaime { get; set; }
        public virtual ICollection<Participation> Participation { get; set; }
        public virtual ICollection<Presentation> Presentation { get; set; }
    }
}
