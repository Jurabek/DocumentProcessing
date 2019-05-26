using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DocumentProcessing.Abstraction;
using Microsoft.AspNetCore.Http;

namespace DocumentProcessing.ViewModels
{
    public class DocumentViewModel : IDocumentModel
    {
        public Guid Id { get; set; }
        
        public string RecipientId { get; set; }
        
        public ApplicantType ApplicantType { get; set; }
        
        [DisplayName("Макони вуруди ҳуҷҷат")]
        [Required]
        public Guid OwnerId { get; set; }
        
        [DisplayName("Номи ташкилот")]
        public string ApplicantName { get; set; }
        
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