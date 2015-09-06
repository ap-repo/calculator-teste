namespace Apponto.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppontoContext : DbContext
    {
        public AppontoContext()
            : base("name=AppontoContext")
        {
        }

        public virtual DbSet<tb_access_level> tb_access_level { get; set; }
        public virtual DbSet<tb_action> tb_action { get; set; }
        public virtual DbSet<tb_company> tb_company { get; set; }
        public virtual DbSet<tb_configuration> tb_configuration { get; set; }
        public virtual DbSet<tb_configuration_limitation> tb_configuration_limitation { get; set; }
        public virtual DbSet<tb_limitation_type> tb_limitation_type { get; set; }
        public virtual DbSet<tb_permission> tb_permission { get; set; }
        public virtual DbSet<tb_permission_group> tb_permission_group { get; set; }
        public virtual DbSet<tb_register> tb_register { get; set; }
        public virtual DbSet<tb_sector> tb_sector { get; set; }
        public virtual DbSet<tb_user> tb_user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_access_level>()
                .Property(e => e.ds_access_level)
                .IsUnicode(false);

            modelBuilder.Entity<tb_access_level>()
                .HasMany(e => e.tb_permission)
                .WithMany(e => e.tb_access_level)
                .Map(m => m.ToTable("tb_access_level_permission"));

            modelBuilder.Entity<tb_access_level>()
                .HasMany(e => e.tb_user)
                .WithMany(e => e.tb_access_level)
                .Map(m => m.ToTable("tb_user_access_level"));

            modelBuilder.Entity<tb_action>()
                .Property(e => e.ds_action)
                .IsUnicode(false);

            modelBuilder.Entity<tb_action>()
                .HasMany(e => e.tb_register)
                .WithRequired(e => e.tb_action)
                .HasForeignKey(e => e.tb_action_id_action)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_company>()
                .Property(e => e.ds_token)
                .IsUnicode(false);

            modelBuilder.Entity<tb_company>()
                .Property(e => e.ds_name)
                .IsUnicode(false);

            modelBuilder.Entity<tb_company>()
                .HasMany(e => e.tb_user)
                .WithOptional(e => e.tb_company)
                .HasForeignKey(e => e.tb_company_id_company);

            modelBuilder.Entity<tb_configuration>()
                .HasMany(e => e.tb_user)
                .WithRequired(e => e.tb_configuration)
                .HasForeignKey(e => e.tb_configuration_id_configuration)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_configuration_limitation>()
                .Property(e => e.ds_host)
                .IsUnicode(false);

            modelBuilder.Entity<tb_configuration_limitation>()
                .HasMany(e => e.tb_configuration)
                .WithRequired(e => e.tb_configuration_limitation)
                .HasForeignKey(e => e.tb_configuration_limitation_id_configuration_limitation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_limitation_type>()
                .Property(e => e.ds_limitation_type)
                .IsUnicode(false);

            modelBuilder.Entity<tb_limitation_type>()
                .HasMany(e => e.tb_configuration_limitation)
                .WithRequired(e => e.tb_limitation_type)
                .HasForeignKey(e => e.tb_limitation_type_id_limitation_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_limitation_type>()
                .HasMany(e => e.tb_register)
                .WithRequired(e => e.tb_limitation_type)
                .HasForeignKey(e => e.tb_limitation_type_id_limitation_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_permission>()
                .Property(e => e.ds_permission)
                .IsUnicode(false);

            modelBuilder.Entity<tb_permission>()
                .Property(e => e.ds_route)
                .IsUnicode(false);

            modelBuilder.Entity<tb_permission_group>()
                .Property(e => e.ds_permission_group)
                .IsUnicode(false);

            modelBuilder.Entity<tb_permission_group>()
                .HasMany(e => e.tb_permission)
                .WithRequired(e => e.tb_permission_group)
                .HasForeignKey(e => e.tb_permission_group_id_permission_group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_register>()
                .Property(e => e.ds_host)
                .IsUnicode(false);

            modelBuilder.Entity<tb_sector>()
                .Property(e => e.ds_sector)
                .IsUnicode(false);

            modelBuilder.Entity<tb_sector>()
                .HasMany(e => e.tb_user)
                .WithRequired(e => e.tb_sector)
                .HasForeignKey(e => e.tb_sector_id_sector)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_user>()
                .Property(e => e.ds_email)
                .IsUnicode(false);

            modelBuilder.Entity<tb_user>()
                .Property(e => e.ds_password)
                .IsUnicode(false);

            modelBuilder.Entity<tb_user>()
                .Property(e => e.ds_name)
                .IsUnicode(false);

            modelBuilder.Entity<tb_user>()
                .Property(e => e.ds_lastname)
                .IsUnicode(false);

            modelBuilder.Entity<tb_user>()
                .HasMany(e => e.tb_register)
                .WithRequired(e => e.tb_user)
                .HasForeignKey(e => e.tb_user_id_user)
                .WillCascadeOnDelete(false);
        }
    }
}
