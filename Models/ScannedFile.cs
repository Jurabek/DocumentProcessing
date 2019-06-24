using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DocumentProcessing.Models
{
    public class ScannedFile
    {
        [Key]
        public Guid Id { get; set; }

        public string ContentType { get; set; }

        public long Length { get; set; }

        public string FileName { get; set; }

        public byte[] File { get; set; }

        public Guid DocumentId { get; set; }
        
        public virtual Document Document { get; set; }

        [NotMapped]
        public DateTime UpdatedDate { get; set; }
    }
}