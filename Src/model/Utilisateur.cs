using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Commentaire = new HashSet<Commentaire>();
            Evenement = new HashSet<Evenement>();
            Participation = new HashSet<Participation>();
        }

        [Required]
        [StringLength(50)]
        public string Pseudo { get; set; }
        [Required]
        [StringLength(50)]
        public string Nom { get; set; }
        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string MotDePasse { get; set; }
        [Required]
        [StringLength(20)]
        public string Role { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Commentaire> Commentaire { get; set; }
        public virtual ICollection<Evenement> Evenement { get; set; }
        public virtual ICollection<Participation> Participation { get; set; }
    }
}
