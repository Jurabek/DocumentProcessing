using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DocumentProcessing.ViewModels.Roles
{
    public class EditRoleViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<IdentityRole> UserRoles { get; set; }
        
        public IEnumerable<IdentityRoleViewModel> Roles { get; set; }
    }
}
