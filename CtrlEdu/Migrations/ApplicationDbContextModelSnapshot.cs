﻿// <auto-generated />
using System;
using CtrlEdu.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CtrlEdu.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CtrlEdu.Models.AssignmentModel", b =>
                {
                    b.Property<int>("AssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AssignmentID"));

                    b.Property<int>("CourseID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AssignmentID");

                    b.HasIndex("CourseID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("CtrlEdu.Models.CourseModel", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EnrollmetCount")
                        .HasColumnType("integer");

                    b.Property<string>("ImageURL")
                        .HasColumnType("text");

                    b.Property<int>("InstructorID")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CourseID");

                    b.HasIndex("InstructorID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CtrlEdu.Models.EnrollmentModel", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<int>("CourseID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserID", "CourseID");

                    b.HasIndex("CourseID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("CtrlEdu.Models.LearningResourceModel", b =>
                {
                    b.Property<int>("ResourceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ResourceID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CourseID")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ResourceID");

                    b.HasIndex("CourseID");

                    b.ToTable("LearningResourceModel");
                });

            modelBuilder.Entity("CtrlEdu.Models.StudentCourseAssignmentModel", b =>
                {
                    b.Property<int>("StudentCourseAssignmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StudentCourseAssignmentID"));

                    b.Property<int>("AssignmentID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<int>("StudentID")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("StudentCourseAssignmentID");

                    b.HasIndex("AssignmentID");

                    b.HasIndex("StudentID");

                    b.ToTable("StudentCourseAssignments");
                });

            modelBuilder.Entity("CtrlEdu.Models.UserModel", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("EnrollmentModelCourseID")
                        .HasColumnType("integer");

                    b.Property<int?>("EnrollmentModelUserID")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserID");

                    b.HasIndex("EnrollmentModelUserID", "EnrollmentModelCourseID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CtrlEdu.Models.AssignmentModel", b =>
                {
                    b.HasOne("CtrlEdu.Models.CourseModel", "Course")
                        .WithMany("Assignments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CtrlEdu.Models.CourseModel", b =>
                {
                    b.HasOne("CtrlEdu.Models.UserModel", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("CtrlEdu.Models.EnrollmentModel", b =>
                {
                    b.HasOne("CtrlEdu.Models.CourseModel", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CtrlEdu.Models.UserModel", "User")
                        .WithMany("Courses")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CtrlEdu.Models.LearningResourceModel", b =>
                {
                    b.HasOne("CtrlEdu.Models.CourseModel", "Course")
                        .WithMany("LearningResources")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CtrlEdu.Models.StudentCourseAssignmentModel", b =>
                {
                    b.HasOne("CtrlEdu.Models.AssignmentModel", "Assignment")
                        .WithMany("StudentCourseAssignments")
                        .HasForeignKey("AssignmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CtrlEdu.Models.UserModel", "Student")
                        .WithMany("MyProperty")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CtrlEdu.Models.UserModel", b =>
                {
                    b.HasOne("CtrlEdu.Models.EnrollmentModel", null)
                        .WithMany("Users")
                        .HasForeignKey("EnrollmentModelUserID", "EnrollmentModelCourseID");
                });

            modelBuilder.Entity("CtrlEdu.Models.AssignmentModel", b =>
                {
                    b.Navigation("StudentCourseAssignments");
                });

            modelBuilder.Entity("CtrlEdu.Models.CourseModel", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("LearningResources");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CtrlEdu.Models.EnrollmentModel", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CtrlEdu.Models.UserModel", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("MyProperty");
                });
#pragma warning restore 612, 618
        }
    }
}
