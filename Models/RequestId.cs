using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentProcessing.Models
{
    public class RequestId
    {
        [Key]
        public Guid Id { get; set; }

        public string Number { get; set; }

        public Guid DocumentId { get; set; }

        public virtual Document Document { get; set; }
   
    }
}
