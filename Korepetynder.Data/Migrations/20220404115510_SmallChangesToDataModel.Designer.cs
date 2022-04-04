﻿// <auto-generated />
using System;
using Korepetynder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    [DbContext(typeof(KorepetynderDbContext))]
    [Migration("20220404115510_SmallChangesToDataModel")]
    partial class SmallChangesToDataModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Korepetynder.Data.DbModels.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.MultimediaFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Owner1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Owner2Id")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Owner1Id");

                    b.HasIndex("Owner2Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PictureId")
                        .IsUnique()
                        .HasFilter("[PictureId] IS NOT NULL");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int?>("UserLocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("UserLocationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("StudentSubject");
                });

            modelBuilder.Entity("StudentTeacher", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("StudentTeacher");
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.Property<int>("TaughtSubjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("TaughtSubjectsId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("SubjectTeacher");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Location", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Location", null)
                        .WithMany("SubLocation")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.MultimediaFile", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Teacher", "Owner1")
                        .WithMany("Pictures")
                        .HasForeignKey("Owner1Id");

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", "Owner2")
                        .WithMany("Videos")
                        .HasForeignKey("Owner2Id");

                    b.HasOne("Korepetynder.Data.DbModels.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.Navigation("Owner1");

                    b.Navigation("Owner2");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.MultimediaFile", "ProfilePicture")
                        .WithOne("Owner")
                        .HasForeignKey("Korepetynder.Data.DbModels.Teacher", "PictureId");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.User", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.HasOne("Korepetynder.Data.DbModels.Location", "UserLocation")
                        .WithMany()
                        .HasForeignKey("UserLocationId");

                    b.Navigation("Student");

                    b.Navigation("Teacher");

                    b.Navigation("UserLocation");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentTeacher", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Subject", null)
                        .WithMany()
                        .HasForeignKey("TaughtSubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Location", b =>
                {
                    b.Navigation("SubLocation");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.MultimediaFile", b =>
                {
                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.Navigation("Pictures");

                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
