using System;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.ViewModels
{
    public class ScannedFileViewModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedDate { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}