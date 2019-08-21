using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dto
{
    public class Participation
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ParticipantId { get; set; }
        [Required]
        public int EvenementId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}