using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dto{
    public class Commentaire
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Texte { get; set; }
        public int? Signaler { get; set; }
        [Required]
        [StringLength(50)]
        public string AuteurId { get; set; }
        [Required]
        public int EvenementId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}