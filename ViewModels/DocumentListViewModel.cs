using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DocumentProcessing.ViewModels
{
    public class DocumentListViewModel
    {
        [DisplayName("№")]
        public Guid Id { get; set; }
        
        [DisplayName("Таърих")]
        public DateTime? Date { get; set; }
        
        [DisplayName("Номи ташкилот")]
        public string Applicant { get; set; }
        
        [DisplayName("№ воридотӣ")]
        public string EntryNumber { get; set; }
        
        [DisplayName("Мақсади муроҷиат ")]
        public string Purpose { get; set; }

        [DisplayName("№ талон")]
        public string AppointmentNumber { get; set; }
        
        [DisplayName("Ҳолати ҳуҷҷат")]
        public string Status { get; set; }

        [DisplayName("Қабулкунанда")]
        public string Recipient { get; set; }
        
        [DisplayName("Нусхаи ҳуҷҷат")]
        public List<ScannedFileViewModel> ScannedDocuments { get; set; }
    }
}