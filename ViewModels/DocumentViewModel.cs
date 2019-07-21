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
        [Required(ErrorMessage = "Макони вуруди ҳуҷҷат холи аст!")]
        public Guid OwnerId { get; set; }
        
        [DisplayName("Номи ташкилот")]
        public string ApplicantName { get; set; }
        
        [DisplayName("Номи ташкилот")]
        public Guid? ApplicantId { get; set; }

        [DisplayName("Рақами воридотӣ")]
        [Required(ErrorMessage = "Рақами воридотӣ холи аст!")]
        public string EntryNumber { get; set; }

        [DisplayName("Мақсади муроҷиат")]
        [Required(ErrorMessage = "Мақсади муроҷиат холи аст!")]
        public Guid? PurposeId { get; set; }
        
        [DisplayName("Ҳолати ҳуҷҷат")]
        public Guid? StatusId { get; set; }

        public AppointmentViewModel Appointment { get; set; }

        public List<ScannedFileViewModel> ScannedFiles { get; set; } = new List<ScannedFileViewModel>();
    }
}