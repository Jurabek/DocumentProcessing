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
        public string Appointment { get; set; }
        
        [DisplayName("Ҳолати ҳуҷҷат")]
        public string Status { get; set; }

        [DisplayName("Қабулкунанда")]
        public string Recipient { get; set; }
        
        [DisplayName("Нусхаи ҳуҷҷат")]
        public List<ScannedFileViewModel> ScannedDocuments { get; set; }

        [DisplayName("ID-и дархост")]
        public string VisaId { get; set; }

        [DisplayName("Эзоҳ")]
        public string Description { get; set; }

        [DisplayName("то")]
        public string VisaDate { get; set; }

        [DisplayName("Навъи раводид")]
        public string VisaType { get; set; }

        [DisplayName("Мӯҳлати раводид")]
        public string VisaDateType { get; set; }

        [DisplayName("ID - дархост")]
        public string RequestId { get; set; }
    }
}