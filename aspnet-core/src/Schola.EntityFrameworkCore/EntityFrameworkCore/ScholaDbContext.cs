using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schola.Asignations;
using Schola.Assistances;
using Schola.Authorization.Roles;
using Schola.Authorization.Users;
using Schola.Comments;
using Schola.Courses;
using Schola.DeliveryAssignments;
using Schola.Materiales;
using Schola.MultiTenancy;
using Schola.UserCourses;

namespace Schola.EntityFrameworkCore
{
    public class ScholaDbContext : AbpZeroDbContext<Tenant, Role, User, ScholaDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Course> Course { get; set; }
        public DbSet<Asignation> Asignation { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Assistance> Assistance { get; set; }
        public DbSet<UserCourse> UserCourse { get; set; }
        public DbSet<DeliveryAssignment> DeliveryAssignment { get; set; }
        public ScholaDbContext(DbContextOptions<ScholaDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.UserTeacherId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Course_User");
            });
            modelBuilder.Entity<Asignation>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Asignation)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Asignation_Course");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Asignation)
                    .HasForeignKey(d => d.UserStudentId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Asignation_User");
            });
            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Material_Course");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Comment_Course");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserCommentId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Comment_User");
            });
            modelBuilder.Entity<Assistance>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Assistance)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Assistance_Course");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Assistance)
                    .HasForeignKey(d => d.UserStudentId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Assistance_User");
            });
            modelBuilder.Entity<UserCourse>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCourse)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_UserCourse_User");
                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.UserCourse)
                    .HasForeignKey(d => d.IdCourse)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_UserCourse_Course");
            });

            modelBuilder.Entity<DeliveryAssignment>(entity =>
            {
                entity.HasOne(d => d.UserStudent)
                    .WithMany(p => p.DeliveryAssignment)
                    .HasForeignKey(d => d.UserStudentId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_DeliveryAssignment_User");

                entity.HasOne(d => d.Asignation)
                    .WithMany(p => p.DeliveryAssignment)
                    .HasForeignKey(d => d.AsignationId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_DeliveryAssignment_Asignation");
            });

        }
    }
}
