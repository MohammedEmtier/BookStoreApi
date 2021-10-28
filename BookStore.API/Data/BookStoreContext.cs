using BookStore.API.Configrations;
using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.Entity<Books>().HasOne(e=>e.auther).WithMany().HasForeignKey();
            // builder.Entity<IdentityUserLogin<String>>().HasNoKey();

            //builder.Entity<Auther>();
            //  builder.Ignore<Books>();
            // new BookTypeConfigrations().Co   nfigure(builder.Entity<Books>());
            //builder.Entity<Books>().Ignore(e => e.Description);
            // builder.Entity<Books>().Property(e => e.Description).HasColumnName("DESC");
            //  builder.ApplyConfigurationsFromAssembly(typeof(BookTypeConfigrations).Assembly);
            //  builder.Entity<Books>().ToTable("Books", e => e.ExcludeFromMigrations());
            //builder.Entity<Books>().ToTable("dd",schema:"blog");
            //builder.HasDefaultSchema("books);
            //  builder.Entity<Books>().HasKey(e=>e.Description).HasName("ff");
            //builder.Entity<Books>().Property(e => e.Description).ValueGeneratedOnAdd();
            //builder.Entity<Books>(e =>
            //{
            //    e.Property(e => e.Description).HasColumnType("varchar(200)");
            //    e.Property(e => e.Title).HasMaxLength(200);
            //    e.Property(e => e.Description).HasDefaultValue("fng");
            //    e.Property(e => e.Title).HasComment("gmh");
            //    e.Property(e => e.Title).HasDefaultValueSql("GETDATE()");
            //    e.Property(e=>e.Description).HasComputedColumnSql("[Title]+','+[Title]");
            //    // e.Property(e => e.Title).UseIdentityColumn().HasColumnName("fgf");

            //});
            // builder.Entity<Books>().HasKey(e=> new {e.Id,e.Title });
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Auther> Auther { get; set; }

    }
}
