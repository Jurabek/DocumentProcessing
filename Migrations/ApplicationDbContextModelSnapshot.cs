﻿// <auto-generated />
using System;
using DocumentProcessing.Data;
using DocumentProcessing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DocumentProcessing.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:Enum:appointment_characters", "a,b,c,d,e,f,g")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.EntryNumbers", "'EntryNumbers', '', '2000', '1', '', '', 'Int64', 'False'");

            modelBuilder.Entity("DocumentProcessing.Models.Applicant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("DocumentProcessing.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DocumentProcessing.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<AppointmentCharacters>("Character");

                    b.Property<Guid>("DocumentId");

                    b.Property<long>("Number");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique();

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("DocumentProcessing.Models.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicantId");

                    b.Property<string>("AppointmentNumber");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<long>("EntryNumber")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("nextval('\"EntryNumbers\"')");

                    b.Property<Guid>("OwnerId");

                    b.Property<Guid?>("PurposeId");

                    b.Property<string>("RecipientId");

                    b.Property<Guid?>("StatusId");

                    b.Property<string>("VisaDate");

                    b.Property<Guid?>("VisaDateTypeId");

                    b.Property<string>("VisaId");

                    b.Property<Guid?>("VisaTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PurposeId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("StatusId");

                    b.HasIndex("VisaDateTypeId");

                    b.HasIndex("VisaTypeId");

                    b.HasIndex("EntryNumber", "AppointmentNumber");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("DocumentProcessing.Models.DocumentOwner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("DocumentOwners");
                });

            modelBuilder.Entity("DocumentProcessing.Models.Purpose", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Character");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Purposes");
                });

            modelBuilder.Entity("DocumentProcessing.Models.RequestId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DocumentId");

                    b.Property<string>("Number");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("RequestId");
                });

            modelBuilder.Entity("DocumentProcessing.Models.ScannedFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("DocumentId");

                    b.Property<byte[]>("File");

                    b.Property<string>("FileName");

                    b.Property<long>("Length");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("ScannedFiles");
                });

            modelBuilder.Entity("DocumentProcessing.Models.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("DocumentProcessing.Models.VisaDateType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("VisaDateType");
                });

            modelBuilder.Entity("DocumentProcessing.Models.VisaType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("VisaType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DocumentProcessing.Models.Appointment", b =>
                {
                    b.HasOne("DocumentProcessing.Models.Document", "Document")
                        .WithOne("Appointment")
                        .HasForeignKey("DocumentProcessing.Models.Appointment", "DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DocumentProcessing.Models.Document", b =>
                {
                    b.HasOne("DocumentProcessing.Models.Applicant", "Applicant")
                        .WithMany("Documents")
                        .HasForeignKey("ApplicantId");

                    b.HasOne("DocumentProcessing.Models.DocumentOwner", "Owner")
                        .WithMany("Documents")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DocumentProcessing.Models.Purpose", "Purpose")
                        .WithMany("Documents")
                        .HasForeignKey("PurposeId");

                    b.HasOne("DocumentProcessing.Models.ApplicationUser", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");

                    b.HasOne("DocumentProcessing.Models.Status", "Status")
                        .WithMany("Documents")
                        .HasForeignKey("StatusId");

                    b.HasOne("DocumentProcessing.Models.VisaDateType", "VisaDateType")
                        .WithMany("Documents")
                        .HasForeignKey("VisaDateTypeId");

                    b.HasOne("DocumentProcessing.Models.VisaType", "VisaType")
                        .WithMany("Documents")
                        .HasForeignKey("VisaTypeId");
                });

            modelBuilder.Entity("DocumentProcessing.Models.RequestId", b =>
                {
                    b.HasOne("DocumentProcessing.Models.Document", "Document")
                        .WithMany("RequestId")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DocumentProcessing.Models.ScannedFile", b =>
                {
                    b.HasOne("DocumentProcessing.Models.Document", "Document")
                        .WithMany("ScannedFiles")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DocumentProcessing.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DocumentProcessing.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DocumentProcessing.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DocumentProcessing.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
