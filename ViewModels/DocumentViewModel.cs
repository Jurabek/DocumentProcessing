using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.ViewModels
{
    public class DocumentViewModel
    {
        public string RecipientId { get; set; }
        
        public ApplicantType ApplicantType { get; set; }
        
        [DisplayName("Макони вуруди ҳуҷҷат")]
        [Required]
        public Guid OwnerId { get; set; }
        
        [DisplayName("Номи ташкилот")]
        public string Applicant { get; set; }
        
        [DisplayName("Номи ташкилот")]
        public Guid? ApplicantId { get; set; }

        [DisplayName("Рақами воридотӣ")]
        [Required]
        public string EntryNumber { get; set; }

        [DisplayName("Мақсади муроҷиат ")]
        public Guid? PurposeId { get; set; }

        [DisplayName("Рақами талон")]
        public string AppointmentNumber { get; set; }
        
        [DisplayName("Ҳолати ҳуҷҷат")]
        public Guid? StatusId { get; set; }
        
        
    }
}