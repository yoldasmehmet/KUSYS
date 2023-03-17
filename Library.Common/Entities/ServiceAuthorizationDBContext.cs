using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Library.Common.Entities
{
    public partial class ServiceAuthorizationDBContext : DbContext
    {
        public ServiceAuthorizationDBContext()
        {
        }

        public ServiceAuthorizationDBContext(DbContextOptions<ServiceAuthorizationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; } = null!;
        public virtual DbSet<AuthenticationType> AuthenticationTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<VersionInfo> VersionInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("application");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Ip).HasColumnName("ip");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.TicketExpirationSecond).HasColumnName("ticket_expiration_second");
            });

            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable("application_user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationUserRoles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_application_user_role_application_id_application_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ApplicationUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_application_user_role_role_id_role_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplicationUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_application_user_role_user_id_user_id");
            });

            modelBuilder.Entity<AuthenticationType>(entity =>
            {
                entity.ToTable("authentication_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actor).HasColumnName("actor");

                entity.Property(e => e.AuthenticationTypeId).HasColumnName("authentication_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.HasOne(d => d.AuthenticationType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AuthenticationTypeId)
                    .HasConstraintName("FK_user_authentication_type_id_authentication_type_id");
            });

            modelBuilder.Entity<VersionInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VersionInfo");

                entity.HasIndex(e => e.Version, "UC_Version")
                    .IsUnique();

                entity.Property(e => e.AppliedOn).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Description).HasMaxLength(1024);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
