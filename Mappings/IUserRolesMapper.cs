using System.Threading.Tasks;
using DocumentProcessing.ViewModels.Roles;

namespace DocumentProcessing.Mappings
{
    public interface IUserRolesMapper
    {
        Task<EditRoleViewModel> GetEditRoleViewModel(string userId);
    }
}