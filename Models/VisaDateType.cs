using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.Models
{
    public class VisaDateType
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}





