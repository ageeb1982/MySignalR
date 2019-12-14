﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ageebSoft.SignlR.Web.Models.data;

namespace ageebSoft.SignlR.Web.Migrations
{
    [DbContext(typeof(MyDB))]
    [Migration("20191214062101_A2019_12_14")]
    partial class A2019_12_14
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ageebSoft.SignlR.Web.Models.data.GroupsOnline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Date1");

                    b.Property<string>("GroupName");

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.ToTable("GroupsOnline");
                });

            modelBuilder.Entity("ageebSoft.SignlR.Web.Models.data.UsersGroupsOnline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Date1");

                    b.Property<Guid>("GroupsOnlineId");

                    b.Property<Guid>("MyUserId");

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.HasIndex("GroupsOnlineId");

                    b.HasIndex("MyUserId");

                    b.ToTable("UsersGroupsOnline");
                });

            modelBuilder.Entity("ageebSoft.SignlR.Web.Models.data.UsersOnline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Date1");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.ToTable("UsersOnline");
                });

            modelBuilder.Entity("ageebSoft.SignlR.Web.Models.DB.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Date1");

                    b.Property<string>("GroupName");

                    b.Property<string>("Msg")
                        .IsRequired();

                    b.Property<string>("Note");

                    b.Property<Guid>("SenderId");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ageebSoft.SignlR.Web.Models.DB.MyUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Date1");

                    b.Property<string>("Note");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("MyUsers");
                });

            modelBuilder.Entity("ageebSoft.SignlR.Web.Models.data.UsersGroupsOnline", b =>
                {
                    b.HasOne("ageebSoft.SignlR.Web.Models.data.GroupsOnline", "GroupsOnline")
                        .WithMany("UsersGroupsOnlines")
                        .HasForeignKey("GroupsOnlineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ageebSoft.SignlR.Web.Models.DB.MyUser", "MyUser")
                        .WithMany("UsersGroupsOnlines")
                        .HasForeignKey("MyUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ageebSoft.SignlR.Web.Models.DB.Message", b =>
                {
                    b.HasOne("ageebSoft.SignlR.Web.Models.DB.MyUser", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}