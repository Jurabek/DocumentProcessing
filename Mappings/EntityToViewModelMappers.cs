using System;
using AutoMapper;
using DocumentProcessing.Models;
using DocumentProcessing.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DocumentProcessing.Mappings
{
    public class EntityToViewModelMappers : Profile
    {
        public EntityToViewModelMappers()
        {
            CreateMap<ApplicationUser, UsersViewModel>();
            CreateMap<DocumentViewModel, Document>()
                .ForMember(x => x.ScannedFiles, opt => opt.Ignore());

            CreateMap<Document, DocumentViewModel>();
            CreateMap<ScannedFile, ScannedFileViewModel>();

            CreateMap<Document, DocumentListViewModel>()
                .ForMember(x => x.Date,
                    map => map.MapFrom(x => x.Date))
                .ForMember(x => x.Applicant,
                    map => map.MapFrom(x => x.Applicant.Name))
                .ForMember(x => x.Recipient,
                    map => map.MapFrom(x => x.Recipient.LastName + " " + x.Recipient.Name))
                .ForMember(x => x.Appointment,
                    map => map.MapFrom(x => x.Appointment.Character + "" + x.Appointment.Number))
                    .ForMember(x => x.Purpose,
                    map => map.MapFrom(x => x.Purpose.Name))
                .ForMember(x => x.VisaType,
                    map => map.MapFrom(x => x.VisaType.Name))
                .ForMember(x => x.VisaDateType,
                    map => map.MapFrom(x => x.VisaDateType.Name))
                .ForMember(x => x.Status,
                    map => map.MapFrom(x => x.Status.Name))
                .ForMember(x => x.ScannedDocuments,
                    map => map.MapFrom(x => x.ScannedFiles))
                 .ForMember(x => x.Registration,
                    map => map.MapFrom(x => x.Registration.Name));


            CreateMap<Appointment, AppointmentViewModel>();
            CreateMap<AppointmentViewModel, Appointment>();
        }
    }
}