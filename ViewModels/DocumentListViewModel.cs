using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.ViewModels
{
    public class DocumentListViewModel
    {
        [DisplayName("№")]
        public Guid Id { get; set; }
        
        [DisplayName("Таърих")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { get; set; }
        
        [DisplayName("Номи ташкилот")]
        public string Applicant { get; set; }
        
        [DisplayName("№ воридотӣ")]
        public string EntryNumber { get; set; }
        
        [DisplayName("Мақсади муроҷиат ")]
        public string Purpose { get; set; }
        
        [DisplayName("№ талон")]
        public AppointmentViewModel Appointment { get; set; }
        
        [DisplayName("Ҳолати ҳуҷҷат")]
        public string Status { get; set; }

        [DisplayName("Қабулкунанда")]
        public string Recipient { get; set; }
        
        [DisplayName("Нусхаи ҳуҷҷат")]
        public List<ScannedFileViewModel> ScannedDocuments { get; set; }
    }
}