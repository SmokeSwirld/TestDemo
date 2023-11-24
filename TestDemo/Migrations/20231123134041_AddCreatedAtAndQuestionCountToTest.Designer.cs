﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestDemo.Data;

#nullable disable

namespace TestDemo.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231123134041_AddCreatedAtAndQuestionCountToTest")]
    partial class AddCreatedAtAndQuestionCountToTest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestDemo.Data.Entity.Question", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Answer2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Answer3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Answer4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Answer5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AnswerBool1")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerBool2")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerBool3")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerBool4")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerBool5")
                        .HasColumnType("bit");

                    b.Property<Guid>("IdTest")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("NumberQuestion")
                        .HasColumnType("int");

                    b.Property<string>("QuestionTest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionId");

                    b.ToTable("Questionts");
                });

            modelBuilder.Entity("TestDemo.Data.Entity.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameTest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });
#pragma warning restore 612, 618
        }
    }
}
