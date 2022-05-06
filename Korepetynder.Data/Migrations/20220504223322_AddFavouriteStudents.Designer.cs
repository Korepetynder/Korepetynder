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
    [Migration("20220504223322_AddFavouriteStudents")]
    partial class AddFavouriteStudents
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TutorId");

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
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

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

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentLessons");
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

            modelBuilder.Entity("Korepetynder.Data.DbModels.Tutor", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ProfilePictureId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("ProfilePictureId")
                        .IsUnique()
                        .HasFilter("[ProfilePictureId] IS NOT NULL");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.TutorLesson", b =>
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

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TutorId");

                    b.ToTable("TutorLessons");
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

                    b.HasKey("Id");

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

            modelBuilder.Entity("LanguageTutorLesson", b =>
                {
                    b.Property<int>("LanguagesId")
                        .HasColumnType("int");

                    b.Property<int>("TutorLessonsId")
                        .HasColumnType("int");

                    b.HasKey("LanguagesId", "TutorLessonsId");

                    b.HasIndex("TutorLessonsId");

                    b.ToTable("TutorLessonLanguages", (string)null);
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

            modelBuilder.Entity("LevelTutorLesson", b =>
                {
                    b.Property<int>("LevelsId")
                        .HasColumnType("int");

                    b.Property<int>("TutorLessonsId")
                        .HasColumnType("int");

                    b.HasKey("LevelsId", "TutorLessonsId");

                    b.HasIndex("TutorLessonsId");

                    b.ToTable("TutorLessonLevels", (string)null);
                });

            modelBuilder.Entity("LocationStudent", b =>
                {
                    b.Property<int>("PreferredLocationsId")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentsUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PreferredLocationsId", "StudentsUserId");

                    b.HasIndex("StudentsUserId");

                    b.ToTable("StudentPreferredLocations", (string)null);
                });

            modelBuilder.Entity("LocationTutor", b =>
                {
                    b.Property<int>("TeachingLocationsId")
                        .HasColumnType("int");

                    b.Property<Guid>("TutorsUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TeachingLocationsId", "TutorsUserId");

                    b.HasIndex("TutorsUserId");

                    b.ToTable("TutorTeachingLocations", (string)null);
                });

            modelBuilder.Entity("StudentTutor", b =>
                {
                    b.Property<Guid>("DiscardedByStudentsUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DiscardedTeachersUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DiscardedByStudentsUserId", "DiscardedTeachersUserId");

                    b.HasIndex("DiscardedTeachersUserId");

                    b.ToTable("DiscardedTutorStudents", (string)null);
                });

            modelBuilder.Entity("StudentTutor1", b =>
                {
                    b.Property<Guid>("FavouriteTeachersUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FavouritedByStudentsUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FavouriteTeachersUserId", "FavouritedByStudentsUserId");

                    b.HasIndex("FavouritedByStudentsUserId");

                    b.ToTable("FavouriteTutorStudents", (string)null);
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

                    b.HasOne("Korepetynder.Data.DbModels.Tutor", "Owner")
                        .WithMany("MultimediaFiles")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Student", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("Korepetynder.Data.DbModels.Student", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("Korepetynder.Data.DbModels.Tutor", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.ProfilePicture", "ProfilePicture")
                        .WithOne("Owner")
                        .HasForeignKey("Korepetynder.Data.DbModels.Tutor", "ProfilePictureId");

                    b.HasOne("Korepetynder.Data.DbModels.User", "User")
                        .WithOne("Tutor")
                        .HasForeignKey("Korepetynder.Data.DbModels.Tutor", "UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("ProfilePicture");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.TutorLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Subject", "Subject")
                        .WithMany("TutorLessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Tutor", "Tutor")
                        .WithMany("TutorLessons")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Tutor");
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

            modelBuilder.Entity("LanguageTutorLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.TutorLesson", null)
                        .WithMany()
                        .HasForeignKey("TutorLessonsId")
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

            modelBuilder.Entity("LevelTutorLesson", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Level", null)
                        .WithMany()
                        .HasForeignKey("LevelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.TutorLesson", null)
                        .WithMany()
                        .HasForeignKey("TutorLessonsId")
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
                        .HasForeignKey("StudentsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LocationTutor", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Location", null)
                        .WithMany()
                        .HasForeignKey("TeachingLocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Tutor", null)
                        .WithMany()
                        .HasForeignKey("TutorsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentTutor", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Student", null)
                        .WithMany()
                        .HasForeignKey("DiscardedByStudentsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Tutor", null)
                        .WithMany()
                        .HasForeignKey("DiscardedTeachersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentTutor1", b =>
                {
                    b.HasOne("Korepetynder.Data.DbModels.Tutor", null)
                        .WithMany()
                        .HasForeignKey("FavouriteTeachersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Korepetynder.Data.DbModels.Student", null)
                        .WithMany()
                        .HasForeignKey("FavouritedByStudentsUserId")
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
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Subject", b =>
                {
                    b.Navigation("StudentLessons");

                    b.Navigation("TutorLessons");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.Tutor", b =>
                {
                    b.Navigation("MultimediaFiles");

                    b.Navigation("TutorLessons");
                });

            modelBuilder.Entity("Korepetynder.Data.DbModels.User", b =>
                {
                    b.Navigation("Student");

                    b.Navigation("Tutor");
                });
#pragma warning restore 612, 618
        }
    }
}
