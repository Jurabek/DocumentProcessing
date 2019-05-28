using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentProcessing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DocumentProcessing.Data
{
    public class ApplicationDataSeed
    {
        public async Task Seed(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ApplicationDataSeed> logger,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            var policy = WebHostExtensions.CreatePolicy(logger, nameof(ApplicationDataSeed));

            await policy.ExecuteAsync(async () =>
            {
                var purposesList = new List<string>
                {
                    "Тамдиди раводид",
                    "Апостилгузори",
                    "Таъйид",
                    "Хуручи",
                    "Бакайдгирӣ",
                    "eVisa",
                    "Тасдики даъват"
                };

                var ownersList = new List<string>
                {
                    "СРК",
                    "ВКХ ЧТ"
                };

                var statusList = new List<string>
                {
                    "Роҳбарият",
                    "№126",
                    "КДАМ",
                    "Барои иҷро"
                };

                var applicantsList = new List<string>
                {
                    "ҶДММ Ташкилоти А",
                    "ҶДММ Ташкилоти Б",
                    "ҶДММ Ташкилоти С",
                    "ҶДММ Ташкилоти Д"
                };

                if (!await context.Purposes.AnyAsync())
                {
                    var purposes = purposesList.Select(p => new Purpose {Name = p});
                    await context.AddRangeAsync(purposes);
                }

                if (!await context.DocumentOwners.AnyAsync())
                {
                    var owners = ownersList.Select(x => new DocumentOwner {Name = x});
                    await context.AddRangeAsync(owners);
                }

                if (!await context.Statuses.AnyAsync())
                {
                    var statuses = statusList.Select(x => new Status {Name = x});
                    await context.AddRangeAsync(statuses);
                }

                if (!await context.Applicants.AnyAsync())
                {
                    var applicants = applicantsList.Select(x => new Applicant {Name = x});
                    await context.AddRangeAsync(applicants);
                }
                
                var _ = context.ChangeTracker.HasChanges() ? await context.SaveChangesAsync() : 0;

                var roleNames = configuration.GetSection("UserSettings:Roles").Get<List<string>>();
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                var usersSection = configuration.GetSection("UserSettings:Users");
                foreach (var section in usersSection.GetChildren())
                {
                    var userEmail = section.GetValue<string>("Email");
                    var existingUser = await userManager.FindByEmailAsync(userEmail);

                    if (existingUser == null)
                    {
                        var userName = section.GetValue<string>("UserName");
                        var name = section.GetValue<string>("Name");
                        var type = section.GetValue<string>("Type");
                        var user = new ApplicationUser()
                        {
                            UserName = userName,
                            Email = userEmail,
                            Name = name
                        };

                        var password = section.GetValue<string>("Password");
                        var createPowerUser = await userManager.CreateAsync(user, password);

                        if (createPowerUser.Succeeded)
                        {
                            if (type == "admin")
                            {
                                await userManager.AddToRoleAsync(user, "Admin");
                            }

                            if (type == "manager")
                            {
                                await userManager.AddToRoleAsync(user, "Manager");
                            }

                            if (type == "operator")
                            {
                                await userManager.AddToRoleAsync(user, "Operator");
                            }
                        }
                        else
                        {
                            logger.LogError(createPowerUser?.Errors?.FirstOrDefault()?.Description);
                        }
                    }
                }
            });
        }
    }
}