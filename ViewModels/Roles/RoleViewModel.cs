using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.ViewModels.Roles
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Role name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Title { get; set; }

    }
}
