using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name= "Номи истифодабаранда")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name= "Рамзи махфи")]
        public string Password { get; set; }

        [Display(Name = "Сабт?")]
        public bool RememberMe { get; set; }
    }
}