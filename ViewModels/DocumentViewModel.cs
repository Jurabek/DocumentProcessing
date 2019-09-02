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

        public PurposeType PurposeType { get; set; }

        [DisplayName("Макони вуруди ҳуҷҷат")]
        [Required(ErrorMessage = "Макони вуруди ҳуҷҷат холи аст!")]
        public Guid OwnerId { get; set; }

        [DisplayName("Номи ташкилот")]
        public string ApplicantName { get; set; }

        [DisplayName("Мақсади муроҷиат")]
        public string PurposeName { get; set; }

        [DisplayName("Номи ташкилот")]
        public Guid? ApplicantId { get; set; }

        public string EntryNumber { get; set; }

        [DisplayName("Мақсади муроҷиат")]

        public Guid? PurposeId { get; set; }

        [DisplayName("Ҳолати ҳуҷҷат")]

        public Guid? StatusId { get; set; }



        [DisplayName("то")]
    
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Хадди нихоии раками ичозатшуда барои Рӯз то 30 ва барои Мох то 32")]
        public string VisaDate { get; set; }

        [DisplayName("Навъи раводид")]
       
        public Guid? VisaTypeId { get; set; }

        [DisplayName("Мӯҳлати раводид")]
        
        public Guid? VisaDateTypeId { get; set; }

        [DisplayName("Эзоҳ")]
        public string Description { get; set; }

        public AppointmentViewModel Appointment { get; set; }

        public RequestIdViewMModel RequestId { get; set; }

        [DisplayName("Иловаи ҳуҷҷати сканшуда")]
        [Required(ErrorMessage = "Иловаи ҳуҷҷати сканшуда холи аст!")]
        public List<ScannedFileViewModel> ScannedFiles { get; set; } = new List<ScannedFileViewModel>();

        [DisplayName("ID-и дархост")]
        
        public string VisaId { get; set; } 
    }
}