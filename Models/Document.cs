using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DocumentProcessing.Models
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        
        public long EntryNumber { get; set; }
        
        public string AppointmentNumber { get; set; }
        
        public Guid ApplicantId { get; set; }
        
        public Guid? StatusId { get; set; }
        
        public Guid? PurposeId { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public string RecipientId { get; set; }
        
        public virtual ApplicationUser Recipient { get; set; }
        
        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        
        [ForeignKey("OwnerId")]
        public virtual DocumentOwner Owner { get; set; }
        
        [ForeignKey("PurposeId")]
        public virtual Purpose Purpose { get; set; }
        
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
        
        public virtual ICollection<ScannedFile> ScannedFiles { get; set; }
    }
}