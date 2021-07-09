using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.Framework
{
    public partial class ProjectDotNetDbContext : DbContext
    {
        public ProjectDotNetDbContext()
            : base("name=ProjectDotNetDbContext")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Cart_item> Cart_item { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User_Information> User_Information { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Cart_item)
                .WithRequired(e => e.Cart)
                .HasForeignKey(e => e.id_Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Company)
                .HasForeignKey(e => e.id_Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Cart_item)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.id_Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Cart)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasMany(e => e.User_Information)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.id_User)
                .WillCascadeOnDelete(false);
        }
    }
}
