﻿// <auto-generated />
using System;
using GttApiWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GttApiWeb.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GttApiWeb.Models.Certificates", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("alias");

                    b.Property<DateTime>("caducidad");

                    b.Property<string>("contacto_renovacion");

                    b.Property<string>("entidad_emisiora");

                    b.Property<string>("id_orga");

                    b.Property<string>("integration_list");

                    b.Property<string>("nombre_cliente");

                    b.Property<string>("numero_de_serie");

                    b.Property<string>("observaciones");

                    b.Property<string>("password");

                    b.Property<string>("repositorio");

                    b.Property<string>("subject");

                    b.Property<long?>("user_idid");

                    b.HasKey("id");

                    b.HasIndex("user_idid");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("GttApiWeb.Models.Jira", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("componente");

                    b.Property<string>("password");

                    b.Property<string>("proyect");

                    b.Property<string>("url");

                    b.Property<long?>("user_idid");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.HasIndex("user_idid");

                    b.ToTable("Jira");
                });

            modelBuilder.Entity("GttApiWeb.Models.Users", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("email");

                    b.Property<string>("password");

                    b.Property<int>("rolUser");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GttApiWeb.Models.Certificates", b =>
                {
                    b.HasOne("GttApiWeb.Models.Users", "user_id")
                        .WithMany()
                        .HasForeignKey("user_idid");
                });

            modelBuilder.Entity("GttApiWeb.Models.Jira", b =>
                {
                    b.HasOne("GttApiWeb.Models.Users", "user_id")
                        .WithMany()
                        .HasForeignKey("user_idid");
                });
#pragma warning restore 612, 618
        }
    }
}
