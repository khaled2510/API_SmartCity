using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model
{
    public partial class Participation
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ParticipantId { get; set; }
        [Required]
        public int EvenementId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Evenement Evenement { get; set; }
        public virtual Utilisateur Participant { get; set; }
    }
}
