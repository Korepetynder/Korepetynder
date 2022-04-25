﻿// <auto-generated />
using System;
using Korepetynder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    [DbContext(typeof(KorepetynderDbContext))]
    partial class KorepetynderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Polski"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Angielski"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Niemiecki"
                        });
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

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Levels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Szkoła podstawowa",
                            Weight = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Liceum",
                            Weight = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Studia wyższe",
                            Weight = 3
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Warszawa"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Wilanów",
                            ParentLocationId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Śródmieście",
                            ParentLocationId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "Łódź"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Kraków"
                        });
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

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.StudentLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Frequency")
                        .HasColumnType("int");

                    b.Property<int>("PreferredCostMaximum")
                        .HasColumnType("int");

                    b.Property<int>("PreferredCostMinimum")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentLesson");
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

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Subjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Matematyka"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Informatyka"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Chemia"
                        });
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

            modelBuilder.Entity("Korepetynder.Data.DbModels.TeacherLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherLesson");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.HasIndex("TeacherId")
                        .IsUnique()
                        .HasFilter("[TeacherId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LanguageStudentLesson", b =>
                {
                    b.Property<int>("LanguagesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentLessonsId")
                        .HasColumnType("int");

                    b.HasKey("LanguagesId", "StudentLessonsId");

                    b.HasIndex("StudentLessonsId");

                    b.ToTable("LessonLanguages", (string)null);
                });

            modelBuilder.Entity("LanguageTeacherLesson", b =>
                {
                    b.Property<int>("LanguagesId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherLessonsId")
                        .HasColumnType("int");

                    b.HasKey("LanguagesId", "TeacherLessonsId");

                    b.HasIndex("TeacherLessonsId");

                    b.ToTable("TeacherLessonLanguages", (string)null);
                });

            modelBuilder.Entity("LevelStudentLesson", b =>
                {
                    b.Property<int>("LevelsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentLessonsId")
                        .HasColumnType("int");

                    b.HasKey("LevelsId", "StudentLessonsId");

                    b.HasIndex("StudentLessonsId");

                    b.ToTable("LessonLevels", (string)null);
                });

            modelBuilder.Entity("LevelTeacherLesson", b =>
                {
                    b.Property<int>("LevelsId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherLessonsId")
                        .HasColumnType("int");

                    b.HasKey("LevelsId", "TeacherLessonsId");

                    b.HasIndex("TeacherLessonsId");

                    b.ToTable("TeacherLessonLevels", (string)null);
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

            modelBuilder.Entity("Korepetynder.Data.DbModels.StudentLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Student", "Student")
                        .WithMany("PreferredLessons")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Subject", "Subject")
                        .WithMany("StudentLessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.ProfilePicture", "ProfilePicture")
                        .WithOne("Owner")
                        .HasForeignKey("Korepetynder.Data.DbModels.Teacher", "ProfilePictureId");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.TeacherLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Subject", "Subject")
                        .WithMany("TeacherLessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", "Teacher")
                        .WithMany("Lessons")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.User", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Student", "Student")
                        .WithOne("User")
                        .HasForeignKey("Korepetynder.Data.DbModels.User", "StudentId");

                    b.HasOne("Korepetynder.Data.DbModels.Teacher", "Teacher")
                        .WithOne("User")
                        .HasForeignKey("Korepetynder.Data.DbModels.User", "TeacherId");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LanguageStudentLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.StudentLesson", null)
                        .WithMany()
                        .HasForeignKey("StudentLessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LanguageTeacherLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.TeacherLesson", null)
                        .WithMany()
                        .HasForeignKey("TeacherLessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LevelStudentLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Level", null)
                        .WithMany()
                        .HasForeignKey("LevelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.StudentLesson", null)
                        .WithMany()
                        .HasForeignKey("StudentLessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LevelTeacherLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Level", null)
                        .WithMany()
                        .HasForeignKey("LevelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.TeacherLesson", null)
                        .WithMany()
                        .HasForeignKey("TeacherLessonsId")
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

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Subject", b =>
                {
                    b.Navigation("StudentLessons");

                    b.Navigation("TeacherLessons");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Teacher", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("MultimediaFiles");

                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
