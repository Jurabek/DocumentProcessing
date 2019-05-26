using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentProcessing.Mappings;
using DocumentProcessing.Models;
using DocumentProcessing.ViewModels;
using DocumentProcessing.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocumentProcessing.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRolesMapper _userRolesMapper;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(
            IUserRolesMapper userRolesMapper,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _userRolesMapper = userRolesMapper;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();
            IEnumerable<UsersViewModel> model = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UsersViewModel>>(users);
            return View(model);
        }

        public async Task<ActionResult> EditRole(string id = null, UsersMessageId? message = null)
        {
            ViewBag.StatusMessage =
                message == UsersMessageId.RoleAddedSuccess ? "Roles has been changed." : "";

            var model = await _userRolesMapper.GetEditRoleViewModel(id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(EditRoleViewModel model)
        {
            var result = await UpdateUserRoles(model);
            if (result.Succeeded)
            {
                return RedirectToAction("EditRole", new { id = model.UserId, message = UsersMessageId.RoleAddedSuccess });
            }
            AddErrors(result);
            return View(model);
        }
        

        public enum UsersMessageId
        {
            RoleAddedSuccess,
            Error
        }
        
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        
        public async Task<IdentityResult> UpdateUserRoles(EditRoleViewModel model)
        {
            try
            {
                var selectedRoles = model.Roles.Where(ir => ir.IsSelected);
                var unSelectedRoles = model.Roles.Where(ir => !ir.IsSelected);

                var user = await _userManager.FindByIdAsync(model.UserId);
                var userRolesString = await _userManager.GetRolesAsync(user);

                var userRoles = userRolesString.Select(async x => await _roleManager.FindByNameAsync(x))
                    .Select(t => t.Result)
                    .Where(i => i != null)
                    .ToList();

                var rolesToAdd = selectedRoles.Where(r => !userRoles.Select(i => i.Id).Contains(r.Id));
                var rolesToRemove = unSelectedRoles.Where(r => userRoles.Select(ur => ur.Id).Contains(r.Id));

                if (rolesToAdd.Any())
                    await _userManager.AddToRolesAsync(user, rolesToAdd.Select(ir => ir.Name).ToArray());

                if (rolesToRemove.Any())
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove.Select(ir => ir.Name).ToArray());

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = ex.Message
                    
                });
            }
        }
    }
}