﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Yarukoto.Data;

#nullable disable

namespace Yarukoto.Data.Migrations
{
    [DbContext(typeof(YarukotoDbContext))]
    partial class YarukotoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Yarukoto.Data.Model.Note", b =>
                {
                    b.Property<string>("NoteId")
                        .HasColumnType("text");

                    b.Property<string>("WorkspaceId")
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("NoteId", "WorkspaceId");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Yarukoto.Data.Model.Workspace", b =>
                {
                    b.Property<string>("WorkspaceId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WorkspaceId");

                    b.ToTable("Workspaces");
                });

            modelBuilder.Entity("Yarukoto.Data.Model.Note", b =>
                {
                    b.HasOne("Yarukoto.Data.Model.Workspace", "Workspace")
                        .WithMany("Notes")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Yarukoto.Data.Model.Workspace", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
