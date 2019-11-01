using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentProcessing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

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
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AppointmentCharacters>();

            await policy.ExecuteAsync(async () =>
            {
                foreach (var document in context.Documents.Include(x => x.Appointment)
                    .Where(x => x.Appointment == null).ToList())
                {
                    var appointmentNumber = document.AppointmentNumber;
                    if (!string.IsNullOrEmpty(appointmentNumber))
                    {
                        var characterOfAppointmentNumber = appointmentNumber[0];
                        if (Char.IsLetter(characterOfAppointmentNumber))
                        {
                            var numberOfAppointmentNumber = appointmentNumber.Substring(1);

                            Enum.TryParse(characterOfAppointmentNumber.ToString(), out AppointmentCharacters character);
                            int.TryParse(numberOfAppointmentNumber, out var number);

                            var appointment = new Appointment
                            {
                                Number = number,
                                Character = character
                            };

                            document.Appointment = appointment;
                            context.Update(document);
                            context.SaveChanges();
                        }
                    }
                }

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

                var  visadatetypeList = new List<string>
                {
                    "Рӯз",
                    "Моҳ"
                };

                var visatypeList = new List<string>
                {
                "Раводиди дипломатии навъи «Д»",
                "Раводиди хизматии навъи «Х»",
                "Раводиди сармоягузории навъи «С»",
                "Раводиди кории навъи «К»",
                "Раводиди меҳнатии навъи «М»",
                "Раводиди сайёҳии навъи «Т»",
                "Раводиди таҳсилотии навъҳои «О1» ва «О2»",
                "Раводиди хусусии навъҳои «ХС1» ва «ХС2»",
                "Раводиди минтақаи озоди иқтисодии навъи «МОИ»",
                "Раводиди нақлиётии навъи «Н»",
                "Раводид барои истиқомати доимии навъи «ИД»",
                "Раводиди воситаи ахбори оммаи навъи «Ж»",
                "Раводиди табширии навъи «ТБ»",
                "Раводиди башардӯстонаи навъи «Б»",
                "Раводиди транзитии навъи «ТР»",
                "Раводиди хуруҷии навъи «Хуруҷӣ»"
                };

                var registrationList = new List<string>
                {
                    "Нест",
                    "2017",
                    "2018",
                    "2019",
                    "2020"
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

                if (!await context.VisaType.AnyAsync())
                {
                    var visatype = visatypeList.Select(v => new VisaType { Name = v });
                    await context.AddRangeAsync(visatype);
                }

                if (!await context.Registration.AnyAsync())
                {
                    var registration = registrationList.Select(r => new Registration { Name = r });
                    await context.AddRangeAsync(registration);
                }

                if (!await context.VisaDateType.AnyAsync())
                {
                    var visadatetype = visadatetypeList.Select(v => new VisaDateType { Name = v });
                    await context.AddRangeAsync(visadatetype);
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