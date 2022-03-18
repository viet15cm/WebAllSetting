using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.Models.IdentityModels;

namespace WebProject.AppDbContextLayer
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public IConfiguration Configuration { get; }

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Product> products { get; set; }

        public DbSet<Commodity> commodities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ContextConnection"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var item in builder.Model.GetEntityTypes())
            {
                var tableName = item.GetTableName();

                if (tableName.StartsWith("AspNet"))
                {
                    item.SetTableName(tableName.Substring(6));
                }

            }
            //Khóa Chính
            builder.Entity<Product>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Commodity>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //Mối quan hệ 1 nhiều
            builder.Entity<Product>()
            .HasOne(s => s.Commodity)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.CommodityId);

            //Đánh dấu chỉ mục
            builder.Entity<Product>().HasIndex(x => new { x.Code }).IsUnique();
            builder.Entity<Commodity>().HasIndex(x => new { x.Code }).IsUnique();
        }
    }
}
