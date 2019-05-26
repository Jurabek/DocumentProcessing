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
            CreateMap<DocumentViewModel, Document>();
            CreateMap<Document, DocumentViewModel>();

            CreateMap<Document, DocumentListViewModel>()
                .ForMember(x => x.Applicant,
                    map => map.MapFrom(x => x.Applicant.Name))
                .ForMember(x => x.Recipient,
                    map => map.MapFrom(x => x.Recipient.LastName))
                .ForMember(x => x.Purpose, 
                    map => map.MapFrom(x => x.Purpose.Name))
                .ForMember(x => x.Status, 
                    map => map.MapFrom(x => x.Status.Name));
  
        }
    }
}