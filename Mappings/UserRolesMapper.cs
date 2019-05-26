using System.Linq;
using System.Threading.Tasks;
using DocumentProcessing.Models;
using DocumentProcessing.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;

namespace DocumentProcessing.Mappings
{
    public class UserRolesMapper : IUserRolesMapper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesMapper(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<EditRoleViewModel> GetEditRoleViewModel(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRolesString = await _userManager.GetRolesAsync(user);

            var userRoles = userRolesString.Select(async x => await _roleManager.FindByNameAsync(x))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            var model = new EditRoleViewModel
            {
                UserRoles = userRoles,
                UserId = user.Id,
                Roles = _roleManager.Roles.ToList().Select(r => new IdentityRoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = userRoles.Select(ur => ur.Id).Contains(r.Id)
                })
            };


            return model;
        }
    }
}