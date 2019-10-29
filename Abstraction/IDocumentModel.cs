using System;

namespace DocumentProcessing.Abstraction
{
    public interface IDocumentModel
    {
        Guid? ApplicantId { get; set; }

        Guid? StatusId { get; set; }
        Guid? VisaTypeId { get; set; }

        Guid? VisaDateTypeId { get; set; }
        Guid? PurposeId { get; set; }

        Guid OwnerId { get; set; }

        //

        Guid? DocTypeId { get; set; }

        Guid? DirectionId { get; set; }

        Guid? DepartmentId { get; set; }
    }
}