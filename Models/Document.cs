using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DocumentProcessing.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace DocumentProcessing.Models
{
    public class Document : IDocumentModel
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        
        public long EntryNumber { get; set; }

        public string AppointmentNumber { get; set; }
        
        public Guid? ApplicantId { get; set; }
        
        public Guid? StatusId { get; set; }

        public Guid? VisaTypeId { get; set; }

        public Guid? VisaDateTypeId { get; set; }

        public Guid? PurposeId { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public string RecipientId { get; set; }

        public string Description { set; get; }

        public string VisaDate { get; set; }

        public virtual ApplicationUser Recipient { get; set; }
        
        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        
        [ForeignKey("OwnerId")]
        public virtual DocumentOwner Owner { get; set; }
        
        [ForeignKey("PurposeId")]
        public virtual Purpose Purpose { get; set; }
        
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        [ForeignKey("VisaTypeId")]
        public virtual VisaType VisaType { get; set; }

        [ForeignKey("VisaDateTypeId")]
        public virtual VisaDateType VisaDateType { get; set; }

        public Appointment Appointment { get; set; }

        public virtual IEnumerable<ScannedFile> ScannedFiles { get; set; }

        public virtual IEnumerable<RequestId> RequestId { get; set; }

        public string VisaId { get; set; }

    }
}