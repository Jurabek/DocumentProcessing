using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DocumentProcessing.Models;

namespace DocumentProcessing.ViewModels
{
    public class AppointmentViewModel
    {
        [DisplayName("Ҳарфи талон")]
        [EnumDataType(typeof(AppointmentCharacters))]
        [Required(ErrorMessage = "Ҳарфи талон холи аст!")]
        public AppointmentCharacters Character { get; set; }

        [DisplayName("Рақами талон")]
        [Required(ErrorMessage = "Рақами талон холи аст!")]
        public string Number { get; set; }

        public Guid? DocumentId { get; set; }
        
        public Guid? Id { get; set; }

        public string Title { get => Character + Number;}
    }
}