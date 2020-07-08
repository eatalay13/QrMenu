using Entities.Models;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Contexts
{
    public partial class ECommerceContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ECommerceContext()
        {
        }

        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<OptionValue> OptionValue { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductOption> ProductOption { get; set; }
        public virtual DbSet<ProductOptionValue> ProductOptionValue { get; set; }
        public virtual DbSet<ProductToCategory> ProductToCategory { get; set; }
        public virtual DbSet<TaxClass> TaxClass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ecommerce;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region AuthModels

            modelBuilder.Entity<User>(x =>
            {
                x.ToTable("Users");
            });


            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<RoleClaim>()
                .ToTable("RoleClaims");

            modelBuilder.Entity<UserClaim>()
                .ToTable("UserClaims");

            modelBuilder.Entity<UserLogin>()
                .ToTable("UserLogins")
                .HasKey(k => new
                {
                    k.UserId,
                    k.ProviderKey
                });

            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasKey(k => new
                {
                    k.UserId,
                    k.RoleId
                });

            modelBuilder.Entity<UserToken>()
                .ToTable("UserTokens")
                .HasKey(k => new
                {
                    k.UserId,
                    k.LoginProvider,
                    k.Name
                });

            #endregion

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<OptionValue>(entity =>
            {
                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.OptionValue)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OptionValue_Option");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StockCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TaxClass)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.TaxClassId)
                    .HasConstraintName("FK_Product_TaxClass");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImage)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImage_Product");
            });

            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.Property(e => e.Value).HasMaxLength(50);

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.ProductOption)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOption_Option");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOption)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOption_Product");
            });

            modelBuilder.Entity<ProductOptionValue>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.OptionValue)
                    .WithMany(p => p.ProductOptionValue)
                    .HasForeignKey(d => d.OptionValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOptionValue_OptionValue");

                entity.HasOne(d => d.ProductOption)
                    .WithMany(p => p.ProductOptionValue)
                    .HasForeignKey(d => d.ProductOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOptionValue_ProductOption");
            });

            modelBuilder.Entity<ProductToCategory>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductToCategory_Category");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductToCategory_Product");
            });

            modelBuilder.Entity<TaxClass>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Rate).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
