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

        //
        public DateTime DeadLineDate { get; set; }

        public Guid? DocTypeId { get; set; }

        public long OutgoingNumber { get; set; }

        public long OutDocDate { get; set; }

        public Guid? DirectionId { get; set; }

        public string SeenById { get; set; }

        public string Note { get; set; }

        public string Additional { get; set; }

        public long Count { get; set; }

        public string SignById { get; set; }

        public bool Control { get; set; }

        public Guid? DepartmentId { get; set; }

        public string AddedUserId { get; set; }

        public string EditedUserId { get; set; }



        public virtual ApplicationUser SeenBy { get; set; }

        public virtual ApplicationUser SignBy { get; set; }

        public virtual ApplicationUser AddedUser { get; set; }

        public virtual ApplicationUser EditedUser { get; set; }

        [ForeignKey("DocTypeId")]
        public virtual DocType DocTypes { get; set; }

        [ForeignKey("DirectionId")]
        public virtual Direction Direction { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }



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