using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dto
{
    public class Categorie
    {
        public Categorie()
        {
            Evenement = new HashSet<Evenement>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Libelle { get; set; }
        public string Image { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Evenement> Evenement { get; set; }
    }
}