using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model
{
    public partial class Presentation
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateHeureDebut { get; set; }
        [Required]
        public DateTime DateHeureFin { get; set; }
        [Required]
        public int EvenementId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Evenement Evenement { get; set; }
    }
}
