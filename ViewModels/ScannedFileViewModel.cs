using System;

namespace DocumentProcessing.ViewModels
{
    public class ScannedFileViewModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }
        

        public bool IsDeleted { get; set; }
    }
}