﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Otus.Teaching.PromoCodeFactory.DataAccess;

#nullable disable

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AppliedPromocodesCount")
                        .HasColumnType("integer")
                        .HasColumnName("applied_promocodes_count");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_employees");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_employees_role_id");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("uuid")
                        .HasColumnName("preference_id");

                    b.HasKey("CustomerId", "PreferenceId")
                        .HasName("pk_customer_preference");

                    b.HasIndex("PreferenceId")
                        .HasDatabaseName("ix_customer_preference_preference_id");

                    b.ToTable("customer_preference", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("NumberIssuedPromoCodes")
                        .HasColumnType("integer")
                        .HasColumnName("number_issued_promo_codes");

                    b.HasKey("Id")
                        .HasName("pk_partners");

                    b.ToTable("partners", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PartnerPromoCodeLimit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("cancel_date");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("Limit")
                        .HasColumnType("integer")
                        .HasColumnName("limit");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("partner_id");

                    b.HasKey("Id")
                        .HasName("pk_partner_promo_code_limit");

                    b.HasIndex("PartnerId")
                        .HasDatabaseName("ix_partner_promo_code_limit_partner_id");

                    b.ToTable("partner_promo_code_limit", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_preferences");

                    b.ToTable("preferences", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("begin_date");

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<Guid?>("PartnerManagerId")
                        .HasColumnType("uuid")
                        .HasColumnName("partner_manager_id");

                    b.Property<string>("PartnerName")
                        .HasColumnType("text")
                        .HasColumnName("partner_name");

                    b.Property<Guid?>("PreferenceId")
                        .HasColumnType("uuid")
                        .HasColumnName("preference_id");

                    b.Property<string>("ServiceInfo")
                        .HasColumnType("text")
                        .HasColumnName("service_info");

                    b.HasKey("Id")
                        .HasName("pk_promo_codes");

                    b.HasIndex("PartnerManagerId")
                        .HasDatabaseName("ix_promo_codes_partner_manager_id");

                    b.HasIndex("PreferenceId")
                        .HasDatabaseName("ix_promo_codes_preference_id");

                    b.ToTable("promo_codes", (string)null);
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_employees_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", "Customer")
                        .WithMany("Preferences")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customer_preference_customers_customer_id");

                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customer_preference_preferences_preference_id");

                    b.Navigation("Customer");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PartnerPromoCodeLimit", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Partner", "Partner")
                        .WithMany("PartnerLimits")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_partner_promo_code_limit_partners_partner_id");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.Administration.Employee", "PartnerManager")
                        .WithMany()
                        .HasForeignKey("PartnerManagerId")
                        .HasConstraintName("fk_promo_codes_employees_partner_manager_id");

                    b.HasOne("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId")
                        .HasConstraintName("fk_promo_codes_preferences_preference_id");

                    b.Navigation("PartnerManager");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Navigation("Preferences");
                });

            modelBuilder.Entity("Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement.Partner", b =>
                {
                    b.Navigation("PartnerLimits");
                });
#pragma warning restore 612, 618
        }
    }
}
