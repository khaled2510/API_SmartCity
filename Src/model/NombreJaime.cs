using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model
{
    public partial class NombreJaime
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UtilisateurId { get; set; }
        [Required]
        public int EvenementId { get; set; }

        public virtual Evenement Evenement { get; set; }
    }
}
