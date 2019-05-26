using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.Models
{
    public class Purpose
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}