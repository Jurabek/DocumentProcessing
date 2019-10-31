using System;

namespace DocumentProcessing.Abstraction
{
    public interface IDocumentModel
    {
        Guid? ApplicantId { get; set; }

        Guid? StatusId { get; set; }
        Guid? VisaTypeId { get; set; }

        Guid? RegistrationId { get; set; }

        Guid? VisaDateTypeId { get; set; }
        Guid? PurposeId { get; set; }

        Guid OwnerId { get; set; }
    }
}