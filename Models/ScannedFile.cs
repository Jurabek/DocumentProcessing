using System;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.Models
{
    public class ScannedFile
    {
        [Key]
        public Guid Id { get; set; }

        public string OriginalFileName { get; set; }

        public string UniqFileName { get; set; }

        public byte[] File { get; set; }

        public Guid DocumentId { get; set; }

        public virtual Document Document { get; set; }
    }
}