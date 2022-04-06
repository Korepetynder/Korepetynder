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
    [Migration("20220406183925_AddSubsubjects")]
    partial class AddSubsubjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Korepetynder.Data.DbModels.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Length", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Lengths");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LengthId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LengthId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lessons");

                    b.HasCheckConstraint("CK_Lesson_StudentId_TeacherId", "[StudentId] IS NULL OR [TeacherId] IS NULL");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentLocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentLocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.MultimediaFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("MultimediaFiles");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.ProfilePicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProfilePictures");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PreferredCost")
                        .HasColumnType("int");

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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentSubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentSubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int?>("ProfilePictureId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureId")
                        .IsUnique()
                        .HasFilter("[ProfilePictureId] IS NOT NULL");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LanguageLesson", b =>
                {
                    b.Property<int>("LanguagesId")
                        .HasColumnType("int");

                    b.Property<int>("LessonsId")
                        .HasColumnType("int");

                    b.HasKey("LanguagesId", "LessonsId");

                    b.HasIndex("LessonsId");

                    b.ToTable("LessonLanguages", (string)null);
                });

            modelBuilder.Entity("LessonLevel", b =>
                {
                    b.Property<int>("LessonsId")
                        .HasColumnType("int");

                    b.Property<int>("LevelsId")
                        .HasColumnType("int");

                    b.HasKey("LessonsId", "LevelsId");

                    b.HasIndex("LevelsId");

                    b.ToTable("LessonLevels", (string)null);
                });

            modelBuilder.Entity("LocationStudent", b =>
                {
                    b.Property<int>("PreferredLocationsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("PreferredLocationsId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("StudentPreferredLocations", (string)null);
                });

            modelBuilder.Entity("LocationTeacher", b =>
                {
                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.Property<int>("TeachingLocationsId")
                        .HasColumnType("int");

                    b.HasKey("TeachersId", "TeachingLocationsId");

                    b.HasIndex("TeachingLocationsId");

                    b.ToTable("TeacherTeachingLocations", (string)null);
                });

            modelBuilder.Entity("StudentTeacher", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("TeacherStudents", (string)null);
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Lesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Length", "Length")
                        .WithMany("Lessons")
                        .HasForeignKey("LengthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Student", "Student")
                        .WithMany("PreferredLessons")
                        .HasForeignKey("StudentId");

                    b.HasOne("Korepetynder.Data.DbModels.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", "Teacher")
                        .WithMany("Lessons")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Length");

                    b.Navigation("Student");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Location", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Location", "ParentLocation")
                        .WithMany("Sublocations")
                        .HasForeignKey("ParentLocationId");

                    b.Navigation("ParentLocation");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.MultimediaFile", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", "Owner")
                        .WithMany("MultimediaFiles")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Subject", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Subject", "ParentSubject")
                        .WithMany("Subsubjects")
                        .HasForeignKey("ParentSubjectId");

                    b.Navigation("ParentSubject");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.ProfilePicture", "ProfilePicture")
                        .WithOne("Owner")
                        .HasForeignKey("Korepetynder.Data.DbModels.Teacher", "ProfilePictureId");

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

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LanguageLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LessonLevel", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Level", null)
                        .WithMany()
                        .HasForeignKey("LevelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LocationStudent", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Location", null)
                        .WithMany()
                        .HasForeignKey("PreferredLocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LocationTeacher", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Location", null)
                        .WithMany()
                        .HasForeignKey("TeachingLocationsId")
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

            modelBuilder.Entity("Korepetynder.Data.DbModels.Length", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Location", b =>
                {
                    b.Navigation("Sublocations");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.ProfilePicture", b =>
                {
                    b.Navigation("Owner")
                        .IsRequired();
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Student", b =>
                {
                    b.Navigation("PreferredLessons");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Subject", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Subsubjects");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("MultimediaFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
