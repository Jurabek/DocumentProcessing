using System;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DocumentProcessing.Models;

namespace DocumentProcessing.ViewModels
{
    public class VisaIdViewModel
    {
        public Guid? Id { get; set; }

        [DisplayName("ID - и дархост")]
        //[Required(ErrorMessage = "ID - и дархост холи аст!")]
        public string ID { get; set; }

        public Guid? DocumentId { get; set; }

       
    }
}
